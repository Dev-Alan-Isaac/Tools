﻿using System;
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
        public UserControl_Filter()
        {
            InitializeComponent();
        }

        private void UserControl_Filter_Load(object sender, EventArgs e)
        {
            while (true)
            {
                if (File.Exists("Config_Sort.json"))
                {
                    // File already exists; get the filepath
                    string filePath = Path.GetFullPath("Config_Sort.json");
                    break;
                }
                else
                {

                }
            }
        }
    }
}
