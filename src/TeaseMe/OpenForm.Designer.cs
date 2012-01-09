namespace TeaseMe
{
    partial class OpenForm
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
            this.TeaseFolderTextBox = new System.Windows.Forms.TextBox();
            this.TeaseListView = new System.Windows.Forms.ListView();
            this.StartButton = new System.Windows.Forms.Button();
            this.OtherLocationButton = new System.Windows.Forms.Button();
            this.LoadButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ConverionErrorTextBox = new System.Windows.Forms.TextBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.DownloadImagesCheckBox = new System.Windows.Forms.CheckBox();
            this.AuthorIdTextBox = new System.Windows.Forms.TextBox();
            this.AuthorNameTextBox = new System.Windows.Forms.TextBox();
            this.TeaseTitleTextBox = new System.Windows.Forms.TextBox();
            this.TeaseIdTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SaveNewScriptDialog = new System.Windows.Forms.SaveFileDialog();
            this.OpenScriptDialog = new System.Windows.Forms.OpenFileDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current tease folder";
            // 
            // TeaseFolderTextBox
            // 
            this.TeaseFolderTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeaseFolderTextBox.Location = new System.Drawing.Point(111, 24);
            this.TeaseFolderTextBox.Name = "TeaseFolderTextBox";
            this.TeaseFolderTextBox.ReadOnly = true;
            this.TeaseFolderTextBox.Size = new System.Drawing.Size(365, 20);
            this.TeaseFolderTextBox.TabIndex = 1;
            // 
            // TeaseListView
            // 
            this.TeaseListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeaseListView.HideSelection = false;
            this.TeaseListView.Location = new System.Drawing.Point(9, 50);
            this.TeaseListView.MultiSelect = false;
            this.TeaseListView.Name = "TeaseListView";
            this.TeaseListView.Size = new System.Drawing.Size(467, 262);
            this.TeaseListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.TeaseListView.TabIndex = 2;
            this.TeaseListView.UseCompatibleStateImageBehavior = false;
            this.TeaseListView.View = System.Windows.Forms.View.List;
            this.TeaseListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TeaseListView_MouseDoubleClick);
            // 
            // StartButton
            // 
            this.StartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartButton.Location = new System.Drawing.Point(370, 318);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(105, 23);
            this.StartButton.TabIndex = 3;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // OtherLocationButton
            // 
            this.OtherLocationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OtherLocationButton.Location = new System.Drawing.Point(9, 318);
            this.OtherLocationButton.Name = "OtherLocationButton";
            this.OtherLocationButton.Size = new System.Drawing.Size(156, 23);
            this.OtherLocationButton.TabIndex = 4;
            this.OtherLocationButton.Text = "Open from other location ...";
            this.OtherLocationButton.UseVisualStyleBackColor = true;
            this.OtherLocationButton.Click += new System.EventHandler(this.OtherLocationButton_Click);
            // 
            // LoadButton
            // 
            this.LoadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadButton.Location = new System.Drawing.Point(370, 17);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(57, 23);
            this.LoadButton.TabIndex = 5;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TeaseFolderTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.TeaseListView);
            this.groupBox1.Controls.Add(this.OtherLocationButton);
            this.groupBox1.Controls.Add(this.StartButton);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(482, 347);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select local tease";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.ConverionErrorTextBox);
            this.groupBox3.Controls.Add(this.SaveButton);
            this.groupBox3.Controls.Add(this.DownloadImagesCheckBox);
            this.groupBox3.Controls.Add(this.AuthorIdTextBox);
            this.groupBox3.Controls.Add(this.AuthorNameTextBox);
            this.groupBox3.Controls.Add(this.TeaseTitleTextBox);
            this.groupBox3.Controls.Add(this.TeaseIdTextBox);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.LoadButton);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(17, 367);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(482, 256);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Download tease";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(79, 216);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(334, 13);
            this.label3.TabIndex = 39;
            this.label3.Text = "maybe in a next version it will be more smooth with progressbar etc. :-)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 199);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(387, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "When this checkbox is checked, the program seems to hang while downloading,";
            // 
            // ConverionErrorTextBox
            // 
            this.ConverionErrorTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.ConverionErrorTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ConverionErrorTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConverionErrorTextBox.ForeColor = System.Drawing.Color.Red;
            this.ConverionErrorTextBox.Location = new System.Drawing.Point(82, 104);
            this.ConverionErrorTextBox.Multiline = true;
            this.ConverionErrorTextBox.Name = "ConverionErrorTextBox";
            this.ConverionErrorTextBox.Size = new System.Drawing.Size(394, 54);
            this.ConverionErrorTextBox.TabIndex = 36;
            this.ConverionErrorTextBox.Text = "Warning: This tease could not be converted without errors.\r\nOpen the xml-file in " +
    "a text editor after saving.\r\nSearch for the text \'<Errors>\' to try to correct th" +
    "em manually.";
            this.ConverionErrorTextBox.Visible = false;
            // 
            // SaveButton
            // 
            this.SaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.Location = new System.Drawing.Point(371, 164);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(105, 23);
            this.SaveButton.TabIndex = 5;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // DownloadImagesCheckBox
            // 
            this.DownloadImagesCheckBox.AutoSize = true;
            this.DownloadImagesCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DownloadImagesCheckBox.Location = new System.Drawing.Point(82, 168);
            this.DownloadImagesCheckBox.Name = "DownloadImagesCheckBox";
            this.DownloadImagesCheckBox.Size = new System.Drawing.Size(234, 17);
            this.DownloadImagesCheckBox.TabIndex = 35;
            this.DownloadImagesCheckBox.Text = "Download images and sounds for offline use";
            this.DownloadImagesCheckBox.UseVisualStyleBackColor = true;
            // 
            // AuthorIdTextBox
            // 
            this.AuthorIdTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AuthorIdTextBox.Location = new System.Drawing.Point(51, 72);
            this.AuthorIdTextBox.Name = "AuthorIdTextBox";
            this.AuthorIdTextBox.ReadOnly = true;
            this.AuthorIdTextBox.Size = new System.Drawing.Size(25, 20);
            this.AuthorIdTextBox.TabIndex = 34;
            this.AuthorIdTextBox.Visible = false;
            // 
            // AuthorNameTextBox
            // 
            this.AuthorNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AuthorNameTextBox.Location = new System.Drawing.Point(82, 72);
            this.AuthorNameTextBox.Name = "AuthorNameTextBox";
            this.AuthorNameTextBox.ReadOnly = true;
            this.AuthorNameTextBox.Size = new System.Drawing.Size(394, 20);
            this.AuthorNameTextBox.TabIndex = 33;
            // 
            // TeaseTitleTextBox
            // 
            this.TeaseTitleTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeaseTitleTextBox.Location = new System.Drawing.Point(82, 46);
            this.TeaseTitleTextBox.Name = "TeaseTitleTextBox";
            this.TeaseTitleTextBox.ReadOnly = true;
            this.TeaseTitleTextBox.Size = new System.Drawing.Size(394, 20);
            this.TeaseTitleTextBox.TabIndex = 31;
            // 
            // TeaseIdTextBox
            // 
            this.TeaseIdTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeaseIdTextBox.Location = new System.Drawing.Point(292, 19);
            this.TeaseIdTextBox.Name = "TeaseIdTextBox";
            this.TeaseIdTextBox.Size = new System.Drawing.Size(72, 20);
            this.TeaseIdTextBox.TabIndex = 28;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(7, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 32;
            this.label9.Text = "Author";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "Title";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(7, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(279, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "http://www.milovana.com/webteases/showflash.php?id=";
            // 
            // SaveNewScriptDialog
            // 
            this.SaveNewScriptDialog.DefaultExt = "xml";
            this.SaveNewScriptDialog.Filter = "Teases|*.xml|All files|*.*";
            // 
            // OpenScriptDialog
            // 
            this.OpenScriptDialog.FileName = "Select your tease";
            this.OpenScriptDialog.Filter = "Tease Me Script|*.xml";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(79, 235);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(309, 13);
            this.label4.TabIndex = 40;
            this.label4.Text = "A messagebox will appear as soon as the download is complete.";
            // 
            // OpenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 635);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OpenForm";
            this.ShowInTaskbar = false;
            this.Text = "Select your tease";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TeaseFolderTextBox;
        private System.Windows.Forms.ListView TeaseListView;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button OtherLocationButton;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.CheckBox DownloadImagesCheckBox;
        private System.Windows.Forms.TextBox AuthorIdTextBox;
        private System.Windows.Forms.TextBox AuthorNameTextBox;
        private System.Windows.Forms.TextBox TeaseTitleTextBox;
        private System.Windows.Forms.TextBox TeaseIdTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox ConverionErrorTextBox;
        private System.Windows.Forms.SaveFileDialog SaveNewScriptDialog;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.OpenFileDialog OpenScriptDialog;
        private System.Windows.Forms.Label label4;
    }
}