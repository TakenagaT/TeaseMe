using System;
using System.Windows.Forms;

namespace TeaseMe
{
    partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();

            ApplicationTitleLabel.Text = TeaseForm.ApplicationTitle;
            ApplicationVersionLabel.Text = TeaseForm.ApplicationVersion;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
