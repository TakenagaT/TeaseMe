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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.milovanaDownloadControl = new TeaseMe.MilovanaDownload.MilovanaDownloadControl();
            this.OpenScriptDialog = new System.Windows.Forms.OpenFileDialog();
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
            this.TeaseFolderTextBox.Size = new System.Drawing.Size(379, 20);
            this.TeaseFolderTextBox.TabIndex = 1;
            // 
            // TeaseListView
            // 
            this.TeaseListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeaseListView.HideSelection = false;
            this.TeaseListView.Location = new System.Drawing.Point(9, 50);
            this.TeaseListView.MultiSelect = false;
            this.TeaseListView.Name = "TeaseListView";
            this.TeaseListView.Size = new System.Drawing.Size(481, 262);
            this.TeaseListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.TeaseListView.TabIndex = 2;
            this.TeaseListView.UseCompatibleStateImageBehavior = false;
            this.TeaseListView.View = System.Windows.Forms.View.List;
            this.TeaseListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TeaseListView_MouseDoubleClick);
            // 
            // StartButton
            // 
            this.StartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartButton.Location = new System.Drawing.Point(390, 318);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(100, 23);
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
            this.groupBox1.Size = new System.Drawing.Size(501, 347);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select local tease";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.milovanaDownloadControl);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 367);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(502, 308);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Download Milovana Tease";
            // 
            // milovanaDownloadControl
            // 
            this.milovanaDownloadControl.Location = new System.Drawing.Point(6, 16);
            this.milovanaDownloadControl.Name = "milovanaDownloadControl";
            this.milovanaDownloadControl.Size = new System.Drawing.Size(485, 284);
            this.milovanaDownloadControl.TabIndex = 10;
            this.milovanaDownloadControl.TeasesFolder = null;
            // 
            // OpenScriptDialog
            // 
            this.OpenScriptDialog.FileName = "Select your tease";
            this.OpenScriptDialog.Filter = "Tease Me Script|*.xml";
            // 
            // OpenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 689);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TeaseFolderTextBox;
        private System.Windows.Forms.ListView TeaseListView;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button OtherLocationButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.OpenFileDialog OpenScriptDialog;
        private MilovanaDownload.MilovanaDownloadControl milovanaDownloadControl;
    }
}