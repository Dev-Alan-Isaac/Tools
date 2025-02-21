namespace Tools
{
    partial class Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panel_NavBar = new Panel();
            bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(components);
            bunifuDragControl2 = new Bunifu.Framework.UI.BunifuDragControl(components);
            panel_TopBar = new Panel();
            panel_Body = new Panel();
            bunifuDragControl3 = new Bunifu.Framework.UI.BunifuDragControl(components);
            SuspendLayout();
            // 
            // panel_NavBar
            // 
            panel_NavBar.BackColor = Color.FromArgb(7, 85, 112);
            panel_NavBar.Dock = DockStyle.Left;
            panel_NavBar.Location = new Point(0, 0);
            panel_NavBar.Name = "panel_NavBar";
            panel_NavBar.Size = new Size(181, 728);
            panel_NavBar.TabIndex = 0;
            // 
            // bunifuDragControl1
            // 
            bunifuDragControl1.Fixed = true;
            bunifuDragControl1.Horizontal = true;
            bunifuDragControl1.TargetControl = panel_NavBar;
            bunifuDragControl1.Vertical = true;
            // 
            // bunifuDragControl2
            // 
            bunifuDragControl2.Fixed = true;
            bunifuDragControl2.Horizontal = true;
            bunifuDragControl2.TargetControl = panel_TopBar;
            bunifuDragControl2.Vertical = true;
            // 
            // panel_TopBar
            // 
            panel_TopBar.BackColor = Color.FromArgb(7, 85, 112);
            panel_TopBar.Dock = DockStyle.Top;
            panel_TopBar.Location = new Point(181, 0);
            panel_TopBar.Name = "panel_TopBar";
            panel_TopBar.Size = new Size(996, 48);
            panel_TopBar.TabIndex = 1;
            panel_TopBar.Paint += panel_TopBar_Paint;
            // 
            // panel_Body
            // 
            panel_Body.BackColor = Color.WhiteSmoke;
            panel_Body.Dock = DockStyle.Fill;
            panel_Body.Location = new Point(181, 48);
            panel_Body.Name = "panel_Body";
            panel_Body.Size = new Size(996, 680);
            panel_Body.TabIndex = 2;
            // 
            // bunifuDragControl3
            // 
            bunifuDragControl3.Fixed = true;
            bunifuDragControl3.Horizontal = true;
            bunifuDragControl3.TargetControl = panel_Body;
            bunifuDragControl3.Vertical = true;
            // 
            // Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1177, 728);
            Controls.Add(panel_Body);
            Controls.Add(panel_TopBar);
            Controls.Add(panel_NavBar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Panel panel_NavBar;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl2;
        private Panel panel_TopBar;
        private Panel panel_Body;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl3;
    }
}
