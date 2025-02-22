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

        private void FollowButton(Button button)
        {
            // Adjust the position of Panel_Index_Config to follow the clicked button
            panel_NavBarIndicator.Height = button.Height;
            panel_NavBarIndicator.Top = button.Top;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FollowButton(sender as Button);
            label_Title.Text = "FILTER";
            userControl_Configuration_Filter1.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FollowButton(sender as Button);
            label_Title.Text = "CONVERT";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FollowButton(sender as Button);
            label_Title.Text = "EXTRACT";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            FollowButton(sender as Button);
            label_Title.Text = "MERGE";

        }

        private void button5_Click(object sender, EventArgs e)
        {
            FollowButton(sender as Button);
            label_Title.Text = "FILTER";

        }

        private void button6_Click(object sender, EventArgs e)
        {
            FollowButton(sender as Button);
            label_Title.Text = "FILTER";

        }

        private void button7_Click(object sender, EventArgs e)
        {
            FollowButton(sender as Button);
            label_Title.Text = "FILTER";

        }

        private void button8_Click(object sender, EventArgs e)
        {
            FollowButton(sender as Button);
            label_Title.Text = "FILTER";

        }

        private void button9_Click(object sender, EventArgs e)
        {
            FollowButton(sender as Button);
            label_Title.Text = "FILTER";

        }

        private void button10_Click(object sender, EventArgs e)
        {
            FollowButton(sender as Button);
            label_Title.Text = "FILTER";

        }
    }
}
