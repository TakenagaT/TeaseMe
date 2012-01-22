namespace TeaseMe
{
    partial class PreferencesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.userGenderMaleRadioButton = new System.Windows.Forms.RadioButton();
            this.userGenderFemaleRadioButton = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(239, 146);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(158, 146);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(275, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "How do you want to be called in the teases (your name)?";
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Location = new System.Drawing.Point(12, 78);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(302, 20);
            this.userNameTextBox.TabIndex = 1;
            // 
            // userGenderMaleRadioButton
            // 
            this.userGenderMaleRadioButton.AutoSize = true;
            this.userGenderMaleRadioButton.Checked = true;
            this.userGenderMaleRadioButton.Location = new System.Drawing.Point(158, 107);
            this.userGenderMaleRadioButton.Name = "userGenderMaleRadioButton";
            this.userGenderMaleRadioButton.Size = new System.Drawing.Size(47, 17);
            this.userGenderMaleRadioButton.TabIndex = 3;
            this.userGenderMaleRadioButton.TabStop = true;
            this.userGenderMaleRadioButton.Text = "male";
            this.userGenderMaleRadioButton.UseVisualStyleBackColor = true;
            // 
            // userGenderFemaleRadioButton
            // 
            this.userGenderFemaleRadioButton.AutoSize = true;
            this.userGenderFemaleRadioButton.Location = new System.Drawing.Point(239, 107);
            this.userGenderFemaleRadioButton.Name = "userGenderFemaleRadioButton";
            this.userGenderFemaleRadioButton.Size = new System.Drawing.Size(56, 17);
            this.userGenderFemaleRadioButton.TabIndex = 4;
            this.userGenderFemaleRadioButton.Text = "female";
            this.userGenderFemaleRadioButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "What\'s your gender?";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(302, 38);
            this.label3.TabIndex = 7;
            this.label3.Text = "Some teases are using these settings to give you an even more personalized experi" +
    "ence.";
            // 
            // PreferencesForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(326, 181);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.userGenderFemaleRadioButton);
            this.Controls.Add(this.userGenderMaleRadioButton);
            this.Controls.Add(this.userNameTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreferencesForm";
            this.Text = "Preferences";
            this.Load += new System.EventHandler(this.PreferencesForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.RadioButton userGenderMaleRadioButton;
        private System.Windows.Forms.RadioButton userGenderFemaleRadioButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}