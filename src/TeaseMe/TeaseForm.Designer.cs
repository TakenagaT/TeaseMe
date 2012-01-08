using System.Drawing;
using System.Windows.Forms;

namespace TeaseMe
{
    partial class TeaseForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TeaseForm));
            this.CountdownPanel = new System.Windows.Forms.Panel();
            this.TimeLeftLabel = new System.Windows.Forms.Label();
            this.MinutesLabel = new System.Windows.Forms.Label();
            this.SecondsLabel = new System.Windows.Forms.Label();
            this.ButtonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SampleButton = new System.Windows.Forms.Button();
            this.CountdownTimer = new System.Windows.Forms.Timer(this.components);
            this.MediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.MetronomeTimer = new System.Windows.Forms.Timer(this.components);
            this.TopPanel = new System.Windows.Forms.Panel();
            this.OnlineButton = new System.Windows.Forms.Button();
            this.AboutButton = new System.Windows.Forms.Button();
            this.OpenButton = new System.Windows.Forms.Button();
            this.AuthorNameLabel = new System.Windows.Forms.Label();
            this.TeaseTitleLabel = new System.Windows.Forms.Label();
            this.ToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.HorizontalSplitContainer = new System.Windows.Forms.SplitContainer();
            this.VerticalSplitContainer = new System.Windows.Forms.SplitContainer();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.TextPanel = new System.Windows.Forms.Panel();
            this.TeaseTextBox = new System.Windows.Forms.RichTextBox();
            this.DebugPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.PagesComboBox = new System.Windows.Forms.ComboBox();
            this.PagePropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.CountdownPanel.SuspendLayout();
            this.ButtonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MediaPlayer)).BeginInit();
            this.TopPanel.SuspendLayout();
            this.HorizontalSplitContainer.Panel1.SuspendLayout();
            this.HorizontalSplitContainer.Panel2.SuspendLayout();
            this.HorizontalSplitContainer.SuspendLayout();
            this.VerticalSplitContainer.Panel1.SuspendLayout();
            this.VerticalSplitContainer.Panel2.SuspendLayout();
            this.VerticalSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.TextPanel.SuspendLayout();
            this.DebugPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // CountdownPanel
            // 
            this.CountdownPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CountdownPanel.Controls.Add(this.TimeLeftLabel);
            this.CountdownPanel.Controls.Add(this.MinutesLabel);
            this.CountdownPanel.Controls.Add(this.SecondsLabel);
            this.CountdownPanel.ForeColor = System.Drawing.Color.Gainsboro;
            this.CountdownPanel.Location = new System.Drawing.Point(897, 3);
            this.CountdownPanel.Name = "CountdownPanel";
            this.CountdownPanel.Size = new System.Drawing.Size(146, 59);
            this.CountdownPanel.TabIndex = 45;
            this.CountdownPanel.Visible = false;
            // 
            // TimeLeftLabel
            // 
            this.TimeLeftLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeLeftLabel.BackColor = System.Drawing.Color.Transparent;
            this.TimeLeftLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeLeftLabel.ForeColor = System.Drawing.Color.White;
            this.TimeLeftLabel.Location = new System.Drawing.Point(8, 17);
            this.TimeLeftLabel.Margin = new System.Windows.Forms.Padding(0);
            this.TimeLeftLabel.Name = "TimeLeftLabel";
            this.TimeLeftLabel.Size = new System.Drawing.Size(138, 41);
            this.TimeLeftLabel.TabIndex = 33;
            this.TimeLeftLabel.Text = "00:00";
            this.TimeLeftLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MinutesLabel
            // 
            this.MinutesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MinutesLabel.AutoSize = true;
            this.MinutesLabel.BackColor = System.Drawing.Color.Transparent;
            this.MinutesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinutesLabel.ForeColor = System.Drawing.Color.White;
            this.MinutesLabel.Location = new System.Drawing.Point(54, 3);
            this.MinutesLabel.Name = "MinutesLabel";
            this.MinutesLabel.Size = new System.Drawing.Size(23, 13);
            this.MinutesLabel.TabIndex = 34;
            this.MinutesLabel.Text = "min";
            // 
            // SecondsLabel
            // 
            this.SecondsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SecondsLabel.AutoSize = true;
            this.SecondsLabel.BackColor = System.Drawing.Color.Transparent;
            this.SecondsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SecondsLabel.ForeColor = System.Drawing.Color.White;
            this.SecondsLabel.Location = new System.Drawing.Point(109, 3);
            this.SecondsLabel.Name = "SecondsLabel";
            this.SecondsLabel.Size = new System.Drawing.Size(24, 13);
            this.SecondsLabel.TabIndex = 35;
            this.SecondsLabel.Text = "sec";
            // 
            // ButtonPanel
            // 
            this.ButtonPanel.BackColor = System.Drawing.Color.Black;
            this.ButtonPanel.Controls.Add(this.SampleButton);
            this.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ButtonPanel.Location = new System.Drawing.Point(0, 0);
            this.ButtonPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonPanel.MinimumSize = new System.Drawing.Size(0, 50);
            this.ButtonPanel.Name = "ButtonPanel";
            this.ButtonPanel.Padding = new System.Windows.Forms.Padding(3);
            this.ButtonPanel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ButtonPanel.Size = new System.Drawing.Size(1053, 51);
            this.ButtonPanel.TabIndex = 7;
            // 
            // SampleButton
            // 
            this.SampleButton.BackColor = System.Drawing.Color.White;
            this.SampleButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SampleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SampleButton.Location = new System.Drawing.Point(934, 6);
            this.SampleButton.MaximumSize = new System.Drawing.Size(500, 35);
            this.SampleButton.MinimumSize = new System.Drawing.Size(110, 35);
            this.SampleButton.Name = "SampleButton";
            this.SampleButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SampleButton.Size = new System.Drawing.Size(110, 35);
            this.SampleButton.TabIndex = 0;
            this.SampleButton.Text = "SampleButton";
            this.SampleButton.UseVisualStyleBackColor = false;
            this.SampleButton.Visible = false;
            // 
            // CountdownTimer
            // 
            this.CountdownTimer.Interval = 1000;
            this.CountdownTimer.Tick += new System.EventHandler(this.CountdownTick);
            // 
            // MediaPlayer
            // 
            this.MediaPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MediaPlayer.Enabled = true;
            this.MediaPlayer.Location = new System.Drawing.Point(0, 0);
            this.MediaPlayer.Name = "MediaPlayer";
            this.MediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("MediaPlayer.OcxState")));
            this.MediaPlayer.Size = new System.Drawing.Size(622, 549);
            this.MediaPlayer.TabIndex = 1;
            // 
            // MetronomeTimer
            // 
            this.MetronomeTimer.Interval = 200;
            this.MetronomeTimer.Tick += new System.EventHandler(this.MetronomeTick);
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.Transparent;
            this.TopPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TopPanel.Controls.Add(this.OnlineButton);
            this.TopPanel.Controls.Add(this.AboutButton);
            this.TopPanel.Controls.Add(this.OpenButton);
            this.TopPanel.Controls.Add(this.CountdownPanel);
            this.TopPanel.Controls.Add(this.AuthorNameLabel);
            this.TopPanel.Controls.Add(this.TeaseTitleLabel);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(1053, 71);
            this.TopPanel.TabIndex = 8;
            // 
            // OnlineButton
            // 
            this.OnlineButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.OnlineButton.Image = global::TeaseMe.Properties.Resources.online_48;
            this.OnlineButton.Location = new System.Drawing.Point(131, 6);
            this.OnlineButton.Name = "OnlineButton";
            this.OnlineButton.Size = new System.Drawing.Size(56, 56);
            this.OnlineButton.TabIndex = 51;
            this.ToolTips.SetToolTip(this.OnlineButton, "Tease online");
            this.OnlineButton.UseVisualStyleBackColor = true;
            this.OnlineButton.Click += new System.EventHandler(this.OnlineButton_Click);
            // 
            // AboutButton
            // 
            this.AboutButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AboutButton.Image = global::TeaseMe.Properties.Resources.help_48;
            this.AboutButton.Location = new System.Drawing.Point(69, 6);
            this.AboutButton.Name = "AboutButton";
            this.AboutButton.Size = new System.Drawing.Size(56, 56);
            this.AboutButton.TabIndex = 50;
            this.ToolTips.SetToolTip(this.AboutButton, "About the application");
            this.AboutButton.UseVisualStyleBackColor = true;
            this.AboutButton.Click += new System.EventHandler(this.AboutButton_Click);
            // 
            // OpenButton
            // 
            this.OpenButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.OpenButton.Image = global::TeaseMe.Properties.Resources.open_48;
            this.OpenButton.Location = new System.Drawing.Point(7, 6);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(56, 56);
            this.OpenButton.TabIndex = 47;
            this.ToolTips.SetToolTip(this.OpenButton, "Open a tease");
            this.OpenButton.UseVisualStyleBackColor = true;
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // AuthorNameLabel
            // 
            this.AuthorNameLabel.AutoSize = true;
            this.AuthorNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AuthorNameLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.AuthorNameLabel.Location = new System.Drawing.Point(194, 41);
            this.AuthorNameLabel.Name = "AuthorNameLabel";
            this.AuthorNameLabel.Size = new System.Drawing.Size(91, 16);
            this.AuthorNameLabel.TabIndex = 10;
            this.AuthorNameLabel.Text = "[AuthorName]";
            this.AuthorNameLabel.Click += new System.EventHandler(this.AuthorNameLabel_Click);
            // 
            // TeaseTitleLabel
            // 
            this.TeaseTitleLabel.AutoSize = true;
            this.TeaseTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeaseTitleLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.TeaseTitleLabel.Location = new System.Drawing.Point(193, 8);
            this.TeaseTitleLabel.Name = "TeaseTitleLabel";
            this.TeaseTitleLabel.Size = new System.Drawing.Size(120, 24);
            this.TeaseTitleLabel.TabIndex = 9;
            this.TeaseTitleLabel.Text = "[TeaseTitle]";
            // 
            // HorizontalSplitContainer
            // 
            this.HorizontalSplitContainer.BackColor = System.Drawing.Color.DimGray;
            this.HorizontalSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HorizontalSplitContainer.Location = new System.Drawing.Point(0, 71);
            this.HorizontalSplitContainer.Name = "HorizontalSplitContainer";
            this.HorizontalSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // HorizontalSplitContainer.Panel1
            // 
            this.HorizontalSplitContainer.Panel1.Controls.Add(this.VerticalSplitContainer);
            // 
            // HorizontalSplitContainer.Panel2
            // 
            this.HorizontalSplitContainer.Panel2.Controls.Add(this.ButtonPanel);
            this.HorizontalSplitContainer.Size = new System.Drawing.Size(1053, 601);
            this.HorizontalSplitContainer.SplitterDistance = 549;
            this.HorizontalSplitContainer.SplitterWidth = 1;
            this.HorizontalSplitContainer.TabIndex = 9;
            // 
            // VerticalSplitContainer
            // 
            this.VerticalSplitContainer.BackColor = System.Drawing.Color.DimGray;
            this.VerticalSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VerticalSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.VerticalSplitContainer.Name = "VerticalSplitContainer";
            // 
            // VerticalSplitContainer.Panel1
            // 
            this.VerticalSplitContainer.Panel1.BackColor = System.Drawing.Color.Black;
            this.VerticalSplitContainer.Panel1.Controls.Add(this.PictureBox1);
            this.VerticalSplitContainer.Panel1.Controls.Add(this.MediaPlayer);
            // 
            // VerticalSplitContainer.Panel2
            // 
            this.VerticalSplitContainer.Panel2.BackColor = System.Drawing.Color.Black;
            this.VerticalSplitContainer.Panel2.Controls.Add(this.TextPanel);
            this.VerticalSplitContainer.Panel2.Controls.Add(this.DebugPanel);
            this.VerticalSplitContainer.Size = new System.Drawing.Size(1053, 549);
            this.VerticalSplitContainer.SplitterDistance = 622;
            this.VerticalSplitContainer.SplitterWidth = 1;
            this.VerticalSplitContainer.TabIndex = 0;
            // 
            // PictureBox1
            // 
            this.PictureBox1.BackColor = System.Drawing.Color.Black;
            this.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureBox1.Location = new System.Drawing.Point(0, 0);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(622, 549);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBox1.TabIndex = 0;
            this.PictureBox1.TabStop = false;
            // 
            // TextPanel
            // 
            this.TextPanel.Controls.Add(this.TeaseTextBox);
            this.TextPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextPanel.Location = new System.Drawing.Point(0, 0);
            this.TextPanel.Name = "TextPanel";
            this.TextPanel.Size = new System.Drawing.Size(430, 293);
            this.TextPanel.TabIndex = 2;
            // 
            // TeaseTextBox
            // 
            this.TeaseTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TeaseTextBox.BackColor = System.Drawing.Color.Black;
            this.TeaseTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TeaseTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeaseTextBox.ForeColor = System.Drawing.Color.Gainsboro;
            this.TeaseTextBox.Location = new System.Drawing.Point(5, 6);
            this.TeaseTextBox.Name = "TeaseTextBox";
            this.TeaseTextBox.Size = new System.Drawing.Size(416, 281);
            this.TeaseTextBox.TabIndex = 0;
            this.TeaseTextBox.Text = "[TeaseText]";
            // 
            // DebugPanel
            // 
            this.DebugPanel.Controls.Add(this.label1);
            this.DebugPanel.Controls.Add(this.PagesComboBox);
            this.DebugPanel.Controls.Add(this.PagePropertyGrid);
            this.DebugPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DebugPanel.Location = new System.Drawing.Point(0, 293);
            this.DebugPanel.Name = "DebugPanel";
            this.DebugPanel.Size = new System.Drawing.Size(430, 256);
            this.DebugPanel.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Gainsboro;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Go to page";
            // 
            // PagesComboBox
            // 
            this.PagesComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.PagesComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.PagesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PagesComboBox.FormattingEnabled = true;
            this.PagesComboBox.Location = new System.Drawing.Point(71, 6);
            this.PagesComboBox.Name = "PagesComboBox";
            this.PagesComboBox.Size = new System.Drawing.Size(350, 21);
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
            this.PagePropertyGrid.Size = new System.Drawing.Size(417, 221);
            this.PagePropertyGrid.TabIndex = 1;
            this.PagePropertyGrid.ToolbarVisible = false;
            // 
            // TeaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1053, 672);
            this.Controls.Add(this.HorizontalSplitContainer);
            this.Controls.Add(this.TopPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TeaseForm";
            this.Text = "[ApplicationTitle]";
            this.Load += new System.EventHandler(this.TeaseForm_Load);
            this.CountdownPanel.ResumeLayout(false);
            this.CountdownPanel.PerformLayout();
            this.ButtonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MediaPlayer)).EndInit();
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.HorizontalSplitContainer.Panel1.ResumeLayout(false);
            this.HorizontalSplitContainer.Panel2.ResumeLayout(false);
            this.HorizontalSplitContainer.ResumeLayout(false);
            this.VerticalSplitContainer.Panel1.ResumeLayout(false);
            this.VerticalSplitContainer.Panel2.ResumeLayout(false);
            this.VerticalSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.TextPanel.ResumeLayout(false);
            this.DebugPanel.ResumeLayout(false);
            this.DebugPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label SecondsLabel;
        internal System.Windows.Forms.Label MinutesLabel;
        internal System.Windows.Forms.Label TimeLeftLabel;
        internal System.Windows.Forms.FlowLayoutPanel ButtonPanel;
        internal System.Windows.Forms.Timer CountdownTimer;
        internal System.Windows.Forms.PictureBox PictureBox1;
        internal System.Windows.Forms.Timer MetronomeTimer;
        private System.Windows.Forms.Button SampleButton;
        private Panel CountdownPanel;
        private AxWMPLib.AxWindowsMediaPlayer MediaPlayer;
        private Panel TopPanel;
        private Label TeaseTitleLabel;
        private Label AuthorNameLabel;
        private Button OnlineButton;
        private Button AboutButton;
        private Button OpenButton;
        private ToolTip ToolTips;
        private SplitContainer HorizontalSplitContainer;
        private SplitContainer VerticalSplitContainer;
        private Panel DebugPanel;
        private ComboBox PagesComboBox;
        private PropertyGrid PagePropertyGrid;
        private Panel TextPanel;
        private RichTextBox TeaseTextBox;
        private Label label1;

    }
}