using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tools
{
    public partial class UserControl_Filter : UserControl
    {
        private string SettingPath;

        public UserControl_Filter()
        {
            InitializeComponent();
        }

        private void button_Path_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    SettingPath = fbd.SelectedPath;
                    textBox_Path.Text = SettingPath;
                }
            }
        }

        private void Set_Settings(string SettingPath)
        {

        }

        private void button_Play_Click(object sender, EventArgs e)
        {

        }
    }
}
