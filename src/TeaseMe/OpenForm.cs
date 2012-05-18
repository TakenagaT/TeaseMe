using System;
using System.IO;
using System.Windows.Forms;
using TeaseMe.Common;
using TeaseMe.MilovanaDownload;

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

            milovanaDownloadControl.TeasesFolder = new DirectoryInfo(teaseLibrary.TeasesFolder);
            milovanaDownloadControl.DownloadCompleted += milovanaDownloadControl_DownloadCompleted;

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

        void milovanaDownloadControl_DownloadCompleted(object sender, MilovanaDownloadControl.DownloadCompletedEventArgs e)
        {
            if (e.Success)
            {
                FillTeaseListView();
                var listviewItem = TeaseListView.FindItemWithText(e.TeaseName);
                if (listviewItem != null)
                {
                    listviewItem.Selected = true;
                    TeaseListView.Focus();
                }
            }
        }
    }
}
