namespace Tools
{
    partial class UserControl_Convert
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
            progressBar1 = new ProgressBar();
            panel_Body = new Panel();
            textBox_Logs = new TextBox();
            panel1 = new Panel();
            button_Path = new Button();
            textBox_Files = new TextBox();
            textBox_Path = new TextBox();
            panel_TopBar = new Panel();
            button_Play = new Button();
            panel_NavBar = new Panel();
            radioButton_Doc = new RadioButton();
            radioButton_Vid = new RadioButton();
            radioButton_Aud = new RadioButton();
            radioButton_Img = new RadioButton();
            panel_Body.SuspendLayout();
            panel1.SuspendLayout();
            panel_TopBar.SuspendLayout();
            panel_NavBar.SuspendLayout();
            SuspendLayout();
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBar1.Location = new Point(3, 3);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(626, 30);
            progressBar1.TabIndex = 0;
            // 
            // panel_Body
            // 
            panel_Body.BackColor = SystemColors.Control;
            panel_Body.Controls.Add(textBox_Logs);
            panel_Body.Controls.Add(panel1);
            panel_Body.Dock = DockStyle.Fill;
            panel_Body.Location = new Point(186, 50);
            panel_Body.Name = "panel_Body";
            panel_Body.Size = new Size(632, 562);
            panel_Body.TabIndex = 5;
            // 
            // textBox_Logs
            // 
            textBox_Logs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox_Logs.BackColor = SystemColors.Window;
            textBox_Logs.Location = new Point(3, 6);
            textBox_Logs.Multiline = true;
            textBox_Logs.Name = "textBox_Logs";
            textBox_Logs.ReadOnly = true;
            textBox_Logs.ScrollBars = ScrollBars.Both;
            textBox_Logs.Size = new Size(626, 514);
            textBox_Logs.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.Controls.Add(progressBar1);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 526);
            panel1.Name = "panel1";
            panel1.Size = new Size(632, 36);
            panel1.TabIndex = 0;
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
            // textBox_Files
            // 
            textBox_Files.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBox_Files.BorderStyle = BorderStyle.FixedSingle;
            textBox_Files.Cursor = Cursors.No;
            textBox_Files.Font = new Font("Arial Rounded MT Bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox_Files.Location = new Point(445, 10);
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
            textBox_Path.Font = new Font("Arial Rounded MT Bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox_Path.Location = new Point(98, 10);
            textBox_Path.Name = "textBox_Path";
            textBox_Path.ReadOnly = true;
            textBox_Path.ScrollBars = ScrollBars.Horizontal;
            textBox_Path.Size = new Size(341, 29);
            textBox_Path.TabIndex = 2;
            // 
            // panel_TopBar
            // 
            panel_TopBar.BackColor = SystemColors.Control;
            panel_TopBar.Controls.Add(textBox_Files);
            panel_TopBar.Controls.Add(textBox_Path);
            panel_TopBar.Controls.Add(button_Play);
            panel_TopBar.Controls.Add(button_Path);
            panel_TopBar.Dock = DockStyle.Top;
            panel_TopBar.Location = new Point(186, 0);
            panel_TopBar.Name = "panel_TopBar";
            panel_TopBar.Size = new Size(632, 50);
            panel_TopBar.TabIndex = 4;
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
            // panel_NavBar
            // 
            panel_NavBar.BackColor = Color.White;
            panel_NavBar.Controls.Add(radioButton_Doc);
            panel_NavBar.Controls.Add(radioButton_Vid);
            panel_NavBar.Controls.Add(radioButton_Aud);
            panel_NavBar.Controls.Add(radioButton_Img);
            panel_NavBar.Dock = DockStyle.Left;
            panel_NavBar.Location = new Point(0, 0);
            panel_NavBar.Name = "panel_NavBar";
            panel_NavBar.Size = new Size(186, 612);
            panel_NavBar.TabIndex = 3;
            // 
            // radioButton_Doc
            // 
            radioButton_Doc.AutoSize = true;
            radioButton_Doc.Font = new Font("Arial Rounded MT Bold", 12F);
            radioButton_Doc.Location = new Point(28, 184);
            radioButton_Doc.Name = "radioButton_Doc";
            radioButton_Doc.Size = new Size(108, 22);
            radioButton_Doc.TabIndex = 3;
            radioButton_Doc.TabStop = true;
            radioButton_Doc.Text = "Document";
            radioButton_Doc.UseVisualStyleBackColor = true;
            // 
            // radioButton_Vid
            // 
            radioButton_Vid.AutoSize = true;
            radioButton_Vid.Font = new Font("Arial Rounded MT Bold", 12F);
            radioButton_Vid.Location = new Point(28, 132);
            radioButton_Vid.Name = "radioButton_Vid";
            radioButton_Vid.Size = new Size(71, 22);
            radioButton_Vid.TabIndex = 2;
            radioButton_Vid.TabStop = true;
            radioButton_Vid.Text = "Video";
            radioButton_Vid.UseVisualStyleBackColor = true;
            // 
            // radioButton_Aud
            // 
            radioButton_Aud.AutoSize = true;
            radioButton_Aud.Font = new Font("Arial Rounded MT Bold", 12F);
            radioButton_Aud.Location = new Point(28, 80);
            radioButton_Aud.Name = "radioButton_Aud";
            radioButton_Aud.Size = new Size(72, 22);
            radioButton_Aud.TabIndex = 1;
            radioButton_Aud.TabStop = true;
            radioButton_Aud.Text = "Audio";
            radioButton_Aud.UseVisualStyleBackColor = true;
            // 
            // radioButton_Img
            // 
            radioButton_Img.AutoSize = true;
            radioButton_Img.Font = new Font("Arial Rounded MT Bold", 12F);
            radioButton_Img.Location = new Point(28, 28);
            radioButton_Img.Name = "radioButton_Img";
            radioButton_Img.Size = new Size(85, 22);
            radioButton_Img.TabIndex = 0;
            radioButton_Img.TabStop = true;
            radioButton_Img.Text = "Imagen";
            radioButton_Img.UseVisualStyleBackColor = true;
            // 
            // UserControl_Convert
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel_Body);
            Controls.Add(panel_TopBar);
            Controls.Add(panel_NavBar);
            Name = "UserControl_Convert";
            Size = new Size(818, 612);
            Load += UserControl_Convert_Load;
            panel_Body.ResumeLayout(false);
            panel_Body.PerformLayout();
            panel1.ResumeLayout(false);
            panel_TopBar.ResumeLayout(false);
            panel_TopBar.PerformLayout();
            panel_NavBar.ResumeLayout(false);
            panel_NavBar.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ProgressBar progressBar1;
        private Panel panel_Body;
        private Panel panel1;
        private Button button_Path;
        private TextBox textBox_Files;
        private TextBox textBox_Path;
        private Panel panel_TopBar;
        private Button button_Play;
        private Panel panel_NavBar;
        private RadioButton radioButton_Doc;
        private RadioButton radioButton_Vid;
        private RadioButton radioButton_Aud;
        private RadioButton radioButton_Img;
        private TextBox textBox_Logs;
    }
}
