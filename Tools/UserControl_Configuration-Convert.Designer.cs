namespace Tools
{
    partial class UserControl_Configuration_Convert
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
            groupBox1 = new GroupBox();
            checkBox_Extract_Folder = new CheckBox();
            checkBox_Extract_Subfolder = new CheckBox();
            checkBox_Extract_Delete = new CheckBox();
            groupBox2 = new GroupBox();
            label_Opt_Img = new Label();
            label6 = new Label();
            label5 = new Label();
            label1 = new Label();
            comboBox_img = new ComboBox();
            groupBox3 = new GroupBox();
            label_Opt_Audio = new Label();
            label2 = new Label();
            label12 = new Label();
            label13 = new Label();
            comboBox_Aud = new ComboBox();
            groupBox4 = new GroupBox();
            label_Opt_Vid = new Label();
            label9 = new Label();
            label10 = new Label();
            label4 = new Label();
            comboBox_Vid = new ComboBox();
            groupBox5 = new GroupBox();
            label_Opt_Doc = new Label();
            label3 = new Label();
            label15 = new Label();
            label16 = new Label();
            comboBox_Doc = new ComboBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(checkBox_Extract_Folder);
            groupBox1.Controls.Add(checkBox_Extract_Subfolder);
            groupBox1.Controls.Add(checkBox_Extract_Delete);
            groupBox1.Font = new Font("Arial Rounded MT Bold", 12F);
            groupBox1.Location = new Point(9, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(800, 124);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Additional";
            // 
            // checkBox_Extract_Folder
            // 
            checkBox_Extract_Folder.AutoSize = true;
            checkBox_Extract_Folder.Location = new Point(6, 93);
            checkBox_Extract_Folder.Name = "checkBox_Extract_Folder";
            checkBox_Extract_Folder.Size = new Size(159, 22);
            checkBox_Extract_Folder.TabIndex = 2;
            checkBox_Extract_Folder.Text = "Keep original file";
            checkBox_Extract_Folder.UseVisualStyleBackColor = true;
            // 
            // checkBox_Extract_Subfolder
            // 
            checkBox_Extract_Subfolder.AutoSize = true;
            checkBox_Extract_Subfolder.Location = new Point(6, 59);
            checkBox_Extract_Subfolder.Name = "checkBox_Extract_Subfolder";
            checkBox_Extract_Subfolder.Size = new Size(158, 22);
            checkBox_Extract_Subfolder.TabIndex = 1;
            checkBox_Extract_Subfolder.Text = "Process subfiles";
            checkBox_Extract_Subfolder.UseVisualStyleBackColor = true;
            // 
            // checkBox_Extract_Delete
            // 
            checkBox_Extract_Delete.AutoSize = true;
            checkBox_Extract_Delete.Location = new Point(6, 25);
            checkBox_Extract_Delete.Name = "checkBox_Extract_Delete";
            checkBox_Extract_Delete.Size = new Size(191, 22);
            checkBox_Extract_Delete.TabIndex = 0;
            checkBox_Extract_Delete.Text = "Delete empty folders";
            checkBox_Extract_Delete.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label_Opt_Img);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(comboBox_img);
            groupBox2.Font = new Font("Arial Rounded MT Bold", 12F);
            groupBox2.Location = new Point(9, 133);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(396, 169);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Image";
            // 
            // label_Opt_Img
            // 
            label_Opt_Img.AutoSize = true;
            label_Opt_Img.Location = new Point(209, 118);
            label_Opt_Img.Name = "label_Opt_Img";
            label_Opt_Img.Size = new Size(57, 18);
            label_Opt_Img.TabIndex = 4;
            label_Opt_Img.Text = "Image";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Arial Rounded MT Bold", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(155, 107);
            label6.Name = "label6";
            label6.Size = new Size(42, 37);
            label6.TabIndex = 3;
            label6.Text = "➜";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(92, 118);
            label5.Name = "label5";
            label5.Size = new Size(57, 18);
            label5.TabIndex = 2;
            label5.Text = "Image";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 67);
            label1.Name = "label1";
            label1.Size = new Size(228, 18);
            label1.TabIndex = 1;
            label1.Text = "Previously Selected Option:";
            // 
            // comboBox_img
            // 
            comboBox_img.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBox_img.FormattingEnabled = true;
            comboBox_img.Items.AddRange(new object[] { "BMP", "JPEG", "PNG", "TIFF", "ICO", "DOCX", "PDF" });
            comboBox_img.Location = new Point(6, 25);
            comboBox_img.Name = "comboBox_img";
            comboBox_img.Size = new Size(384, 26);
            comboBox_img.TabIndex = 0;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox3.Controls.Add(label_Opt_Audio);
            groupBox3.Controls.Add(label2);
            groupBox3.Controls.Add(label12);
            groupBox3.Controls.Add(label13);
            groupBox3.Controls.Add(comboBox_Aud);
            groupBox3.Font = new Font("Arial Rounded MT Bold", 12F);
            groupBox3.Location = new Point(413, 133);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(396, 169);
            groupBox3.TabIndex = 4;
            groupBox3.TabStop = false;
            groupBox3.Text = "Audio";
            // 
            // label_Opt_Audio
            // 
            label_Opt_Audio.AutoSize = true;
            label_Opt_Audio.Location = new Point(230, 118);
            label_Opt_Audio.Name = "label_Opt_Audio";
            label_Opt_Audio.Size = new Size(57, 18);
            label_Opt_Audio.TabIndex = 10;
            label_Opt_Audio.Text = "Image";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 67);
            label2.Name = "label2";
            label2.Size = new Size(228, 18);
            label2.TabIndex = 2;
            label2.Text = "Previously Selected Option:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Arial Rounded MT Bold", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label12.Location = new Point(182, 107);
            label12.Name = "label12";
            label12.Size = new Size(42, 37);
            label12.TabIndex = 9;
            label12.Text = "➜";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(119, 116);
            label13.Name = "label13";
            label13.Size = new Size(54, 18);
            label13.TabIndex = 8;
            label13.Text = "Audio";
            // 
            // comboBox_Aud
            // 
            comboBox_Aud.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBox_Aud.FormattingEnabled = true;
            comboBox_Aud.Items.AddRange(new object[] { "MP3", "WAV" });
            comboBox_Aud.Location = new Point(6, 25);
            comboBox_Aud.Name = "comboBox_Aud";
            comboBox_Aud.Size = new Size(384, 26);
            comboBox_Aud.TabIndex = 1;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(label_Opt_Vid);
            groupBox4.Controls.Add(label9);
            groupBox4.Controls.Add(label10);
            groupBox4.Controls.Add(label4);
            groupBox4.Controls.Add(comboBox_Vid);
            groupBox4.Font = new Font("Arial Rounded MT Bold", 12F);
            groupBox4.Location = new Point(9, 335);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(396, 169);
            groupBox4.TabIndex = 4;
            groupBox4.TabStop = false;
            groupBox4.Text = "Video";
            // 
            // label_Opt_Vid
            // 
            label_Opt_Vid.AutoSize = true;
            label_Opt_Vid.Location = new Point(209, 117);
            label_Opt_Vid.Name = "label_Opt_Vid";
            label_Opt_Vid.Size = new Size(57, 18);
            label_Opt_Vid.TabIndex = 7;
            label_Opt_Vid.Text = "Image";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Arial Rounded MT Bold", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(155, 106);
            label9.Name = "label9";
            label9.Size = new Size(42, 37);
            label9.TabIndex = 6;
            label9.Text = "➜";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(95, 117);
            label10.Name = "label10";
            label10.Size = new Size(53, 18);
            label10.TabIndex = 5;
            label10.Text = "Video";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 73);
            label4.Name = "label4";
            label4.Size = new Size(228, 18);
            label4.TabIndex = 2;
            label4.Text = "Previously Selected Option:";
            // 
            // comboBox_Vid
            // 
            comboBox_Vid.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBox_Vid.FormattingEnabled = true;
            comboBox_Vid.Items.AddRange(new object[] { "MP4", "WEBM", "AVI", "WAV", "GIF", "MP3" });
            comboBox_Vid.Location = new Point(6, 25);
            comboBox_Vid.Name = "comboBox_Vid";
            comboBox_Vid.Size = new Size(384, 26);
            comboBox_Vid.TabIndex = 3;
            // 
            // groupBox5
            // 
            groupBox5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox5.Controls.Add(label_Opt_Doc);
            groupBox5.Controls.Add(label3);
            groupBox5.Controls.Add(label15);
            groupBox5.Controls.Add(label16);
            groupBox5.Controls.Add(comboBox_Doc);
            groupBox5.Font = new Font("Arial Rounded MT Bold", 12F);
            groupBox5.Location = new Point(413, 335);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(396, 169);
            groupBox5.TabIndex = 5;
            groupBox5.TabStop = false;
            groupBox5.Text = "Document";
            // 
            // label_Opt_Doc
            // 
            label_Opt_Doc.AutoSize = true;
            label_Opt_Doc.Location = new Point(230, 116);
            label_Opt_Doc.Name = "label_Opt_Doc";
            label_Opt_Doc.Size = new Size(57, 18);
            label_Opt_Doc.TabIndex = 13;
            label_Opt_Doc.Text = "Image";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 73);
            label3.Name = "label3";
            label3.Size = new Size(228, 18);
            label3.TabIndex = 3;
            label3.Text = "Previously Selected Option:";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Arial Rounded MT Bold", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label15.Location = new Point(182, 106);
            label15.Name = "label15";
            label15.Size = new Size(42, 37);
            label15.TabIndex = 12;
            label15.Text = "➜";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(88, 115);
            label16.Name = "label16";
            label16.Size = new Size(90, 18);
            label16.TabIndex = 11;
            label16.Text = "Document";
            // 
            // comboBox_Doc
            // 
            comboBox_Doc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBox_Doc.FormattingEnabled = true;
            comboBox_Doc.Items.AddRange(new object[] { "DOC", "DOCX", "PDF", "TXT" });
            comboBox_Doc.Location = new Point(6, 25);
            comboBox_Doc.Name = "comboBox_Doc";
            comboBox_Doc.Size = new Size(384, 26);
            comboBox_Doc.TabIndex = 2;
            // 
            // UserControl_Configuration_Convert
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "UserControl_Configuration_Convert";
            Size = new Size(818, 612);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private CheckBox checkBox_Extract_Folder;
        private CheckBox checkBox_Extract_Subfolder;
        private CheckBox checkBox_Extract_Delete;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private ComboBox comboBox_img;
        private ComboBox comboBox_Aud;
        private ComboBox comboBox_Vid;
        private ComboBox comboBox_Doc;
        private Label label1;
        private Label label2;
        private Label label4;
        private Label label3;
        private Label label6;
        private Label label5;
        private Label label_Opt_Img;
        private Label label_Opt_Audio;
        private Label label12;
        private Label label13;
        private Label label_Opt_Vid;
        private Label label9;
        private Label label10;
        private Label label_Opt_Doc;
        private Label label15;
        private Label label16;
    }
}
