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
            treeView1 = new TreeView();
            panel2 = new Panel();
            panel_Body = new Panel();
            panel1 = new Panel();
            button_Path = new Button();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            panel_TopBar = new Panel();
            button_Play = new Button();
            panel_NavBar = new Panel();
            radioButton4 = new RadioButton();
            radioButton3 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            panel2.SuspendLayout();
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
            // treeView1
            // 
            treeView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            treeView1.Location = new Point(0, 0);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(280, 526);
            treeView1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(treeView1);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(349, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(283, 526);
            panel2.TabIndex = 1;
            // 
            // panel_Body
            // 
            panel_Body.BackColor = SystemColors.Control;
            panel_Body.Controls.Add(panel2);
            panel_Body.Controls.Add(panel1);
            panel_Body.Dock = DockStyle.Fill;
            panel_Body.Location = new Point(186, 50);
            panel_Body.Name = "panel_Body";
            panel_Body.Size = new Size(632, 562);
            panel_Body.TabIndex = 5;
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
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.Cursor = Cursors.No;
            textBox2.Font = new Font("Arial Rounded MT Bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox2.Location = new Point(445, 10);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.ScrollBars = ScrollBars.Horizontal;
            textBox2.Size = new Size(176, 29);
            textBox2.TabIndex = 3;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Cursor = Cursors.No;
            textBox1.Font = new Font("Arial Rounded MT Bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(98, 10);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.ScrollBars = ScrollBars.Horizontal;
            textBox1.Size = new Size(341, 29);
            textBox1.TabIndex = 2;
            // 
            // panel_TopBar
            // 
            panel_TopBar.BackColor = SystemColors.Control;
            panel_TopBar.Controls.Add(textBox2);
            panel_TopBar.Controls.Add(textBox1);
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
            // 
            // panel_NavBar
            // 
            panel_NavBar.BackColor = Color.White;
            panel_NavBar.Controls.Add(radioButton4);
            panel_NavBar.Controls.Add(radioButton3);
            panel_NavBar.Controls.Add(radioButton2);
            panel_NavBar.Controls.Add(radioButton1);
            panel_NavBar.Dock = DockStyle.Left;
            panel_NavBar.Location = new Point(0, 0);
            panel_NavBar.Name = "panel_NavBar";
            panel_NavBar.Size = new Size(186, 612);
            panel_NavBar.TabIndex = 3;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Font = new Font("Arial Rounded MT Bold", 12F);
            radioButton4.Location = new Point(28, 184);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(131, 22);
            radioButton4.TabIndex = 3;
            radioButton4.TabStop = true;
            radioButton4.Text = "radioButton4";
            radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Font = new Font("Arial Rounded MT Bold", 12F);
            radioButton3.Location = new Point(28, 132);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(131, 22);
            radioButton3.TabIndex = 2;
            radioButton3.TabStop = true;
            radioButton3.Text = "radioButton3";
            radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Font = new Font("Arial Rounded MT Bold", 12F);
            radioButton2.Location = new Point(28, 80);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(131, 22);
            radioButton2.TabIndex = 1;
            radioButton2.TabStop = true;
            radioButton2.Text = "radioButton2";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Font = new Font("Arial Rounded MT Bold", 12F);
            radioButton1.Location = new Point(28, 28);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(131, 22);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "radioButton1";
            radioButton1.UseVisualStyleBackColor = true;
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
            panel2.ResumeLayout(false);
            panel_Body.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel_TopBar.ResumeLayout(false);
            panel_TopBar.PerformLayout();
            panel_NavBar.ResumeLayout(false);
            panel_NavBar.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ProgressBar progressBar1;
        private TreeView treeView1;
        private Panel panel2;
        private Panel panel_Body;
        private Panel panel1;
        private Button button_Path;
        private TextBox textBox2;
        private TextBox textBox1;
        private Panel panel_TopBar;
        private Button button_Play;
        private Panel panel_NavBar;
        private RadioButton radioButton4;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
    }
}
