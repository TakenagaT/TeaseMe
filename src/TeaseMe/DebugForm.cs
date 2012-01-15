using System;
using System.Windows.Forms;

namespace TeaseMe
{
    public partial class DebugForm : Form
    {
        private readonly TeaseForm teaseForm;

        public DebugForm(TeaseForm teaseForm)
        {
            this.teaseForm = teaseForm;
            InitializeComponent();
        }
        
        private void PagesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            teaseForm.CurrentTease.NavigateToPage(PagesComboBox.SelectedItem.ToString());
        }

        private void DebugForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }
    }
}
