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
            FollowButton(sender as Button);
            userControl_Filter1.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FollowButton(sender as Button);
            userControl_Convert1.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FollowButton(sender as Button);
            userControl_Extract1.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FollowButton(sender as Button);
            userControl_Merge1.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FollowButton(sender as Button);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FollowButton(sender as Button);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FollowButton(sender as Button);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FollowButton(sender as Button);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FollowButton(sender as Button);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            FollowButton(sender as Button);
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
            while (true)
            {
                if (File.Exists("appsettings.json"))
                {
                    // File already exists; get the filepath
                    string filePath = Path.GetFullPath("appsettings.json");
                    break;
                }
                else
                {
                    //set_Jsonfile();
                    break;
                }
            }
        }

        private void set_Jsonfile()
        {
            // Create the JSON structure
            var jsonObject = new JObject(
                new JProperty("Filter", new JObject(
                    new JProperty("Types", new JArray(
                        new JObject(new JProperty("Image", false)),
                        new JObject(new JProperty("Video", false)),
                        new JObject(new JProperty("Document", false)),
                        new JObject(new JProperty("Audio", false)),
                        new JObject(new JProperty("Archive", false)),
                        new JObject(new JProperty("Executable", false)),
                        new JObject(new JProperty("Other", false)),
                        new JObject(new JProperty("Image_List", new JArray("itemA", "itemB", "itemC"))),
                        new JObject(new JProperty("Video_List", new JArray("itemA", "itemB", "itemC"))),
                        new JObject(new JProperty("Document_List", new JArray("itemA", "itemB", "itemC")))),
                        new JObject(new JProperty("Audio_List", new JArray("itemA", "itemB", "itemC"))),
                        new JObject(new JProperty("Archive_List", new JArray("itemA", "itemB", "itemC"))),
                        new JObject(new JProperty("Executable_List", new JArray("itemA", "itemB", "itemC"))),
                        new JObject(new JProperty("Other_List", new JArray("itemA", "itemB", "itemC")))
                    )),
                    new JProperty("Additional", new JArray(
                        new JObject(new JProperty("Delete", false)),
                        new JObject(new JProperty("Subfolder", false))
                    )),
                    new JProperty("Date", new JArray(
                        new JObject(new JProperty("Creation", false)),
                        new JObject(new JProperty("Access", false)),
                        new JObject(new JProperty("Modify", false))
                    )),
                    new JProperty("Media", new JArray(
                        new JObject(new JProperty("Duration", false)),
                        new JObject(new JProperty("Resolution", false)),
                        new JObject(new JProperty("FrameRate", false)),
                        new JObject(new JProperty("Codec", false)),
                        new JObject(new JProperty("AspectRatio", false))
                    )),
                    new JProperty("Size", new JArray(
                        new JObject(new JProperty("Range", false)),
                        new JObject(new JProperty("Resolution", false)),
                        new JProperty("var4", new JArray(
                             new JObject(new JProperty("key1", new JArray("value1a", "value1b"))),
                             new JObject(new JProperty("key2", new JArray("value2a", "value2b")))
                        ))
                    )),
                    new JProperty("Convert", new JObject(
                        new JProperty("var1", "another_string"),
                        new JProperty("var2", "yet_another_string")
                    ))
            );

            // Convert the JObject to a JSON string
            string jsonString = jsonObject.ToString();

            // Write the JSON string to the file appsettings.json
            File.WriteAllText("appsettings.json", jsonString);
        }
    }
}
