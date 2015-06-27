using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

using TeaseMe.Common;

namespace TeaseMe.MilovanaDownload
{
    public partial class MilovanaDownloadControl : UserControl
    {
        private const string MilovanaRootUrl = "https://milovana.com";

        private bool isFlashTease;
        private string teaseId;
        private string teaseUrl;
        private string authorId;
        private FileInfo teaseFile;

        public DirectoryInfo TeasesFolder { get; set; }


        public class DownloadCompletedEventArgs : EventArgs
        {
            public bool Success { get; set; }
            public string TeaseName { get; set; }
        }

        public event EventHandler<DownloadCompletedEventArgs> DownloadCompleted;

        protected virtual void OnDownloadCompleted(DownloadCompletedEventArgs e)
        {
            var handler = DownloadCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public MilovanaDownloadControl()
        {
            InitializeComponent();
            ClearInputFields();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            ClearInputFields();

            teaseUrl = teaseUrlTextBox.Text.Trim();
            var urlMatch = Regex.Match(teaseUrl, "(http|https)://(www.)?milovana.com/webteases/show(?<teaseType>flash|tease).php\\?id=(?<teaseId>\\d+)", RegexOptions.IgnoreCase);
            if (urlMatch.Success)
            {
                isFlashTease = "flash".Equals(urlMatch.Groups["teaseType"].Value, StringComparison.OrdinalIgnoreCase);

                Regex summaryRegex;
                if (isFlashTease)
                {
                    summaryRegex = new Regex(@"<div id=""headerbar"">\s*<div class=""title"">(?<teaseTitle>.*?) by <a href=""webteases/#author=(?<authorId>\d+)"" [^>]*>(?<authorName>.*?)</a></div>", RegexOptions.Multiline | RegexOptions.Singleline);
                }
                else
                {
                    summaryRegex = new Regex("<h1 id=\"tease_title\">(?<teaseTitle>.*?)<span.*?/#author=(?<authorId>\\d+)\".*?>(?<authorName>.*?)</a></span></h1>");
                }

                string xhtml = DownloadString(teaseUrl);

                var summaryMatch = summaryRegex.Match(xhtml);
                if (summaryMatch.Success)
                {
                    teaseTitleTextBox.Text = summaryMatch.Groups["teaseTitle"].Value.Trim();
                    authorNameTextBox.Text = summaryMatch.Groups["authorName"].Value.Trim();
                    authorId = summaryMatch.Groups["authorId"].Value;
                    teaseId = urlMatch.Groups["teaseId"].Value;
                    saveButton.Enabled = true;
                }
                else
                {
                    AppendProgress(Color.Crimson, "Unexpected HTML-format of the page at Milovana. Please report (include the tease id) in the TeaseMe thread at Milovana.");
                }
            }
            else
            {
                AppendProgress(Color.Crimson, "Invalid format for the tease url. The following format is expected:\n\nFor Flash teases:\n{0}/webteases/showflash.php?id=9999\n\nFor HTML teases:\n{0}/webteases/showtease.php?id=9999", MilovanaRootUrl);
            }
        }

        void ClearInputFields()
        {
            progressTextBox.Text = String.Empty;
            teaseTitleTextBox.Text = String.Empty;
            authorNameTextBox.Text = String.Empty;
            authorId = String.Empty;
            teaseId = String.Empty;
            cancelButton.Enabled = false;
            saveButton.Enabled = false;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveDialog.InitialDirectory = TeasesFolder.FullName;
            saveDialog.FileName = teaseTitleTextBox.Text.Remove(Path.GetInvalidFileNameChars());
            if (DialogResult.OK == saveDialog.ShowDialog())
            {
                loadButton.Enabled = false;
                cancelButton.Enabled = true;
                saveButton.Enabled = false;

                teaseFile = new FileInfo(saveDialog.FileName);
                var mediaDirectory = new DirectoryInfo(Path.Combine(teaseFile.DirectoryName, Path.GetFileNameWithoutExtension(teaseFile.Name)));
                if (!mediaDirectory.Exists)
                {
                    mediaDirectory.Create();
                }

                var downloadTask = new DownloadTask
                {
                    TeaseId = teaseId,
                    TeaseTitle = teaseTitleTextBox.Text,
                    TeaseUrl = teaseUrl,
                    AuthorId = authorId,
                    AuthorName = authorNameTextBox.Text,
                    IsFlashTease = isFlashTease,
                    TeaseFile = teaseFile,
                    MediaDirectory = mediaDirectory
                };

                if (!backgroundWorker.IsBusy)
                {
                    backgroundWorker.RunWorkerAsync(downloadTask);
                }
            }
        }


        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var task = e.Argument as DownloadTask;
            if (task != null)
            {
                Tease tease = task.CreateTease();

                if (task.IsFlashTease)
                {
                    string script;
                    string downloadUrl = String.Format("{0}/webteases/getscript.php?id={1}", MilovanaRootUrl, task.TeaseId);
                    backgroundWorker.ReportProgress(0, "Downloading " + downloadUrl);
                    try
                    {
                        script = Encoding.UTF8.GetString(new WebClient().DownloadData(downloadUrl));
                        backgroundWorker.ReportProgress(0, "Ok");
                        new FlashTeaseConverter().AddPages(tease, script);
                    }
                    catch (Exception err)
                    {
                        backgroundWorker.ReportProgress(1, String.Format("Error: [{0}] {1}", err.GetType(), err.Message));
                        return;
                    }
                }
                else
                {
                    string firstPageHtml;
                    string downloadUrl = String.Format("{0}/webteases/showtease.php?id={1}", MilovanaRootUrl, task.TeaseId);
                    backgroundWorker.ReportProgress(0, "Downloading " + downloadUrl);
                    try
                    {
                        firstPageHtml = Encoding.UTF8.GetString(new WebClient().DownloadData(downloadUrl));
                        backgroundWorker.ReportProgress(0, "Ok");

                        var page = HtmlTeaseConverter.CreatePage("start", firstPageHtml);
                        tease.Pages.Add(page);
                        while (page.ButtonList.Count > 0)
                        {
                            if (backgroundWorker.CancellationPending)
                            {
                                return;
                            }

                            string url = String.Format("{0}/webteases/showtease.php?id={1}&p={2}#t", MilovanaRootUrl, tease.Id, page.ButtonList[0].Target);
                            backgroundWorker.ReportProgress(0, "Downloading " + url);
                            try
                            {
                                string nextPageHtml = Encoding.UTF8.GetString(new WebClient().DownloadData(url));
                                backgroundWorker.ReportProgress(0, "Ok");
                                page = HtmlTeaseConverter.CreatePage(page.ButtonList[0].Target, nextPageHtml);
                                tease.Pages.Add(page);

                                // Be nice to the Milovana webserver and wait a bit before the next request...
                                Thread.Sleep(800);
                            }
                            catch (Exception err)
                            {
                                backgroundWorker.ReportProgress(1, String.Format("Error: [{0}] {1}", err.GetType(), err.Message));
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        backgroundWorker.ReportProgress(1, String.Format("Error: [{0}] {1}", err.GetType(), err.Message));
                        return;
                    }
                }

                foreach (var page in tease.Pages)
                {
                    if (backgroundWorker.CancellationPending)
                    {
                        return;
                    }

                    if (page.ImageList.Count > 0)
                    {
                        DownloadMedia(task, page, page.ImageList[0]);
                    }
                    if (page.AudioList.Count > 0)
                    {
                        DownloadMedia(task, page, page.AudioList[0]);
                    }
                }

                e.Result = tease;
            }
        }

        void DownloadMedia(DownloadTask task, TeasePage page, TeaseMedia teaseMedia)
        {
            string mediaName = teaseMedia.Id;
            if (teaseMedia.Id.Contains("*"))
            {
                mediaName = teaseMedia.Id.Replace("*", String.Format("[{0}]", Guid.NewGuid()));
            }
            if (teaseMedia.Id.StartsWith("http://") || teaseMedia.Id.StartsWith("https://"))
            {
                mediaName = new Uri(teaseMedia.Id).Segments.Last();
            }
            string fileName = Path.Combine(task.MediaDirectory.FullName, mediaName);
            if (!File.Exists(fileName))
            {
                string url = String.Format("{0}/media/get.php?folder={1}/{2}&name={3}", MilovanaRootUrl, task.AuthorId, task.TeaseId, teaseMedia.Id);
                if (teaseMedia.Id.StartsWith("http://") || teaseMedia.Id.StartsWith("https://"))
                {
                    url = teaseMedia.Id;
                }
                try
                {
                    using (var client = new WebClient())
                    {
                        backgroundWorker.ReportProgress(0, "Downloading " + url);
                        
                        // Be nice to the Milovana webserver and wait a bit before the next request...
                        Thread.Sleep(800);

                        client.DownloadFile(url, fileName);
                        backgroundWorker.ReportProgress(0, "Ok");
                    }
                }
                catch (Exception err)
                {
                    backgroundWorker.ReportProgress(1, String.Format("Error: [{0}] {1}", err.GetType(), err.Message));
                    page.Errors = String.Format("Error while downloading file '{0}'. {1}.", url, page.Errors);
                }
            }
            if (teaseMedia.Id.StartsWith("http://") || teaseMedia.Id.StartsWith("https://"))
            {
                teaseMedia.Id = mediaName;
            }
        }


        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            AppendProgress(e.ProgressPercentage == 0 ? Color.Black : Color.Crimson, String.Format("{0}", e.UserState));
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                AppendProgress(Color.Black, "Download canceled.");
            }
            else
            {
                AppendProgress(Color.Black, "Download completed.");
                Tease tease = e.Result as Tease;
                if (tease != null)
                {
                    string teaseXml = new TeaseSerializer().ConvertToXmlString(tease);
                    File.WriteAllText(teaseFile.FullName, teaseXml);

                    if (tease.Pages.Exists(page => !String.IsNullOrEmpty(page.Errors)))
                    {
                        AppendProgress(Color.Crimson, "There are some errors. Try to correct the errors in the script using a text editor.");
                    }
                    else
                    {
                        AppendProgress(Color.Black, "Select the tease from the list and press start.");
                        OnDownloadCompleted(new DownloadCompletedEventArgs { Success = true, TeaseName = Path.GetFileNameWithoutExtension(teaseFile.Name) });
                    }
                }
            }

            cancelButton.Enabled = false;
            loadButton.Enabled = true;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker.CancellationPending)
            {
                AppendProgress(Color.LightSteelBlue, "Canceling...");
                backgroundWorker.CancelAsync();
            }
        }


        private void AppendProgress(Color color, string format, params object[] args)
        {
            int start = progressTextBox.TextLength;
            progressTextBox.AppendText(String.Format(format, args) + Environment.NewLine);
            progressTextBox.ScrollToCaret();
            int end = progressTextBox.TextLength;
            progressTextBox.Select(start, end - start);
            progressTextBox.SelectionColor = color;
            progressTextBox.SelectionLength = 0;
        }


        private string DownloadString(string address)
        {
            Cursor = Cursors.WaitCursor;
            AppendProgress(Color.Black, "Downloading {0}", address);
            try
            {
                using (var client = new WebClient())
                {
                    var data = client.DownloadData(address);
                    AppendProgress(Color.Black, "Ok");
                    return Encoding.UTF8.GetString(data);
                }
            }
            catch (Exception err)
            {
                AppendProgress(Color.Crimson, "An error occured: [{1}] {2}", address, err.GetType(), err.Message);
                return String.Empty;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private class DownloadTask
        {
            public string TeaseId { get; set; }
            public string TeaseTitle { get; set; }
            public string TeaseUrl { get; set; }
            public string AuthorId { get; set; }
            public string AuthorName { get; set; }
            public bool IsFlashTease { get; set; }

            public FileInfo TeaseFile { get; set; }
            public DirectoryInfo MediaDirectory { get; set; }

            public Tease CreateTease()
            {
                return new Tease
                {
                    Id = TeaseId,
                    Title = TeaseTitle,
                    Url = TeaseUrl,
                    Author = new Author
                    {
                        Id = AuthorId,
                        Name = AuthorName,
                        Url = MilovanaRootUrl + "/forum/memberlist.php?mode=viewprofile&u=" + AuthorId
                    },
                    MediaDirectory = MediaDirectory.Name
                };
            }
        }

    }
}
