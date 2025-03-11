namespace Tools
{
    partial class UserControl_Configuration_Filter
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
            components = new System.ComponentModel.Container();
            toolTip1 = new ToolTip(components);
            groupBox1 = new GroupBox();
            checkBox_Type_Executable = new CheckBox();
            groupBox5 = new GroupBox();
            treeView_Type = new TreeView();
            button_RemoveGroup = new Button();
            button_AddGroup = new Button();
            checkBox_Type_Other = new CheckBox();
            checkBox_Type_Archive = new CheckBox();
            checkBox_Type_Video = new CheckBox();
            checkBox_Type_Audio = new CheckBox();
            checkBox_Type_Image = new CheckBox();
            checkBox_Type_Document = new CheckBox();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            treeView_Tags = new TreeView();
            button_RemoveTags = new Button();
            button_AddTags = new Button();
            groupBox4 = new GroupBox();
            checkBox_Additional_subfiles = new CheckBox();
            checkBox_Additional_Delete = new CheckBox();
            groupBox6 = new GroupBox();
            radioButton_Size_Dynamic = new RadioButton();
            radioButton_Size_Range = new RadioButton();
            groupBox7 = new GroupBox();
            radioButton_Media_AspectRatio = new RadioButton();
            radioButton_Media_Codec = new RadioButton();
            radioButton_Media_FrameRate = new RadioButton();
            radioButton_Media_Resolution = new RadioButton();
            radioButton_Media_Duration = new RadioButton();
            groupBox8 = new GroupBox();
            radioButton_Date_Modify = new RadioButton();
            radioButton_Date_Access = new RadioButton();
            radioButton_Date_Creation = new RadioButton();
            groupBox9 = new GroupBox();
            checkBox_Name_Characters = new CheckBox();
            checkBox_Name_Caps = new CheckBox();
            groupBox1.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox8.SuspendLayout();
            groupBox9.SuspendLayout();
            SuspendLayout();
            // 
            // toolTip1
            // 
            toolTip1.ToolTipIcon = ToolTipIcon.Info;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(checkBox_Type_Executable);
            groupBox1.Controls.Add(groupBox5);
            groupBox1.Controls.Add(button_RemoveGroup);
            groupBox1.Controls.Add(button_AddGroup);
            groupBox1.Controls.Add(checkBox_Type_Other);
            groupBox1.Controls.Add(checkBox_Type_Archive);
            groupBox1.Controls.Add(checkBox_Type_Video);
            groupBox1.Controls.Add(checkBox_Type_Audio);
            groupBox1.Controls.Add(checkBox_Type_Image);
            groupBox1.Controls.Add(checkBox_Type_Document);
            groupBox1.Font = new Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(418, 400);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Type";
            // 
            // checkBox_Type_Executable
            // 
            checkBox_Type_Executable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            checkBox_Type_Executable.AutoSize = true;
            checkBox_Type_Executable.Location = new Point(6, 252);
            checkBox_Type_Executable.Name = "checkBox_Type_Executable";
            checkBox_Type_Executable.Size = new Size(116, 22);
            checkBox_Type_Executable.TabIndex = 10;
            checkBox_Type_Executable.Text = "Executable";
            checkBox_Type_Executable.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            groupBox5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox5.Controls.Add(treeView_Type);
            groupBox5.FlatStyle = FlatStyle.Flat;
            groupBox5.Location = new Point(128, 25);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(284, 312);
            groupBox5.TabIndex = 9;
            groupBox5.TabStop = false;
            groupBox5.Text = "Groups / Extensions";
            // 
            // treeView_Type
            // 
            treeView_Type.Dock = DockStyle.Fill;
            treeView_Type.Location = new Point(3, 22);
            treeView_Type.Name = "treeView_Type";
            treeView_Type.Size = new Size(278, 287);
            treeView_Type.TabIndex = 0;
            // 
            // button_RemoveGroup
            // 
            button_RemoveGroup.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            button_RemoveGroup.BackgroundImage = Properties.Resources.icons8_remove_50;
            button_RemoveGroup.BackgroundImageLayout = ImageLayout.Center;
            button_RemoveGroup.Cursor = Cursors.Hand;
            button_RemoveGroup.Location = new Point(292, 340);
            button_RemoveGroup.Name = "button_RemoveGroup";
            button_RemoveGroup.Size = new Size(120, 54);
            button_RemoveGroup.TabIndex = 8;
            button_RemoveGroup.UseVisualStyleBackColor = true;
            button_RemoveGroup.Click += button_RemoveGroup_Click;
            // 
            // button_AddGroup
            // 
            button_AddGroup.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            button_AddGroup.BackgroundImage = Properties.Resources.icons8_add_50;
            button_AddGroup.BackgroundImageLayout = ImageLayout.Center;
            button_AddGroup.Cursor = Cursors.Hand;
            button_AddGroup.Location = new Point(6, 340);
            button_AddGroup.Name = "button_AddGroup";
            button_AddGroup.Size = new Size(120, 54);
            button_AddGroup.TabIndex = 7;
            button_AddGroup.UseVisualStyleBackColor = true;
            button_AddGroup.Click += button_AddGroup_Click;
            // 
            // checkBox_Type_Other
            // 
            checkBox_Type_Other.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            checkBox_Type_Other.AutoSize = true;
            checkBox_Type_Other.Location = new Point(6, 293);
            checkBox_Type_Other.Name = "checkBox_Type_Other";
            checkBox_Type_Other.Size = new Size(73, 22);
            checkBox_Type_Other.TabIndex = 6;
            checkBox_Type_Other.Text = "Other";
            checkBox_Type_Other.UseVisualStyleBackColor = true;
            // 
            // checkBox_Type_Archive
            // 
            checkBox_Type_Archive.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            checkBox_Type_Archive.AutoSize = true;
            checkBox_Type_Archive.Location = new Point(6, 211);
            checkBox_Type_Archive.Name = "checkBox_Type_Archive";
            checkBox_Type_Archive.Size = new Size(89, 22);
            checkBox_Type_Archive.TabIndex = 5;
            checkBox_Type_Archive.Text = "Archive";
            checkBox_Type_Archive.UseVisualStyleBackColor = true;
            // 
            // checkBox_Type_Video
            // 
            checkBox_Type_Video.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            checkBox_Type_Video.AutoSize = true;
            checkBox_Type_Video.Location = new Point(6, 170);
            checkBox_Type_Video.Name = "checkBox_Type_Video";
            checkBox_Type_Video.Size = new Size(72, 22);
            checkBox_Type_Video.TabIndex = 4;
            checkBox_Type_Video.Text = "Video";
            checkBox_Type_Video.UseVisualStyleBackColor = true;
            // 
            // checkBox_Type_Audio
            // 
            checkBox_Type_Audio.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            checkBox_Type_Audio.AutoSize = true;
            checkBox_Type_Audio.Location = new Point(6, 129);
            checkBox_Type_Audio.Name = "checkBox_Type_Audio";
            checkBox_Type_Audio.Size = new Size(73, 22);
            checkBox_Type_Audio.TabIndex = 3;
            checkBox_Type_Audio.Text = "Audio";
            checkBox_Type_Audio.UseVisualStyleBackColor = true;
            // 
            // checkBox_Type_Image
            // 
            checkBox_Type_Image.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            checkBox_Type_Image.AutoSize = true;
            checkBox_Type_Image.Location = new Point(6, 88);
            checkBox_Type_Image.Name = "checkBox_Type_Image";
            checkBox_Type_Image.Size = new Size(76, 22);
            checkBox_Type_Image.TabIndex = 2;
            checkBox_Type_Image.Text = "Image";
            checkBox_Type_Image.UseVisualStyleBackColor = true;
            // 
            // checkBox_Type_Document
            // 
            checkBox_Type_Document.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            checkBox_Type_Document.AutoSize = true;
            checkBox_Type_Document.Location = new Point(6, 47);
            checkBox_Type_Document.Name = "checkBox_Type_Document";
            checkBox_Type_Document.Size = new Size(109, 22);
            checkBox_Type_Document.TabIndex = 1;
            checkBox_Type_Document.Text = "Document";
            checkBox_Type_Document.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox2.Controls.Add(groupBox3);
            groupBox2.Controls.Add(button_RemoveTags);
            groupBox2.Controls.Add(button_AddTags);
            groupBox2.Font = new Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(427, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(388, 400);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Tags / Lales";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(treeView_Tags);
            groupBox3.Location = new Point(6, 25);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(376, 309);
            groupBox3.TabIndex = 12;
            groupBox3.TabStop = false;
            groupBox3.Text = "Tags List";
            // 
            // treeView_Tags
            // 
            treeView_Tags.Dock = DockStyle.Fill;
            treeView_Tags.Location = new Point(3, 22);
            treeView_Tags.Name = "treeView_Tags";
            treeView_Tags.Size = new Size(370, 284);
            treeView_Tags.TabIndex = 0;
            // 
            // button_RemoveTags
            // 
            button_RemoveTags.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            button_RemoveTags.BackgroundImage = Properties.Resources.icons8_remove_50;
            button_RemoveTags.BackgroundImageLayout = ImageLayout.Center;
            button_RemoveTags.Cursor = Cursors.Hand;
            button_RemoveTags.Location = new Point(262, 344);
            button_RemoveTags.Name = "button_RemoveTags";
            button_RemoveTags.Size = new Size(120, 50);
            button_RemoveTags.TabIndex = 11;
            button_RemoveTags.UseVisualStyleBackColor = true;
            button_RemoveTags.Click += button_RemoveTags_Click;
            // 
            // button_AddTags
            // 
            button_AddTags.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            button_AddTags.BackgroundImage = Properties.Resources.icons8_add_50;
            button_AddTags.BackgroundImageLayout = ImageLayout.Center;
            button_AddTags.Cursor = Cursors.Hand;
            button_AddTags.Location = new Point(6, 340);
            button_AddTags.Name = "button_AddTags";
            button_AddTags.Size = new Size(120, 54);
            button_AddTags.TabIndex = 10;
            button_AddTags.UseVisualStyleBackColor = true;
            button_AddTags.Click += button_AddTags_Click;
            // 
            // groupBox4
            // 
            groupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox4.Controls.Add(checkBox_Additional_subfiles);
            groupBox4.Controls.Add(checkBox_Additional_Delete);
            groupBox4.Font = new Font("Arial Rounded MT Bold", 12F);
            groupBox4.Location = new Point(3, 409);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(275, 86);
            groupBox4.TabIndex = 2;
            groupBox4.TabStop = false;
            groupBox4.Text = "Additional";
            // 
            // checkBox_Additional_subfiles
            // 
            checkBox_Additional_subfiles.AutoSize = true;
            checkBox_Additional_subfiles.Location = new Point(6, 53);
            checkBox_Additional_subfiles.Name = "checkBox_Additional_subfiles";
            checkBox_Additional_subfiles.Size = new Size(158, 22);
            checkBox_Additional_subfiles.TabIndex = 1;
            checkBox_Additional_subfiles.Text = "Process subfiles";
            checkBox_Additional_subfiles.UseVisualStyleBackColor = true;
            // 
            // checkBox_Additional_Delete
            // 
            checkBox_Additional_Delete.AutoSize = true;
            checkBox_Additional_Delete.Location = new Point(6, 25);
            checkBox_Additional_Delete.Name = "checkBox_Additional_Delete";
            checkBox_Additional_Delete.Size = new Size(191, 22);
            checkBox_Additional_Delete.TabIndex = 0;
            checkBox_Additional_Delete.Text = "Delete empty folders";
            checkBox_Additional_Delete.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            groupBox6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox6.Controls.Add(radioButton_Size_Dynamic);
            groupBox6.Controls.Add(radioButton_Size_Range);
            groupBox6.Font = new Font("Arial Rounded MT Bold", 12F);
            groupBox6.Location = new Point(520, 409);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(295, 86);
            groupBox6.TabIndex = 3;
            groupBox6.TabStop = false;
            groupBox6.Text = "Size";
            // 
            // radioButton_Size_Dynamic
            // 
            radioButton_Size_Dynamic.AutoSize = true;
            radioButton_Size_Dynamic.Location = new Point(6, 53);
            radioButton_Size_Dynamic.Name = "radioButton_Size_Dynamic";
            radioButton_Size_Dynamic.Size = new Size(95, 22);
            radioButton_Size_Dynamic.TabIndex = 1;
            radioButton_Size_Dynamic.TabStop = true;
            radioButton_Size_Dynamic.Text = "Dynamic";
            radioButton_Size_Dynamic.UseVisualStyleBackColor = true;
            // 
            // radioButton_Size_Range
            // 
            radioButton_Size_Range.AutoSize = true;
            radioButton_Size_Range.Location = new Point(6, 25);
            radioButton_Size_Range.Name = "radioButton_Size_Range";
            radioButton_Size_Range.Size = new Size(78, 22);
            radioButton_Size_Range.TabIndex = 0;
            radioButton_Size_Range.TabStop = true;
            radioButton_Size_Range.Text = "Range";
            radioButton_Size_Range.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            groupBox7.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            groupBox7.Controls.Add(radioButton_Media_AspectRatio);
            groupBox7.Controls.Add(radioButton_Media_Codec);
            groupBox7.Controls.Add(radioButton_Media_FrameRate);
            groupBox7.Controls.Add(radioButton_Media_Resolution);
            groupBox7.Controls.Add(radioButton_Media_Duration);
            groupBox7.Font = new Font("Arial Rounded MT Bold", 12F);
            groupBox7.Location = new Point(284, 409);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(230, 200);
            groupBox7.TabIndex = 3;
            groupBox7.TabStop = false;
            groupBox7.Text = "Media";
            // 
            // radioButton_Media_AspectRatio
            // 
            radioButton_Media_AspectRatio.AutoSize = true;
            radioButton_Media_AspectRatio.Location = new Point(6, 169);
            radioButton_Media_AspectRatio.Name = "radioButton_Media_AspectRatio";
            radioButton_Media_AspectRatio.Size = new Size(124, 22);
            radioButton_Media_AspectRatio.TabIndex = 4;
            radioButton_Media_AspectRatio.TabStop = true;
            radioButton_Media_AspectRatio.Text = "Aspect ratio";
            radioButton_Media_AspectRatio.UseVisualStyleBackColor = true;
            // 
            // radioButton_Media_Codec
            // 
            radioButton_Media_Codec.AutoSize = true;
            radioButton_Media_Codec.Location = new Point(6, 133);
            radioButton_Media_Codec.Name = "radioButton_Media_Codec";
            radioButton_Media_Codec.Size = new Size(78, 22);
            radioButton_Media_Codec.TabIndex = 3;
            radioButton_Media_Codec.TabStop = true;
            radioButton_Media_Codec.Text = "Codec";
            radioButton_Media_Codec.UseVisualStyleBackColor = true;
            // 
            // radioButton_Media_FrameRate
            // 
            radioButton_Media_FrameRate.AutoSize = true;
            radioButton_Media_FrameRate.Location = new Point(6, 97);
            radioButton_Media_FrameRate.Name = "radioButton_Media_FrameRate";
            radioButton_Media_FrameRate.Size = new Size(114, 22);
            radioButton_Media_FrameRate.TabIndex = 2;
            radioButton_Media_FrameRate.TabStop = true;
            radioButton_Media_FrameRate.Text = "Frame rate";
            radioButton_Media_FrameRate.UseVisualStyleBackColor = true;
            // 
            // radioButton_Media_Resolution
            // 
            radioButton_Media_Resolution.AutoSize = true;
            radioButton_Media_Resolution.Location = new Point(6, 61);
            radioButton_Media_Resolution.Name = "radioButton_Media_Resolution";
            radioButton_Media_Resolution.Size = new Size(111, 22);
            radioButton_Media_Resolution.TabIndex = 1;
            radioButton_Media_Resolution.TabStop = true;
            radioButton_Media_Resolution.Text = "Resolution";
            radioButton_Media_Resolution.UseVisualStyleBackColor = true;
            // 
            // radioButton_Media_Duration
            // 
            radioButton_Media_Duration.AutoSize = true;
            radioButton_Media_Duration.Location = new Point(6, 25);
            radioButton_Media_Duration.Name = "radioButton_Media_Duration";
            radioButton_Media_Duration.Size = new Size(95, 22);
            radioButton_Media_Duration.TabIndex = 0;
            radioButton_Media_Duration.TabStop = true;
            radioButton_Media_Duration.Text = "Duration";
            radioButton_Media_Duration.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            groupBox8.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox8.Controls.Add(radioButton_Date_Modify);
            groupBox8.Controls.Add(radioButton_Date_Access);
            groupBox8.Controls.Add(radioButton_Date_Creation);
            groupBox8.Font = new Font("Arial Rounded MT Bold", 12F);
            groupBox8.Location = new Point(3, 501);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(275, 108);
            groupBox8.TabIndex = 3;
            groupBox8.TabStop = false;
            groupBox8.Text = "Date";
            // 
            // radioButton_Date_Modify
            // 
            radioButton_Date_Modify.AutoSize = true;
            radioButton_Date_Modify.Location = new Point(6, 77);
            radioButton_Date_Modify.Name = "radioButton_Date_Modify";
            radioButton_Date_Modify.Size = new Size(117, 22);
            radioButton_Date_Modify.TabIndex = 2;
            radioButton_Date_Modify.TabStop = true;
            radioButton_Date_Modify.Text = "Last modify";
            radioButton_Date_Modify.UseVisualStyleBackColor = true;
            // 
            // radioButton_Date_Access
            // 
            radioButton_Date_Access.AutoSize = true;
            radioButton_Date_Access.Location = new Point(6, 51);
            radioButton_Date_Access.Name = "radioButton_Date_Access";
            radioButton_Date_Access.Size = new Size(123, 22);
            radioButton_Date_Access.TabIndex = 1;
            radioButton_Date_Access.TabStop = true;
            radioButton_Date_Access.Text = "Last access";
            radioButton_Date_Access.UseVisualStyleBackColor = true;
            // 
            // radioButton_Date_Creation
            // 
            radioButton_Date_Creation.AutoSize = true;
            radioButton_Date_Creation.Location = new Point(6, 25);
            radioButton_Date_Creation.Name = "radioButton_Date_Creation";
            radioButton_Date_Creation.Size = new Size(95, 22);
            radioButton_Date_Creation.TabIndex = 0;
            radioButton_Date_Creation.TabStop = true;
            radioButton_Date_Creation.Text = "Creation";
            radioButton_Date_Creation.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            groupBox9.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox9.Controls.Add(checkBox_Name_Characters);
            groupBox9.Controls.Add(checkBox_Name_Caps);
            groupBox9.Font = new Font("Arial Rounded MT Bold", 12F);
            groupBox9.Location = new Point(520, 501);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(295, 108);
            groupBox9.TabIndex = 4;
            groupBox9.TabStop = false;
            groupBox9.Text = "Name";
            // 
            // checkBox_Name_Characters
            // 
            checkBox_Name_Characters.AutoSize = true;
            checkBox_Name_Characters.Location = new Point(6, 54);
            checkBox_Name_Characters.Name = "checkBox_Name_Characters";
            checkBox_Name_Characters.Size = new Size(233, 22);
            checkBox_Name_Characters.TabIndex = 1;
            checkBox_Name_Characters.Text = "Ignore special characters";
            checkBox_Name_Characters.UseVisualStyleBackColor = true;
            // 
            // checkBox_Name_Caps
            // 
            checkBox_Name_Caps.AutoSize = true;
            checkBox_Name_Caps.Location = new Point(6, 26);
            checkBox_Name_Caps.Name = "checkBox_Name_Caps";
            checkBox_Name_Caps.Size = new Size(122, 22);
            checkBox_Name_Caps.TabIndex = 0;
            checkBox_Name_Caps.Text = "Ignore caps";
            checkBox_Name_Caps.UseVisualStyleBackColor = true;
            // 
            // UserControl_Configuration_Filter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox9);
            Controls.Add(groupBox8);
            Controls.Add(groupBox7);
            Controls.Add(groupBox6);
            Controls.Add(groupBox4);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "UserControl_Configuration_Filter";
            Size = new Size(818, 612);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            groupBox9.ResumeLayout(false);
            groupBox9.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private ToolTip toolTip1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private CheckBox checkBox_Type_Other;
        private CheckBox checkBox_Type_Archive;
        private CheckBox checkBox_Type_Video;
        private CheckBox checkBox_Type_Audio;
        private CheckBox checkBox_Type_Image;
        private CheckBox checkBox_Type_Document;
        private Button button_RemoveGroup;
        private Button button_AddGroup;
        private GroupBox groupBox5;
        private Button button_RemoveTags;
        private Button button_AddTags;
        private TreeView treeView_Type;
        private GroupBox groupBox3;
        private TreeView treeView_Tags;
        private GroupBox groupBox4;
        private GroupBox groupBox6;
        private GroupBox groupBox7;
        private GroupBox groupBox8;
        private GroupBox groupBox9;
        private CheckBox checkBox_Additional_subfiles;
        private CheckBox checkBox_Additional_Delete;
        private RadioButton radioButton_Date_Modify;
        private RadioButton radioButton_Date_Access;
        private RadioButton radioButton_Date_Creation;
        private RadioButton radioButton_Size_Dynamic;
        private RadioButton radioButton_Size_Range;
        private RadioButton radioButton_Media_AspectRatio;
        private RadioButton radioButton_Media_Codec;
        private RadioButton radioButton_Media_FrameRate;
        private RadioButton radioButton_Media_Resolution;
        private RadioButton radioButton_Media_Duration;
        private CheckBox checkBox_Name_Characters;
        private CheckBox checkBox_Name_Caps;
        private CheckBox checkBox_Type_Executable;
    }
}
