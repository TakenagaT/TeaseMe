using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TeaseMe.Common;
using TeaseMe.FlashConversion;

namespace TeaseMe
{
    public partial class OpenForm : Form
    {
        readonly TeaseLibrary teaseLibrary;

        public Tease SelectedTease { get; set; }

        public OpenForm(TeaseLibrary teaseLibrary)
        {
            InitializeComponent();
            this.teaseLibrary = teaseLibrary;
            TeaseFolderTextBox.Text = teaseLibrary.TeasesFolder;

            FillTeaseListView();
        }

        private void FillTeaseListView()
        {
            TeaseListView.Items.Clear();
            foreach (var file in new DirectoryInfo(teaseLibrary.TeasesFolder).GetFiles("*.xml"))
            {
                TeaseListView.Items.Add(file.Name.BeforeLast(file.Extension));
            }
        }

        private void OtherLocationButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == OpenScriptDialog.ShowDialog())
            {
                SelectedTease = teaseLibrary.LoadTease(OpenScriptDialog.FileName);
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (TeaseListView.SelectedItems.Count > 0)
            {
                SelectedTease = teaseLibrary.LoadTease(Path.Combine(teaseLibrary.TeasesFolder, TeaseListView.SelectedItems[0].Text + ".xml"));
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void TeaseListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (TeaseListView.SelectedItems.Count > 0)
            {
                SelectedTease = teaseLibrary.LoadTease(Path.Combine(teaseLibrary.TeasesFolder, TeaseListView.SelectedItems[0].Text + ".xml"));
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                using (var webClient = new WebClient())
                {
                    string xhtml = webClient.DownloadString("http://www.milovana.com/webteases/showflash.php?id=" + TeaseIdTextBox.Text);

                    var match = Regex.Match(xhtml, @"<div id=""headerbar"">\s*<div class=""title"">(?<title>.*?) by <a href=""webteases/#author=(?<authorid>\d+)"" [^>]*>(?<authorname>.*?)</a></div>", RegexOptions.Multiline | RegexOptions.Singleline);

                    TeaseTitleTextBox.Text = match.Groups["title"].Value;
                    AuthorNameTextBox.Text = match.Groups["authorname"].Value;
                    AuthorIdTextBox.Text = match.Groups["authorid"].Value;

                    string[] scriptLines = webClient.DownloadString("http://www.milovana.com/webteases/getscript.php?id=" + TeaseIdTextBox.Text).Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                    SelectedTease = new FlashTeaseConverter().Convert(TeaseIdTextBox.Text, TeaseTitleTextBox.Text, AuthorIdTextBox.Text, AuthorNameTextBox.Text, scriptLines);

                    ConverionErrorTextBox.Visible = SelectedTease.Pages.Exists(page => !String.IsNullOrEmpty(page.Errors));

                    Cursor = Cursors.Default;
                }
            }
            catch (Exception err)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(err.Message, "Error while downloading tease", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (SelectedTease == null)
            {
                return;
            }

            SaveNewScriptDialog.InitialDirectory = teaseLibrary.TeasesFolder;
            SaveNewScriptDialog.FileName = SelectedTease.Title;
            if (DialogResult.OK == SaveNewScriptDialog.ShowDialog())
            {
                Cursor = Cursors.WaitCursor;

                try
                {
                    var saveFile = new FileInfo(SaveNewScriptDialog.FileName);

                    SelectedTease.MediaDirectory = saveFile.Name.BeforeLast(saveFile.Extension);

                    string downloadDirectory = Path.Combine(saveFile.DirectoryName, SelectedTease.MediaDirectory);


                    using (var webClient = new WebClient())
                    {
                        foreach (var page in SelectedTease.Pages)
                        {
                            if (page.Image != null)
                            {
                                string url = String.Format("http://www.milovana.com/media/get.php?folder={0}/{1}&name={2}", SelectedTease.Author.Id, SelectedTease.Id, page.Image.Id);
                                if (DownloadImagesCheckBox.Checked)
                                {
                                    if (page.Image.Id.Contains("*"))
                                    {
                                        page.Errors = String.Format("Warning: random images are not fully supported, the conversion picked a random one and gave it a name. {0}", page.Errors);
                                        page.Image.Id = page.Image.Id.Replace("*", Guid.NewGuid().ToString());

                                    }

                                    string fileName = Path.Combine(downloadDirectory, page.Image.Id);
                                    if (!File.Exists(fileName))
                                    {
                                        if (!Directory.Exists(downloadDirectory))
                                        {
                                            Directory.CreateDirectory(downloadDirectory);
                                        }
                                        webClient.DownloadFile(url, fileName);
                                    }
                                }
                                else
                                {
                                    page.Image.Id = url;
                                }
                            }
                            if (page.Audio != null)
                            {
                                string url = String.Format("http://www.milovana.com/media/get.php?folder={0}/{1}&name={2}", SelectedTease.Author.Id, SelectedTease.Id, page.Audio.Id);
                                if (DownloadImagesCheckBox.Checked)
                                {
                                    if (page.Audio.Id.Contains("*"))
                                    {
                                        page.Errors = String.Format("Warning: random audio is not fully supported, the conversion picked a random one and gave it a name. {0}", page.Errors);
                                        page.Audio.Id = page.Audio.Id.Replace("*", Guid.NewGuid().ToString());
                                    }
                                    string fileName = Path.Combine(downloadDirectory, page.Audio.Id);
                                    if (!File.Exists(fileName))
                                    {
                                        if (!Directory.Exists(downloadDirectory))
                                        {
                                            Directory.CreateDirectory(downloadDirectory);
                                        }
                                        webClient.DownloadFile(url, fileName);
                                    }
                                }
                                else
                                {
                                    page.Audio.Id = url;
                                }
                            }
                        }
                    }

                    MessageBox.Show("Download completed. Select the tease from the list and press start.");

                    string teaseXml = new TeaseSerializer().ConvertToXmlString(SelectedTease);
                    File.WriteAllText(saveFile.FullName, teaseXml);

                    FillTeaseListView();
                    var listviewItem = TeaseListView.FindItemWithText(saveFile.Name.BeforeLast(saveFile.Extension));
                    if (listviewItem != null)
                    {
                        listviewItem.Selected = true;
                        TeaseListView.Focus();
                    }

                    Cursor = Cursors.Default;
                }
                catch (Exception err)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show(err.Message, "Error while saving tease.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Check the errors again as the downloaded media may contain errors.
                ConverionErrorTextBox.Visible = SelectedTease.Pages.Exists(page => !String.IsNullOrEmpty(page.Errors));
            }
        }



    }
}
