using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TeaseMe.Properties;

namespace TeaseMe
{
    public partial class PreferencesForm : Form
    {
        public PreferencesForm()
        {
            InitializeComponent();
        }


        private void PreferencesForm_Load(object sender, EventArgs e)
        {
            userNameTextBox.Text = Settings.Default.UserName;
            if ("female".Equals(Settings.Default.UserGender))
            {
                 userGenderFemaleRadioButton.Checked = true;
            }
            else
            {
                userGenderMaleRadioButton.Checked = true;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Settings.Default.UserName = userNameTextBox.Text;
            Settings.Default.UserGender = userGenderFemaleRadioButton.Checked ? "female" : "male";
            Settings.Default.Save();

            Close();
        }

    }
}
