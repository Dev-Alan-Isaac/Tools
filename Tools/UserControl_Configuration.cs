namespace Tools
{
    public partial class UserControl_Configuration : UserControl
    {
        private Color defaultColor;

        public UserControl_Configuration()
        {
            InitializeComponent();
            defaultColor = button1.BackColor;
            label_Title.Text = "FILTER";
            userControl_Configuration_Filter1.BringToFront();
        }

        public void ReloadConfigFilterSettings()
        {
            userControl_Configuration_Filter1.ReloadSettings();
            userControl_Configuration_Extract1.ReloadSettings();
        }

        private void FollowButton(Button button)
        {
            // Adjust the position of Panel_Index_Config to follow the clicked button
            panel_NavBarIndicator.Height = button.Height;
            panel_NavBarIndicator.Top = button.Top;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                FollowButton(button);
                label_Title.Text = "FILTER";
                userControl_Configuration_Filter1.BringToFront();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                FollowButton(button);
                label_Title.Text = "CONVERT";
                userControl_Configuration_Convert1.BringToFront();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                FollowButton(button);
                label_Title.Text = "EXTRACT";
                userControl_Configuration_Extract1.BringToFront();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                FollowButton(button);
                label_Title.Text = "MERGE";
                userControl_Configuration_Merge1.BringToFront();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                FollowButton(button);
                label_Title.Text = "";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                FollowButton(button);
                label_Title.Text = "";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                FollowButton(button);
                label_Title.Text = "";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                FollowButton(button);
                label_Title.Text = "";
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                FollowButton(button);
                label_Title.Text = "";
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                FollowButton(button);
                label_Title.Text = "";
            }
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            string optionstring = label_Title.Text;

            switch (optionstring)
            {
                case "FILTER":
                    userControl_Configuration_Filter1.Set_FilterJson();
                    break;
                case "CONVERT":
                    break;
                case "EXTRACT":
                    break;
                case "MERGE":
                    break;
                default:
                    break;
            }
        }

        private void button_Save_MouseEnter(object sender, EventArgs e)
        {
            button_Save.BackColor = Color.LightBlue; // Change to desired hover color
        }

        private void button_Save_MouseLeave(object sender, EventArgs e)
        {
            button_Save.BackColor = defaultColor; // Change to desired hover color
        }
    }
}
