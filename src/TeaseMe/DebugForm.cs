using System;
using System.Diagnostics;
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

            var traceListener = new TextBoxTraceListener(tracingTextBox);
            Trace.Listeners.Add(traceListener);
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

    public class TextBoxTraceListener : TraceListener
    {
        private readonly TextBoxBase textBox;

        public TextBoxTraceListener(TextBoxBase textBox)
        {
            this.textBox = textBox;
        }

        public override void Write(string message)
        {
            Action append = () => textBox.AppendText(message);
            if (textBox.InvokeRequired)
            {
                textBox.BeginInvoke(append);
            }
            else
            {
                append();
            }
        }

        public override void WriteLine(string message)
        {
            Write(message + Environment.NewLine);
        }
    }
}
