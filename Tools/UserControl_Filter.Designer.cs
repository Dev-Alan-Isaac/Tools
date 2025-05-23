﻿namespace Tools
{
    partial class UserControl_Filter
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel_NavBar = new Panel();
            checkBox_scan = new CheckBox();
            checkBox_Duplicates = new CheckBox();
            checkBox_media = new CheckBox();
            checkBox_tags = new CheckBox();
            checkBox_extension = new CheckBox();
            checkBox_name = new CheckBox();
            checkBox_date = new CheckBox();
            checkBox_size = new CheckBox();
            checkBox_type = new CheckBox();
            panel_TopBar = new Panel();
            textBox_Files = new TextBox();
            textBox_Path = new TextBox();
            button_Play = new Button();
            button_Path = new Button();
            panel_Body = new Panel();
            panel2 = new Panel();
            treeView1 = new TreeView();
            panel1 = new Panel();
            progressBar1 = new ProgressBar();
            textBox_Logs = new RichTextBox();
            panel_NavBar.SuspendLayout();
            panel_TopBar.SuspendLayout();
            panel_Body.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel_NavBar
            // 
            panel_NavBar.BackColor = Color.White;
            panel_NavBar.Controls.Add(checkBox_scan);
            panel_NavBar.Controls.Add(checkBox_Duplicates);
            panel_NavBar.Controls.Add(checkBox_media);
            panel_NavBar.Controls.Add(checkBox_tags);
            panel_NavBar.Controls.Add(checkBox_extension);
            panel_NavBar.Controls.Add(checkBox_name);
            panel_NavBar.Controls.Add(checkBox_date);
            panel_NavBar.Controls.Add(checkBox_size);
            panel_NavBar.Controls.Add(checkBox_type);
            panel_NavBar.Dock = DockStyle.Left;
            panel_NavBar.Location = new Point(0, 0);
            panel_NavBar.Name = "panel_NavBar";
            panel_NavBar.Size = new Size(196, 612);
            panel_NavBar.TabIndex = 0;
            // 
            // checkBox_scan
            // 
            checkBox_scan.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            checkBox_scan.AutoSize = true;
            checkBox_scan.Cursor = Cursors.Hand;
            checkBox_scan.Font = new Font("Arial Rounded MT Bold", 12F);
            checkBox_scan.Location = new Point(54, 492);
            checkBox_scan.Name = "checkBox_scan";
            checkBox_scan.Size = new Size(74, 22);
            checkBox_scan.TabIndex = 9;
            checkBox_scan.Text = "SCAN";
            checkBox_scan.UseVisualStyleBackColor = true;
            // 
            // checkBox_Duplicates
            // 
            checkBox_Duplicates.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            checkBox_Duplicates.AutoSize = true;
            checkBox_Duplicates.Cursor = Cursors.Hand;
            checkBox_Duplicates.Font = new Font("Arial Rounded MT Bold", 12F);
            checkBox_Duplicates.Location = new Point(30, 434);
            checkBox_Duplicates.Name = "checkBox_Duplicates";
            checkBox_Duplicates.Size = new Size(122, 22);
            checkBox_Duplicates.TabIndex = 8;
            checkBox_Duplicates.Text = "DUPLICATE";
            checkBox_Duplicates.UseVisualStyleBackColor = true;
            // 
            // checkBox_media
            // 
            checkBox_media.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            checkBox_media.AutoSize = true;
            checkBox_media.Cursor = Cursors.Hand;
            checkBox_media.Font = new Font("Arial Rounded MT Bold", 12F);
            checkBox_media.Location = new Point(51, 376);
            checkBox_media.Name = "checkBox_media";
            checkBox_media.Size = new Size(80, 22);
            checkBox_media.TabIndex = 7;
            checkBox_media.Text = "MEDIA";
            checkBox_media.UseVisualStyleBackColor = true;
            // 
            // checkBox_tags
            // 
            checkBox_tags.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            checkBox_tags.AutoSize = true;
            checkBox_tags.Cursor = Cursors.Hand;
            checkBox_tags.Font = new Font("Arial Rounded MT Bold", 12F);
            checkBox_tags.Location = new Point(16, 318);
            checkBox_tags.Name = "checkBox_tags";
            checkBox_tags.Size = new Size(151, 22);
            checkBox_tags.TabIndex = 6;
            checkBox_tags.Text = "TAGS / LABELS";
            checkBox_tags.UseVisualStyleBackColor = true;
            // 
            // checkBox_extension
            // 
            checkBox_extension.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            checkBox_extension.AutoSize = true;
            checkBox_extension.Cursor = Cursors.Hand;
            checkBox_extension.Font = new Font("Arial Rounded MT Bold", 12F);
            checkBox_extension.Location = new Point(30, 260);
            checkBox_extension.Name = "checkBox_extension";
            checkBox_extension.Size = new Size(122, 22);
            checkBox_extension.TabIndex = 5;
            checkBox_extension.Text = "EXTENSION";
            checkBox_extension.UseVisualStyleBackColor = true;
            // 
            // checkBox_name
            // 
            checkBox_name.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            checkBox_name.AutoSize = true;
            checkBox_name.Cursor = Cursors.Hand;
            checkBox_name.Font = new Font("Arial Rounded MT Bold", 12F);
            checkBox_name.Location = new Point(54, 202);
            checkBox_name.Name = "checkBox_name";
            checkBox_name.Size = new Size(75, 22);
            checkBox_name.TabIndex = 3;
            checkBox_name.Text = "NAME";
            checkBox_name.UseVisualStyleBackColor = true;
            // 
            // checkBox_date
            // 
            checkBox_date.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            checkBox_date.AutoSize = true;
            checkBox_date.Cursor = Cursors.Hand;
            checkBox_date.Font = new Font("Arial Rounded MT Bold", 12F);
            checkBox_date.Location = new Point(55, 144);
            checkBox_date.Name = "checkBox_date";
            checkBox_date.Size = new Size(72, 22);
            checkBox_date.TabIndex = 2;
            checkBox_date.Text = "DATE";
            checkBox_date.UseVisualStyleBackColor = true;
            // 
            // checkBox_size
            // 
            checkBox_size.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            checkBox_size.AutoSize = true;
            checkBox_size.Cursor = Cursors.Hand;
            checkBox_size.Font = new Font("Arial Rounded MT Bold", 12F);
            checkBox_size.Location = new Point(59, 86);
            checkBox_size.Name = "checkBox_size";
            checkBox_size.Size = new Size(64, 22);
            checkBox_size.TabIndex = 1;
            checkBox_size.Text = "SIZE";
            checkBox_size.UseVisualStyleBackColor = true;
            // 
            // checkBox_type
            // 
            checkBox_type.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            checkBox_type.AutoSize = true;
            checkBox_type.Cursor = Cursors.Hand;
            checkBox_type.Font = new Font("Arial Rounded MT Bold", 12F);
            checkBox_type.Location = new Point(57, 28);
            checkBox_type.Name = "checkBox_type";
            checkBox_type.Size = new Size(69, 22);
            checkBox_type.TabIndex = 0;
            checkBox_type.Text = "TYPE";
            checkBox_type.UseVisualStyleBackColor = true;
            // 
            // panel_TopBar
            // 
            panel_TopBar.BackColor = SystemColors.Control;
            panel_TopBar.Controls.Add(textBox_Files);
            panel_TopBar.Controls.Add(textBox_Path);
            panel_TopBar.Controls.Add(button_Play);
            panel_TopBar.Controls.Add(button_Path);
            panel_TopBar.Dock = DockStyle.Top;
            panel_TopBar.Location = new Point(196, 0);
            panel_TopBar.Name = "panel_TopBar";
            panel_TopBar.Size = new Size(622, 50);
            panel_TopBar.TabIndex = 1;
            // 
            // textBox_Files
            // 
            textBox_Files.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBox_Files.BorderStyle = BorderStyle.FixedSingle;
            textBox_Files.Cursor = Cursors.No;
            textBox_Files.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox_Files.Location = new Point(435, 10);
            textBox_Files.Name = "textBox_Files";
            textBox_Files.ReadOnly = true;
            textBox_Files.ScrollBars = ScrollBars.Horizontal;
            textBox_Files.Size = new Size(176, 29);
            textBox_Files.TabIndex = 3;
            // 
            // textBox_Path
            // 
            textBox_Path.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox_Path.BorderStyle = BorderStyle.FixedSingle;
            textBox_Path.Cursor = Cursors.No;
            textBox_Path.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox_Path.Location = new Point(98, 10);
            textBox_Path.Name = "textBox_Path";
            textBox_Path.ReadOnly = true;
            textBox_Path.ScrollBars = ScrollBars.Horizontal;
            textBox_Path.Size = new Size(331, 29);
            textBox_Path.TabIndex = 2;
            // 
            // button_Play
            // 
            button_Play.BackgroundImage = Properties.Resources.icons8_play_50;
            button_Play.BackgroundImageLayout = ImageLayout.Zoom;
            button_Play.Cursor = Cursors.Hand;
            button_Play.FlatAppearance.BorderSize = 0;
            button_Play.Location = new Point(6, 5);
            button_Play.Name = "button_Play";
            button_Play.Size = new Size(40, 40);
            button_Play.TabIndex = 1;
            button_Play.UseVisualStyleBackColor = true;
            button_Play.Click += button_Play_Click;
            // 
            // button_Path
            // 
            button_Path.BackgroundImage = Properties.Resources.icons8_file_50;
            button_Path.BackgroundImageLayout = ImageLayout.Zoom;
            button_Path.Cursor = Cursors.Hand;
            button_Path.FlatAppearance.BorderSize = 0;
            button_Path.Location = new Point(51, 5);
            button_Path.Name = "button_Path";
            button_Path.Size = new Size(40, 40);
            button_Path.TabIndex = 0;
            button_Path.UseVisualStyleBackColor = true;
            button_Path.Click += button_Path_Click;
            // 
            // panel_Body
            // 
            panel_Body.BackColor = SystemColors.Control;
            panel_Body.Controls.Add(textBox_Logs);
            panel_Body.Controls.Add(panel2);
            panel_Body.Controls.Add(panel1);
            panel_Body.Dock = DockStyle.Fill;
            panel_Body.Location = new Point(196, 50);
            panel_Body.Name = "panel_Body";
            panel_Body.Size = new Size(622, 562);
            panel_Body.TabIndex = 2;
            // 
            // panel2
            // 
            panel2.Controls.Add(treeView1);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(409, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(213, 526);
            panel2.TabIndex = 1;
            // 
            // treeView1
            // 
            treeView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            treeView1.Location = new Point(3, 3);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(207, 520);
            treeView1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(progressBar1);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 526);
            panel1.Name = "panel1";
            panel1.Size = new Size(622, 36);
            panel1.TabIndex = 0;
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBar1.Location = new Point(3, 3);
            progressBar1.Maximum = 0;
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(616, 30);
            progressBar1.Step = 1;
            progressBar1.TabIndex = 0;
            // 
            // textBox_Logs
            // 
            textBox_Logs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox_Logs.BorderStyle = BorderStyle.FixedSingle;
            textBox_Logs.Location = new Point(3, 3);
            textBox_Logs.Name = "textBox_Logs";
            textBox_Logs.Size = new Size(403, 520);
            textBox_Logs.TabIndex = 2;
            textBox_Logs.Text = "";
            // 
            // UserControl_Filter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel_Body);
            Controls.Add(panel_TopBar);
            Controls.Add(panel_NavBar);
            Name = "UserControl_Filter";
            Size = new Size(818, 612);
            Load += UserControl_Filter_Load;
            panel_NavBar.ResumeLayout(false);
            panel_NavBar.PerformLayout();
            panel_TopBar.ResumeLayout(false);
            panel_TopBar.PerformLayout();
            panel_Body.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel_NavBar;
        private Panel panel_Body;
        private Panel panel_TopBar;
        private Panel panel1;
        private ProgressBar progressBar1;
        private CheckBox checkBox_tags;
        private CheckBox checkBox_extension;
        private CheckBox checkBox_name;
        private CheckBox checkBox_date;
        private CheckBox checkBox_size;
        private CheckBox checkBox_type;
        private CheckBox checkBox_scan;
        private CheckBox checkBox_Duplicates;
        private CheckBox checkBox_media;
        private Button button_Play;
        private Button button_Path;
        private TextBox textBox_Path;
        private TextBox textBox_Files;
        private Panel panel2;
        private TreeView treeView1;
        private RichTextBox textBox_Logs;
    }
}
