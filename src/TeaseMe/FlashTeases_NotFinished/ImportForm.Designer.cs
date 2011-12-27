namespace TeaseMe.FlashTeases
{
    partial class ImportForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.SaveNewScriptButton = new System.Windows.Forms.Button();
            this.PreviewNewScriptTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.OpenOldScriptDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveNewScriptDialog = new System.Windows.Forms.SaveFileDialog();
            this.DownloadDirectoryDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.DownloadTabPage = new System.Windows.Forms.TabPage();
            this.TeaseIdTextBox = new System.Windows.Forms.TextBox();
            this.FlashTeaseScriptTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ConvertFlashTeaseButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.DownloadProgressTextBox = new System.Windows.Forms.TextBox();
            this.DownloadMediaButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.TeaseTitleTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.AuthorNameTextBox = new System.Windows.Forms.TextBox();
            this.AuthorIdTextBox = new System.Windows.Forms.TextBox();
            this.FlashTeaseInspectButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.DownloadDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.SelectDownloadDirectoryButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.DownloadTabPage.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 0;
            // 
            // SaveNewScriptButton
            // 
            this.SaveNewScriptButton.Location = new System.Drawing.Point(81, 583);
            this.SaveNewScriptButton.Name = "SaveNewScriptButton";
            this.SaveNewScriptButton.Size = new System.Drawing.Size(106, 23);
            this.SaveNewScriptButton.TabIndex = 9;
            this.SaveNewScriptButton.Text = "Save as...";
            this.SaveNewScriptButton.UseVisualStyleBackColor = true;
            this.SaveNewScriptButton.Click += new System.EventHandler(this.SaveNewScriptButton_Click);
            // 
            // PreviewNewScriptTextBox
            // 
            this.PreviewNewScriptTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PreviewNewScriptTextBox.Location = new System.Drawing.Point(81, 318);
            this.PreviewNewScriptTextBox.Multiline = true;
            this.PreviewNewScriptTextBox.Name = "PreviewNewScriptTextBox";
            this.PreviewNewScriptTextBox.ReadOnly = true;
            this.PreviewNewScriptTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.PreviewNewScriptTextBox.Size = new System.Drawing.Size(688, 259);
            this.PreviewNewScriptTextBox.TabIndex = 8;
            this.PreviewNewScriptTextBox.WordWrap = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 321);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Preview";
            // 
            // OpenOldScriptDialog
            // 
            this.OpenOldScriptDialog.DefaultExt = "xml";
            this.OpenOldScriptDialog.Filter = "Teases|*.xml|All files|*.*";
            this.OpenOldScriptDialog.Title = "Selecct your tease";
            // 
            // SaveNewScriptDialog
            // 
            this.SaveNewScriptDialog.DefaultExt = "xml";
            this.SaveNewScriptDialog.Filter = "Teases|*.xml|All files|*.*";
            // 
            // DownloadTabPage
            // 
            this.DownloadTabPage.BackColor = System.Drawing.SystemColors.Control;
            this.DownloadTabPage.Controls.Add(this.SelectDownloadDirectoryButton);
            this.DownloadTabPage.Controls.Add(this.DownloadDirectoryTextBox);
            this.DownloadTabPage.Controls.Add(this.AuthorIdTextBox);
            this.DownloadTabPage.Controls.Add(this.AuthorNameTextBox);
            this.DownloadTabPage.Controls.Add(this.TeaseTitleTextBox);
            this.DownloadTabPage.Controls.Add(this.DownloadProgressTextBox);
            this.DownloadTabPage.Controls.Add(this.FlashTeaseScriptTextBox);
            this.DownloadTabPage.Controls.Add(this.TeaseIdTextBox);
            this.DownloadTabPage.Controls.Add(this.label10);
            this.DownloadTabPage.Controls.Add(this.label6);
            this.DownloadTabPage.Controls.Add(this.FlashTeaseInspectButton);
            this.DownloadTabPage.Controls.Add(this.label9);
            this.DownloadTabPage.Controls.Add(this.label7);
            this.DownloadTabPage.Controls.Add(this.button1);
            this.DownloadTabPage.Controls.Add(this.DownloadMediaButton);
            this.DownloadTabPage.Controls.Add(this.label8);
            this.DownloadTabPage.Controls.Add(this.ConvertFlashTeaseButton);
            this.DownloadTabPage.Controls.Add(this.label3);
            this.DownloadTabPage.Location = new System.Drawing.Point(4, 22);
            this.DownloadTabPage.Name = "DownloadTabPage";
            this.DownloadTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.DownloadTabPage.Size = new System.Drawing.Size(769, 283);
            this.DownloadTabPage.TabIndex = 0;
            this.DownloadTabPage.Text = "Download FlashTease";
            // 
            // TeaseIdTextBox
            // 
            this.TeaseIdTextBox.Location = new System.Drawing.Point(296, 12);
            this.TeaseIdTextBox.Name = "TeaseIdTextBox";
            this.TeaseIdTextBox.Size = new System.Drawing.Size(72, 20);
            this.TeaseIdTextBox.TabIndex = 11;
            // 
            // FlashTeaseScriptTextBox
            // 
            this.FlashTeaseScriptTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FlashTeaseScriptTextBox.Location = new System.Drawing.Point(77, 91);
            this.FlashTeaseScriptTextBox.Multiline = true;
            this.FlashTeaseScriptTextBox.Name = "FlashTeaseScriptTextBox";
            this.FlashTeaseScriptTextBox.ReadOnly = true;
            this.FlashTeaseScriptTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.FlashTeaseScriptTextBox.Size = new System.Drawing.Size(460, 89);
            this.FlashTeaseScriptTextBox.TabIndex = 13;
            this.FlashTeaseScriptTextBox.WordWrap = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Script";
            // 
            // ConvertFlashTeaseButton
            // 
            this.ConvertFlashTeaseButton.Location = new System.Drawing.Point(77, 250);
            this.ConvertFlashTeaseButton.Name = "ConvertFlashTeaseButton";
            this.ConvertFlashTeaseButton.Size = new System.Drawing.Size(106, 23);
            this.ConvertFlashTeaseButton.TabIndex = 15;
            this.ConvertFlashTeaseButton.Text = "Convert";
            this.ConvertFlashTeaseButton.UseVisualStyleBackColor = true;
            this.ConvertFlashTeaseButton.Click += new System.EventHandler(this.ConvertFlashTeaseButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(279, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "http://www.milovana.com/webteases/showflash.php?id=";
            // 
            // DownloadProgressTextBox
            // 
            this.DownloadProgressTextBox.Location = new System.Drawing.Point(543, 39);
            this.DownloadProgressTextBox.Multiline = true;
            this.DownloadProgressTextBox.Name = "DownloadProgressTextBox";
            this.DownloadProgressTextBox.ReadOnly = true;
            this.DownloadProgressTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.DownloadProgressTextBox.Size = new System.Drawing.Size(218, 141);
            this.DownloadProgressTextBox.TabIndex = 19;
            // 
            // DownloadMediaButton
            // 
            this.DownloadMediaButton.Location = new System.Drawing.Point(543, 221);
            this.DownloadMediaButton.Name = "DownloadMediaButton";
            this.DownloadMediaButton.Size = new System.Drawing.Size(106, 23);
            this.DownloadMediaButton.TabIndex = 21;
            this.DownloadMediaButton.Text = "Download media";
            this.DownloadMediaButton.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(655, 221);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 23);
            this.button1.TabIndex = 22;
            this.button1.Text = "Cancel download";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Title";
            // 
            // TeaseTitleTextBox
            // 
            this.TeaseTitleTextBox.Location = new System.Drawing.Point(77, 39);
            this.TeaseTitleTextBox.Name = "TeaseTitleTextBox";
            this.TeaseTitleTextBox.ReadOnly = true;
            this.TeaseTitleTextBox.Size = new System.Drawing.Size(460, 20);
            this.TeaseTitleTextBox.TabIndex = 24;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Author";
            // 
            // AuthorNameTextBox
            // 
            this.AuthorNameTextBox.Location = new System.Drawing.Point(155, 65);
            this.AuthorNameTextBox.Name = "AuthorNameTextBox";
            this.AuthorNameTextBox.ReadOnly = true;
            this.AuthorNameTextBox.Size = new System.Drawing.Size(382, 20);
            this.AuthorNameTextBox.TabIndex = 26;
            // 
            // AuthorIdTextBox
            // 
            this.AuthorIdTextBox.Location = new System.Drawing.Point(77, 65);
            this.AuthorIdTextBox.Name = "AuthorIdTextBox";
            this.AuthorIdTextBox.ReadOnly = true;
            this.AuthorIdTextBox.Size = new System.Drawing.Size(72, 20);
            this.AuthorIdTextBox.TabIndex = 27;
            // 
            // FlashTeaseInspectButton
            // 
            this.FlashTeaseInspectButton.Location = new System.Drawing.Point(374, 10);
            this.FlashTeaseInspectButton.Name = "FlashTeaseInspectButton";
            this.FlashTeaseInspectButton.Size = new System.Drawing.Size(75, 23);
            this.FlashTeaseInspectButton.TabIndex = 30;
            this.FlashTeaseInspectButton.Text = "Inspect";
            this.FlashTeaseInspectButton.UseVisualStyleBackColor = true;
            this.FlashTeaseInspectButton.Click += new System.EventHandler(this.FlashTeaseInspectButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 199);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(223, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Select a directory to download the media files.";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 226);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 13);
            this.label10.TabIndex = 32;
            this.label10.Text = "Directory";
            // 
            // DownloadDirectoryTextBox
            // 
            this.DownloadDirectoryTextBox.Location = new System.Drawing.Point(112, 223);
            this.DownloadDirectoryTextBox.Name = "DownloadDirectoryTextBox";
            this.DownloadDirectoryTextBox.Size = new System.Drawing.Size(425, 20);
            this.DownloadDirectoryTextBox.TabIndex = 33;
            // 
            // SelectDownloadDirectoryButton
            // 
            this.SelectDownloadDirectoryButton.Location = new System.Drawing.Point(77, 221);
            this.SelectDownloadDirectoryButton.Name = "SelectDownloadDirectoryButton";
            this.SelectDownloadDirectoryButton.Size = new System.Drawing.Size(29, 23);
            this.SelectDownloadDirectoryButton.TabIndex = 34;
            this.SelectDownloadDirectoryButton.Text = "...";
            this.SelectDownloadDirectoryButton.UseVisualStyleBackColor = true;
            this.SelectDownloadDirectoryButton.Click += new System.EventHandler(this.SelectDownloadDirectoryButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.DownloadTabPage);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(777, 309);
            this.tabControl1.TabIndex = 1;
            // 
            // ImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 618);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.SaveNewScriptButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PreviewNewScriptTextBox);
            this.Controls.Add(this.label5);
            this.Name = "ImportForm";
            this.Text = "Import";
            this.DownloadTabPage.ResumeLayout(false);
            this.DownloadTabPage.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SaveNewScriptButton;
        private System.Windows.Forms.TextBox PreviewNewScriptTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.OpenFileDialog OpenOldScriptDialog;
        private System.Windows.Forms.SaveFileDialog SaveNewScriptDialog;
        private System.Windows.Forms.FolderBrowserDialog DownloadDirectoryDialog;
        private System.Windows.Forms.TabPage DownloadTabPage;
        private System.Windows.Forms.Button SelectDownloadDirectoryButton;
        private System.Windows.Forms.TextBox DownloadDirectoryTextBox;
        private System.Windows.Forms.TextBox AuthorIdTextBox;
        private System.Windows.Forms.TextBox AuthorNameTextBox;
        private System.Windows.Forms.TextBox TeaseTitleTextBox;
        private System.Windows.Forms.TextBox DownloadProgressTextBox;
        private System.Windows.Forms.TextBox FlashTeaseScriptTextBox;
        private System.Windows.Forms.TextBox TeaseIdTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button FlashTeaseInspectButton;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button DownloadMediaButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button ConvertFlashTeaseButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl1;
    }
}