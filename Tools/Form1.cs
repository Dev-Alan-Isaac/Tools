namespace Tools
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
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
    }
}
