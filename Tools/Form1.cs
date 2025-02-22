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

        private void button_Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_Config_Click(object sender, EventArgs e)
        {
            userControl_Configuration1.BringToFront();
            panel_NavBarIndicator.BackgroundImage = Resources.icons8_minus_50;
        }
    }
}
