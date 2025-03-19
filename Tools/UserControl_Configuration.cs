using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection.Emit;
using static System.Net.Mime.MediaTypeNames;

namespace Tools
{
    public partial class UserControl_Configuration : UserControl
    {
        public UserControl_Configuration()
        {
            InitializeComponent();
            label_Title.Text = "FILTER";
            userControl_Configuration_Filter1.BringToFront();
        }

        public void ReloadConfigFilterSettings()
        {
            userControl_Configuration_Filter1.ReloadSettings();
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

        private void button_Save_MouseEnter(object sender, EventArgs e)
        {

        }

        private void button_Save_MouseLeave(object sender, EventArgs e)
        {

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
    }
}
