using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;
using TeaseMe.FlashTeases;

namespace TeaseMe
{
    public partial class TeaseForm : Form
    {
        private readonly Audio audio = new Audio();

        public const string ApplicationTitle = "TeaseMe";
        public const string ApplicationVersion = "v0.1";

        static readonly  string DefaultUrl = ConfigurationManager.AppSettings["DefaultUrl"];

        static readonly  string MetronomeAudio = ConfigurationManager.AppSettings["MetronomeAudio"];

        private Tease currentTease;

        private TeaseLibrary teaseLibrary;

        private int secondsUntilNextPage;
        
        private string ApplicationDirectory
        {
            get { return new FileInfo(Application.ExecutablePath).DirectoryName; }
        }

        public TeaseForm()
        {
            InitializeComponent();
        }


        private void TeaseForm_Load(object sender, EventArgs e)
        {
            Text = ApplicationTitle + " " + ApplicationVersion;

            teaseLibrary = new TeaseLibrary(ApplicationDirectory);

            SetCurrentTease(teaseLibrary.EmptyTease());

            DebugPanel.Visible = false;
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Shift | Keys.D))
            {
                DebugPanel.Visible = !DebugPanel.Visible;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        
        private void SetCurrentTease(Tease tease)
        {
            try
            {
                PictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                currentTease = tease;

                TeaseTitleLabel.Text = currentTease.Title;
                AuthorNameLabel.Text = (currentTease.Author != null) ? currentTease.Author.Name : String.Empty;

                ToolTips.SetToolTip(OnlineButton, String.Format("Open {0} in web browser", !String.IsNullOrEmpty(currentTease.Url) ? currentTease.Url : DefaultUrl));

                if (currentTease.Author != null && !String.IsNullOrEmpty(currentTease.Author.Url))
                {
                    AuthorNameLabel.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Underline, GraphicsUnit.Point, 0);
                    AuthorNameLabel.ForeColor = Color.SkyBlue;
                    ToolTips.SetToolTip(AuthorNameLabel, String.Format("Open {0} in web browser", currentTease.Author.Url));
                }
                else
                {
                    AuthorNameLabel.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
                    AuthorNameLabel.ForeColor = Color.Gainsboro;
                    ToolTips.SetToolTip(AuthorNameLabel, null);
                }

                PagesComboBox.Sorted = true;
                PagesComboBox.Items.Clear();
                currentTease.Pages.ForEach(page => PagesComboBox.Items.Add(page.Id));

                currentTease.CurrentPageChanged += currentTease_CurrentPageChanged;

                currentTease.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        } 

        
        void currentTease_CurrentPageChanged(object sender, TeasePageEventArgs e)
        {
            PagesComboBox.SelectedItem = currentTease.CurrentPage.Id;
            PagePropertyGrid.SelectedObject = currentTease.CurrentPage;

            SetText();
            SetMedia();
            SetButtons();
            SetDelay();
            SetMetronome();
        }

        private void SetText()
        {
            TeaseTextBox.Text = currentTease.CurrentPage.Text;
        }

        private void SetMedia()
        {
            PictureBox1.Visible = false;
            if (currentTease.CurrentPage.Image != null)
            {   
                string fileName = currentTease.GetFileName(currentTease.CurrentPage.Image);
                if (!String.IsNullOrEmpty(fileName))
                {
                    PictureBox1.Image = Image.FromFile(fileName);
                    PictureBox1.Visible = true;
                }
            }

            // Audio and Metronome cannot be combined in the same page.
            if (currentTease.CurrentPage.Audio != null && currentTease.CurrentPage.Metronome == null)
            {
                string fileName = currentTease.GetFileName(currentTease.CurrentPage.Audio);
                if (!String.IsNullOrEmpty(fileName))
                {
                    audio.Play(fileName);    
                }
            }

            MediaPlayer.Visible = false;
            if (currentTease.CurrentPage.Video != null)
            {
                string fileName = currentTease.GetFileName(currentTease.CurrentPage.Video);
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
            currentTease.CurrentPage.AvailableButtons.ForEach(AddButton);

            // Set a default button so that you can use the space bar or enter key.
            if (currentTease.CurrentPage.AvailableButtons.Count > 0)
            {
                ButtonPanel.Controls[0].Focus();
            }
        }

        private void SetDelay()
        {
            CountdownTimer.Stop();
            CountdownPanel.Visible = currentTease.CurrentPage.AvailableDelay != null && currentTease.CurrentPage.AvailableDelay.Style != DelayStyle.Hidden;
            if (currentTease.CurrentPage.AvailableDelay != null)
            {
                secondsUntilNextPage = currentTease.GetInteger(currentTease.CurrentPage.AvailableDelay.Seconds);
                UpdateCountDownPanel();
                CountdownTimer.Start();
            }
        }

        private void SetMetronome()
        {
            MetronomeTimer.Stop();
            if (currentTease.CurrentPage.Metronome != null)
            {
                int bpm = currentTease.GetInteger(currentTease.CurrentPage.Metronome.BeatsPerMinute);
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
            UpdateCountDownPanel();
            if (secondsUntilNextPage == 0)
            {
                CountdownTimer.Stop();
                ExecuteTeaseAction(currentTease.CurrentPage.AvailableDelay);
            }
        }

        private void UpdateCountDownPanel()
        {
            var left = new TimeSpan(0, 0, secondsUntilNextPage);
            TimeLeftLabel.Text = (currentTease.CurrentPage.AvailableDelay.Style == DelayStyle.Secret) ?  "??:??" : String.Format("{0:00}:{1:00}", Math.Floor(left.TotalMinutes), left.Seconds); 
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
                currentTease.ExecuteTeaseAction(teaseAction);
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
            OpenScript.InitialDirectory = teaseLibrary.TeasesFolder;
            if (DialogResult.OK == OpenScript.ShowDialog())
            {
                SetCurrentTease(teaseLibrary.LoadTease(OpenScript.FileName));
            }
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            new ImportForm().Show();
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }

        private void OnlineButton_Click(object sender, EventArgs e)
        {
            string url = DefaultUrl;
            if (currentTease != null && !String.IsNullOrEmpty(currentTease.Url))
            {
                url = currentTease.Url;
            }
            Process.Start(url);
        }

        private void AuthorNameLabel_Click(object sender, EventArgs e)
        {
            if (currentTease != null && currentTease.Author != null && !String.IsNullOrEmpty(currentTease.Author.Url))
            {
                Process.Start(currentTease.Author.Url);
            }
        }

        private void PagesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentTease.NavigateToPage(PagesComboBox.SelectedItem.ToString());
        }







    }
}
