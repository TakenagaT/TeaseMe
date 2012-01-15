namespace TeaseMe
{
    partial class DebugForm
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
            this.DebugPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.PagesComboBox = new System.Windows.Forms.ComboBox();
            this.PagePropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.DebugPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // DebugPanel
            // 
            this.DebugPanel.Controls.Add(this.label1);
            this.DebugPanel.Controls.Add(this.PagesComboBox);
            this.DebugPanel.Controls.Add(this.PagePropertyGrid);
            this.DebugPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DebugPanel.Location = new System.Drawing.Point(0, 0);
            this.DebugPanel.Name = "DebugPanel";
            this.DebugPanel.Size = new System.Drawing.Size(301, 265);
            this.DebugPanel.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Go to page";
            // 
            // PagesComboBox
            // 
            this.PagesComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PagesComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.PagesComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.PagesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PagesComboBox.FormattingEnabled = true;
            this.PagesComboBox.Location = new System.Drawing.Point(71, 6);
            this.PagesComboBox.Name = "PagesComboBox";
            this.PagesComboBox.Size = new System.Drawing.Size(219, 21);
            this.PagesComboBox.TabIndex = 2;
            this.PagesComboBox.SelectedIndexChanged += new System.EventHandler(this.PagesComboBox_SelectedIndexChanged);
            // 
            // PagePropertyGrid
            // 
            this.PagePropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PagePropertyGrid.HelpVisible = false;
            this.PagePropertyGrid.Location = new System.Drawing.Point(4, 32);
            this.PagePropertyGrid.Name = "PagePropertyGrid";
            this.PagePropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.PagePropertyGrid.Size = new System.Drawing.Size(288, 230);
            this.PagePropertyGrid.TabIndex = 1;
            this.PagePropertyGrid.ToolbarVisible = false;
            // 
            // DebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 265);
            this.Controls.Add(this.DebugPanel);
            this.Name = "DebugForm";
            this.Text = "Debug";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DebugForm_FormClosing);
            this.DebugPanel.ResumeLayout(false);
            this.DebugPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel DebugPanel;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox PagesComboBox;
        public System.Windows.Forms.PropertyGrid PagePropertyGrid;
    }
}