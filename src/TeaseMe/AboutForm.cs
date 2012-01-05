using System;
using System.Reflection;
using System.Windows.Forms;

namespace TeaseMe
{
    partial class AboutForm : Form
    {
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
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public static string AssemblyVersion
        {
            get
            {
                var version =  Assembly.GetExecutingAssembly().GetName().Version;
                return String.Format("v{0}.{1}.{2}", version.Major, version.Minor, version.Build);
            }
        }

        public AboutForm()
        {
            InitializeComponent();

            ApplicationTitleLabel.Text = AssemblyTitle;
            ApplicationVersionLabel.Text = AssemblyVersion;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
