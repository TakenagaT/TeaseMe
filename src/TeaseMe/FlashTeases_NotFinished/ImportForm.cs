using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TeaseMe.Common;
using TeaseMe.FlashConversion;

namespace TeaseMe.FlashTeases
{
    public partial class ImportForm : Form
    {
        public ImportForm()
        {
            InitializeComponent();
        }

        private void FlashTeaseInspectButton_Click(object sender, EventArgs e)
        {
            using (var webClient = new WebClient())
            {
                string xhtml = webClient.DownloadString("http://www.milovana.com/webteases/showflash.php?id=" + TeaseIdTextBox.Text);

                var match = Regex.Match(xhtml, @"<div id=""headerbar"">\s*<div class=""title"">(?<title>.*?) by <a href=""webteases/#author=(?<authorid>\d+)"" [^>]*>(?<authorname>.*?)</a></div>", RegexOptions.Multiline|RegexOptions.Singleline);

                TeaseTitleTextBox.Text = match.Groups["title"].Value;
                AuthorNameTextBox.Text = match.Groups["authorname"].Value;
                AuthorIdTextBox.Text =  match.Groups["authorid"].Value;

                var scriptFile = Path.GetTempFileName();
                webClient.DownloadFile("http://www.milovana.com/webteases/getscript.php?id=" + TeaseIdTextBox.Text, scriptFile);
                string[] scriptLines = File.ReadAllLines(scriptFile);
                new FileInfo(scriptFile).Delete();

                FlashTeaseScriptTextBox.Lines = scriptLines;

                //var mediaFiles = new List<string>();
                //foreach (var line in scriptLines)
                //{
                //    var media = GetFunction(line, "media:pic");  // media:pic(id:"sc1.jpg")
                //    if (!String.IsNullOrEmpty(media))
                //    {
                //        var imgName = GetSubStringBetweenMatchingChars(media, media.IndexOf('"'), '"', '"');
                //        if (!imgName.Contains("*"))
                //        {
                //            mediaFiles.Add(imgName);
                //            DownloadProgressTextBox.AppendText(imgName + Environment.NewLine);

                //            //if (!File.Exists(imgName))
                //            //{
                //            //    webClient.DownloadFile(String.Format("http://www.milovana.com/media/get.php?folder={0}/{1}&name={2}", authorId, id, imgName), imgName);
                //            //}
                //        }
                //    }

                //    // TODO download audio files.
                //}
                
            }
        }

        private void SelectDownloadDirectoryButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == DownloadDirectoryDialog.ShowDialog())
            {
                DownloadDirectoryTextBox.Text = DownloadDirectoryDialog.SelectedPath;
            }
        }


        private void ConvertFlashTeaseButton_Click(object sender, EventArgs e)
        {
            try
            {
                var newTease = new FlashTeaseConverter().Convert(TeaseIdTextBox.Text, TeaseTitleTextBox.Text, AuthorIdTextBox.Text, AuthorNameTextBox.Text, FlashTeaseScriptTextBox.Lines);
                PreviewNewScriptTextBox.Text = new TeaseSerializer().ConvertToXmlString(newTease);
            }
            catch (Exception err)
            {
                MessageBox.Show("Error: " + err.Message, err.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void SaveNewScriptButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == SaveNewScriptDialog.ShowDialog())
            {
                File.WriteAllText(SaveNewScriptDialog.FileName, PreviewNewScriptTextBox.Text);
                MessageBox.Show("File is saved.");
            }
        }

        
    }
}
