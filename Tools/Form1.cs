using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tools.Properties;

namespace Tools
{
    public partial class Form : System.Windows.Forms.Form
    {
        private Color defaultColor;
        private Image defaultIndicator = Resources.icons8_double_right_50;

        public Form()
        {
            InitializeComponent();
            defaultColor = button_Close.BackColor;
            userControl_Filter1.BringToFront();
        }

        private void panel_TopBar_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e); Graphics g = e.Graphics; int shadowSize = 20; // Adjust this to change shadow size
                                                                           // Draw shadow
            for (int i = 0; i < shadowSize; i++)
            {
                int alpha = Math.Max(0, 50 - (i * 5));
                Color shadowColor = Color.FromArgb(alpha, Color.Black);
                using (Pen pen = new Pen(shadowColor, 1))
                {
                    g.DrawRectangle(pen, new Rectangle(i, i, this.Width - i * 2, this.Height - i * 2));
                }
            }
            // Check if Parent is not null
            Color parentBackColor = this.Parent != null ? this.Parent.BackColor : Color.White;
            // Draw original panel
            g.FillRectangle(new SolidBrush(parentBackColor), new Rectangle(0, 0, this.Width, this.Height));
            g.FillRectangle(Brushes.White, new Rectangle(shadowSize, shadowSize, this.Width - shadowSize * 2, this.Height - shadowSize * 2));
        }

        private void button_Close_MouseLeave(object sender, EventArgs e)
        {
            button_Close.BackColor = defaultColor; // Change to desired hover color
        }

        private void button_Close_MouseEnter(object sender, EventArgs e)
        {
            button_Close.BackColor = Color.Red; // Change to desired hover color
        }

        private void button_Config_MouseLeave(object sender, EventArgs e)
        {
            button_Config.BackColor = defaultColor; // Change to desired hover color
        }

        private void button_Config_MouseEnter(object sender, EventArgs e)
        {
            button_Config.BackColor = Color.SteelBlue; // Change to desired hover color
        }

        private void FollowButton(Button button)
        {
            // Adjust the position of Panel_Index_Config to follow the clicked button
            panel_NavBarIndicator.Height = button.Height;
            panel_NavBarIndicator.Top = button.Top;
            panel_NavBarIndicator.BackgroundImage = defaultIndicator;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                FollowButton(button);
                userControl_Filter1.BringToFront();
                userControl_Filter1.ReloadSettings();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                FollowButton(button);
                userControl_Convert1.BringToFront();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                FollowButton(button);
                userControl_Extract1.BringToFront();
                userControl_Extract1.ReloadSettings();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                FollowButton(button);
                userControl_Merge1.BringToFront();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                FollowButton(button);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                FollowButton(button);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                FollowButton(button);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                FollowButton(button);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                FollowButton(button);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                FollowButton(button);
            }
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_Config_Click(object sender, EventArgs e)
        {
            userControl_Configuration1.BringToFront();
            panel_NavBarIndicator.BackgroundImage = Resources.icons8_minus_50;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            if (!File.Exists("appsettings.json"))
            {
                set_Jsonfile();
            }
            userControl_Configuration1.ReloadConfigFilterSettings();
        }

        public void set_Jsonfile()
        {
            var jsonObject = new JObject(
                new JProperty("Filter", new JObject(
                    new JProperty("Types", new JArray(
                        new JObject(new JProperty("Image", true)),
                        new JObject(new JProperty("Video", true)),
                        new JObject(new JProperty("Document", true)),
                        new JObject(new JProperty("Audio", true)),
                        new JObject(new JProperty("Archive", true)),
                        new JObject(new JProperty("Executable", true)),
                        new JObject(new JProperty("Other", true)),
                        new JObject(new JProperty("Image_List", new JArray("jpg", "png", "gif", "bmp", "jpeg"))),
                        new JObject(new JProperty("Video_List", new JArray("mp4", "m4v", "avi", "mkv", "3gp", "mov", "wmv", "webm", "ts", "mpg", "asf", "flv", "mpeg"))),
                        new JObject(new JProperty("Document_List", new JArray("txt", "docx", "pdf", "pptx"))),
                        new JObject(new JProperty("Audio_List", new JArray("mp3", "wav", "aac", "flac", "ogg", "m4a", "wma", "alac", "aiff"))),
                        new JObject(new JProperty("Archive_List", new JArray("zip", "rar", "7z", "tar", "gz", "bz2", "iso", "xz"))),
                        new JObject(new JProperty("Executable_List", new JArray("exe", "bat", "sh", "msi", "bin", "cmd", "apk", "com", "jar"))),
                        new JObject(new JProperty("Other_List", new JArray("")))
                    )),
                    new JProperty("Additional", new JArray(
                        new JObject(new JProperty("Delete", true)),
                        new JObject(new JProperty("Subfolder", true))
                    )),
                    new JProperty("Date", new JArray(
                        new JObject(new JProperty("Creation", true)),
                        new JObject(new JProperty("Access", false)),
                        new JObject(new JProperty("Modify", false))
                    )),
                    new JProperty("Media", new JArray(
                        new JObject(new JProperty("Duration", true)),
                        new JObject(new JProperty("Resolution", false)),
                        new JObject(new JProperty("FrameRate", false)),
                        new JObject(new JProperty("Codec", false)),
                        new JObject(new JProperty("AspectRatio", false))
                    )),
                    new JProperty("Tags", new JArray(
                        new JObject(new JProperty("Tag_List", new JArray("")))
                    )),
                    new JProperty("Size", new JArray(
                        new JObject(new JProperty("Range", true)),
                        new JObject(new JProperty("Dynamic", false)),
                        new JObject(new JProperty("Range_List", new JArray(
                            new JObject(new JProperty("Small", new JArray("100", "MB"))),
                            new JObject(new JProperty("Medium", new JArray("100", "MB", "1", "GB"))),
                            new JObject(new JProperty("Large", new JArray("1", "GB", "10", "GB"))),
                            new JObject(new JProperty("Extra_Large", new JArray("10", "GB")))
                        )))
                    )),
                    new JProperty("Name", new JArray(
                        new JObject(new JProperty("Caps", true)),
                        new JObject(new JProperty("Chars", false))
                    ))
                )),
                new JProperty("Convert", new JObject(
                    new JProperty("Additional", new JArray(
                        new JObject(new JProperty("Delete", true)),
                        new JObject(new JProperty("Subfolder", true)),
                        new JObject(new JProperty("Keep", true))
                    )),
                    new JProperty("Selection", new JArray(
                        new JObject(new JProperty("Image", "BMP")),
                        new JObject(new JProperty("Audio", "MP3")),
                        new JObject(new JProperty("Video", "MP4")),
                        new JObject(new JProperty("Document", "DOC"))
                    ))
                )),
                new JProperty("Extract", new JObject(
                    new JProperty("Additional", new JArray(
                        new JObject(new JProperty("Delete", true)),
                        new JObject(new JProperty("Subfolder", true)), 
                        new JObject(new JProperty("Folder", true))
                    ))
                )),
                new JProperty("Merge", new JObject(
                    new JProperty("Additional", new JArray(
                        new JObject(new JProperty("Delete", true)),
                        new JObject(new JProperty("Subfolder", true)),
                        new JObject(new JProperty("Keep", true))
                    ))
                ))
            );

            // Convert the JObject to a JSON string
            string jsonString = jsonObject.ToString();

            // Write the JSON string to the file appsettings.json
            File.WriteAllText("appsettings.json", jsonString);
        }
    }
}
