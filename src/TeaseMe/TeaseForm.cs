using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;
using TeaseMe.Common;
using TeaseMe.Properties;

namespace TeaseMe
{
    public partial class TeaseForm : Form
    {
        private readonly Audio audio = new Audio();

        static readonly string DefaultUrl = ConfigurationManager.AppSettings["DefaultUrl"];

        static readonly string MetronomeAudio = ConfigurationManager.AppSettings["MetronomeAudio"];

        static readonly string HtmlTextTemplate = File.ReadAllText(@"Resources\HtmlTextTemplate.html");

        private readonly DebugForm debugForm;

        public Tease CurrentTease;

        private TeaseLibrary teaseLibrary;

        private int secondsUntilNextPage;
        // There might be a difference between the remaining time shown to the user and the actual time.
        private int secondsShownUntilNextPage;


        private FormWindowState windowState;
        private FormBorderStyle borderStyle;
        private bool topMost;
        private Rectangle bounds;
        private bool isFullscreen;


        private static string ApplicationDirectory
        {
            get { return new FileInfo(Application.ExecutablePath).DirectoryName; }
        }

        public static string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    var titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public static string AssemblyVersion
        {
            get
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                return String.Format("v{0}.{1}.{2}", version.Major, version.Minor, version.Build);
            }
        }

        public TeaseForm()
        {
            InitializeComponent();
            debugForm = new DebugForm(this);
        }


        private void TeaseForm_Load(object sender, EventArgs e)
        {
            Text = AssemblyTitle + " " + AssemblyVersion;

            teaseLibrary = new TeaseLibrary(ApplicationDirectory);

            SetCurrentTease(teaseLibrary.EmptyTease());


        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Shift | Keys.D))
            {
                debugForm.Show();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void SetCurrentTease(Tease tease)
        {
            try
            {
                PictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                CurrentTease = tease;

                TeaseTitleLabel.Text = CurrentTease.Title;
                if (!String.IsNullOrEmpty(CurrentTease.Title))
                {
                    TeaseTitleLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Underline, GraphicsUnit.Point, 0);
                    TeaseTitleLabel.ForeColor = Color.SkyBlue;
                    ToolTips.SetToolTip(TeaseTitleLabel, String.Format("Open {0} in web browser", CurrentTease.Url));
                }
                else
                {
                    TeaseTitleLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
                    TeaseTitleLabel.ForeColor = Color.Gainsboro;
                    ToolTips.SetToolTip(TeaseTitleLabel, null);
                }

                AuthorNameLabel.Text = (CurrentTease.Author != null) ? CurrentTease.Author.Name : String.Empty;
                if (CurrentTease.Author != null && !String.IsNullOrEmpty(CurrentTease.Author.Url))
                {
                    AuthorNameLabel.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Underline, GraphicsUnit.Point, 0);
                    AuthorNameLabel.ForeColor = Color.SkyBlue;
                    ToolTips.SetToolTip(AuthorNameLabel, String.Format("Open {0} in web browser", CurrentTease.Author.Url));
                }
                else
                {
                    AuthorNameLabel.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
                    AuthorNameLabel.ForeColor = Color.Gainsboro;
                    ToolTips.SetToolTip(AuthorNameLabel, null);
                }

                debugForm.PagesComboBox.Sorted = true;
                debugForm.PagesComboBox.Items.Clear();
                CurrentTease.Pages.ForEach(page => debugForm.PagesComboBox.Items.Add(page.Id));

                CurrentTease.CurrentPageChanged += currentTease_CurrentPageChanged;

                CurrentTease.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        void currentTease_CurrentPageChanged(object sender, TeasePageEventArgs e)
        {
            debugForm.PagesComboBox.SelectedItem = CurrentTease.CurrentPage.Id;
            debugForm.PagePropertyGrid.SelectedObject = CurrentTease.CurrentPage;

            SetText(CurrentTease.CurrentPage.Text);
            SetMedia();
            SetButtons();
            SetDelay();
            SetMetronome();
        }

        private void SetText(string text)
        {
            // The extra steps are necessary because of strange/wrong behaviour of the webbrowser control.
            TeaseTextWebBrowser.Navigate("about:blank");
            if (TeaseTextWebBrowser.Document != null)
            {
                TeaseTextWebBrowser.Document.Write(String.Empty);
            }
            TeaseTextWebBrowser.DocumentText = HtmlTextTemplate.Replace("[TEXT]", text);
        }


        private void SetMedia()
        {
            PictureBox1.Visible = false;
            if (CurrentTease.CurrentPage.Image != null)
            {
                string fileName = CurrentTease.GetFileName(CurrentTease.CurrentPage.Image);
                if (!String.IsNullOrEmpty(fileName))
                {
                    PictureBox1.ImageLocation = fileName;
                    PictureBox1.Visible = true;
                }
            }

            // Audio and Metronome cannot be combined in the same page.
            MediaPlayer.Visible = false;
            MediaPlayer.URL = null;
            if (CurrentTease.CurrentPage.Audio != null && CurrentTease.CurrentPage.Metronome == null)
            {
                string fileName = CurrentTease.GetFileName(CurrentTease.CurrentPage.Audio);
                if (!String.IsNullOrEmpty(fileName))
                {
                    MediaPlayer.uiMode = "none";
                    MediaPlayer.stretchToFit = true;
                    MediaPlayer.enableContextMenu = false;
                    MediaPlayer.Ctlenabled = false;
                    MediaPlayer.URL = fileName;
                }
            }


            if (CurrentTease.CurrentPage.Video != null)
            {
                string fileName = CurrentTease.GetFileName(CurrentTease.CurrentPage.Video);
                if (!String.IsNullOrEmpty(fileName))
                {
                    MediaPlayer.uiMode = "none";
                    MediaPlayer.stretchToFit = true;
                    MediaPlayer.enableContextMenu = false;
                    MediaPlayer.Ctlenabled = false;
                    MediaPlayer.URL = fileName;
                    MediaPlayer.Visible = true;
                }
            }
        }

        private void SetButtons()
        {
            ClearButtons();
            CurrentTease.CurrentPage.AvailableButtons.ForEach(AddButton);

            // Set a default button so that you can use the space bar or enter key.
            if (CurrentTease.CurrentPage.AvailableButtons.Count > 0)
            {
                ButtonPanel.Controls[0].Focus();
            }
        }

        private void SetDelay()
        {
            CountdownTimer.Stop();
            CountdownPanel.Visible = CurrentTease.CurrentPage.AvailableDelay != null && CurrentTease.CurrentPage.AvailableDelay.Style != DelayStyle.Hidden;
            if (CurrentTease.CurrentPage.AvailableDelay != null)
            {
                secondsUntilNextPage = CurrentTease.GetInteger(CurrentTease.CurrentPage.AvailableDelay.Seconds);

                secondsShownUntilNextPage = secondsUntilNextPage;
                if (!String.IsNullOrEmpty(CurrentTease.CurrentPage.AvailableDelay.StartWithSeconds))
                {
                    secondsShownUntilNextPage = CurrentTease.GetInteger(CurrentTease.CurrentPage.AvailableDelay.StartWithSeconds);
                }
                

                UpdateCountDownPanel();
                CountdownTimer.Start();
            }
        }

        private void SetMetronome()
        {
            MetronomeTimer.Stop();
            if (CurrentTease.CurrentPage.Metronome != null)
            {
                int bpm = CurrentTease.GetInteger(CurrentTease.CurrentPage.Metronome.BeatsPerMinute);
                if (1 <= bpm && bpm <= 250)
                {
                    MetronomeTimer.Interval = Convert.ToInt32(1000f * (60f / bpm));
                    MetronomeTimer.Start();
                }
            }
        }


        private void CountdownTick(object sender, EventArgs e)
        {
            secondsUntilNextPage--;
            secondsShownUntilNextPage--;
            UpdateCountDownPanel();
            if (secondsUntilNextPage == 0)
            {
                CountdownTimer.Stop();
                ExecuteTeaseAction(CurrentTease.CurrentPage.AvailableDelay);
            }
        }

        private void UpdateCountDownPanel()
        {
            var left = new TimeSpan(0, 0, secondsShownUntilNextPage);
            TimeLeftLabel.Text = (CurrentTease.CurrentPage.AvailableDelay.Style == DelayStyle.Secret) ? "??:??" : String.Format("{0:00}:{1:00}", Math.Floor(left.TotalMinutes), left.Seconds);
        }

        private void MetronomeTick(object sender, EventArgs e)
        {
            audio.Play(Path.Combine(ApplicationDirectory, MetronomeAudio));
        }

        private void ClearButtons()
        {
            foreach (var childControl in ButtonPanel.Controls)
            {
                var button = childControl as Button;
                if (button != null)
                {
                    button.Click -= ButtonClick;
                }
            }
            ButtonPanel.Controls.Clear();
        }

        private void AddButton(TeaseButton teaseButton)
        {
            Button btn = CloneSampleButton();

            btn.Text = teaseButton.Text;
            btn.Tag = teaseButton;
            btn.Click += ButtonClick;

            ButtonPanel.Controls.Add(btn);
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            var teaseButton = ((Button)sender).Tag as TeaseButton;
            ExecuteTeaseAction(teaseButton);
        }


        private void ExecuteTeaseAction(TeaseAction teaseAction)
        {
            try
            {
                CurrentTease.ExecuteTeaseAction(teaseAction);
            }
            catch (Exception err)
            {
                MessageBox.Show("Error: " + err.Message);
            }
        }



        private Button CloneSampleButton()
        {
            return new Button
            {
                BackColor = SampleButton.BackColor,
                FlatStyle = SampleButton.FlatStyle,
                UseVisualStyleBackColor = SampleButton.UseVisualStyleBackColor,
                Font = SampleButton.Font,
                MinimumSize = SampleButton.MinimumSize,
                MaximumSize = SampleButton.MaximumSize,
                RightToLeft = RightToLeft.No,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowOnly
            };
        }


        private void OpenButton_Click(object sender, EventArgs e)
        {
            using (var popup = new OpenForm(teaseLibrary))
            {
                if (DialogResult.OK == popup.ShowDialog())
                {
                    SetCurrentTease(popup.SelectedTease);
                }
            }
        }


        private void TeaseTitleLabel_Click(object sender, EventArgs e)
        {
            string url = DefaultUrl;
            if (CurrentTease != null && !String.IsNullOrEmpty(CurrentTease.Url))
            {
                url = CurrentTease.Url;
            }
            Process.Start(url);
        }

        private void AuthorNameLabel_Click(object sender, EventArgs e)
        {
            if (CurrentTease != null && CurrentTease.Author != null && !String.IsNullOrEmpty(CurrentTease.Author.Url))
            {
                Process.Start(CurrentTease.Author.Url);
            }
        }

        private void FullscreenButton_Click(object sender, EventArgs e)
        {
            if (isFullscreen)
            {
                RestoreScreen();
            }
            else
            {
                Fullscreen();
            }
        }

        private void TeaseForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (isFullscreen)
                {
                    RestoreScreen();
                }
            }
        }


        private void Fullscreen()
        {
            if (!isFullscreen)
            {
                windowState = WindowState;
                borderStyle = FormBorderStyle;
                topMost = TopMost;
                bounds = Bounds; 
                
                WindowState = FormWindowState.Maximized;
                FormBorderStyle = FormBorderStyle.None;
                TopMost = true;
                WinApi.SetWinFullScreen(Handle);
                
                FullscreenButton.Image = Resources.down_left;
                ToolTips.SetToolTip(FullscreenButton, "Exit fullscreen (ESC)");
                isFullscreen = true;
            }
        }

        private void RestoreScreen()
        {
            WindowState = windowState;
            FormBorderStyle = borderStyle;
            TopMost = topMost;
            Bounds = bounds;
            
            FullscreenButton.Image = Resources.up_right;
            ToolTips.SetToolTip(FullscreenButton, "Fullscreen");
            isFullscreen = false;
        }

    }
}
