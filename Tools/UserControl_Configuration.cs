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

namespace Tools
{
    public partial class UserControl_Configuration : UserControl
    {
        private string OptionString = string.Empty;

        public UserControl_Configuration()
        {
            InitializeComponent();
        }

        private void SetText(string text)
        {
            label_Title.Text = text;
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

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FollowButton(sender as Button);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FollowButton(sender as Button);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            FollowButton(sender as Button);

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
    }
}
