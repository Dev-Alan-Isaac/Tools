namespace Tools
{
    partial class UserControl_Configuration_Extract
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
            radioButton_Text = new RadioButton();
            radioButton_Window = new RadioButton();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(checkBox_Extract_Folder);
            groupBox1.Controls.Add(checkBox_Extract_Subfolder);
            groupBox1.Controls.Add(checkBox_Extract_Delete);
            groupBox1.Font = new Font("Arial Rounded MT Bold", 12F);
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(491, 124);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Additional";
            // 
            // checkBox_Extract_Folder
            // 
            checkBox_Extract_Folder.AutoSize = true;
            checkBox_Extract_Folder.Location = new Point(6, 93);
            checkBox_Extract_Folder.Name = "checkBox_Extract_Folder";
            checkBox_Extract_Folder.Size = new Size(146, 22);
            checkBox_Extract_Folder.TabIndex = 2;
            checkBox_Extract_Folder.Text = "Create a folder";
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
            groupBox2.Controls.Add(radioButton_Text);
            groupBox2.Controls.Add(radioButton_Window);
            groupBox2.Font = new Font("Arial Rounded MT Bold", 12F);
            groupBox2.Location = new Point(500, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(315, 124);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Metadata";
            // 
            // radioButton_Text
            // 
            radioButton_Text.AutoSize = true;
            radioButton_Text.Location = new Point(6, 61);
            radioButton_Text.Name = "radioButton_Text";
            radioButton_Text.Size = new Size(92, 22);
            radioButton_Text.TabIndex = 1;
            radioButton_Text.TabStop = true;
            radioButton_Text.Text = "Text File";
            radioButton_Text.UseVisualStyleBackColor = true;
            // 
            // radioButton_Window
            // 
            radioButton_Window.AutoSize = true;
            radioButton_Window.Location = new Point(6, 25);
            radioButton_Window.Name = "radioButton_Window";
            radioButton_Window.Size = new Size(88, 22);
            radioButton_Window.TabIndex = 0;
            radioButton_Window.TabStop = true;
            radioButton_Window.Text = "Window";
            radioButton_Window.UseVisualStyleBackColor = true;
            // 
            // UserControl_Configuration_Extract
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            Name = "UserControl_Configuration_Extract";
            Size = new Size(818, 612);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private CheckBox checkBox_Extract_Subfolder;
        private CheckBox checkBox_Extract_Delete;
        private CheckBox checkBox_Extract_Folder;
        private GroupBox groupBox2;
        private RadioButton radioButton_Text;
        private RadioButton radioButton_Window;
    }
}
