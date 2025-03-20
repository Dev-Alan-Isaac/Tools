using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Tools
{
    public partial class UserControl_Configuration_Extract: UserControl
    {
        public UserControl_Configuration_Extract()
        {
            InitializeComponent();
        }

        public void ReloadSettings()
        {
            if (File.Exists("appsettings.json"))
            {
                string settingPath = Path.GetFullPath("appsettings.json");
                Get_ExtractSection(settingPath);
            }
        }

        private void Get_ExtractSection(string settingPath)
        {
            string jsonContent = File.ReadAllText(settingPath);
            JObject jsonObject = JObject.Parse(jsonContent);
            JObject filterSection = (JObject)jsonObject["Extract"];
            Split_ExtractSection(filterSection);
        }

        private void Split_ExtractSection(JObject filterSection)
        {
            var additional = filterSection["Additional"] as JArray;

            ProcessAdditional(additional);
        }

        private void ProcessAdditional(JArray additional)
        {
            foreach (var type in additional)
            {
                foreach (var property in (type as JObject).Properties())
                {
                    if (property.Value.Type == JTokenType.Boolean)
                    {
                        // Process key-value pairs like "Image": false
                        Set_CheckboxStateAdditional(property.Name, (bool)property.Value);
                    }
                }
            }
        }

        private void Set_CheckboxStateAdditional(string propertyName, bool state)
        {
            switch (propertyName)
            {
                case "Delete":
                    checkBox_Extract_Delete.Checked = state;
                    break;
                case "Subfolder":
                    checkBox_Extract_Subfolder.Checked = state;
                    break;
                case "Folder":
                    checkBox_Extract_Subfolder.Checked = state;
                    break;
                default:
                    Debug.WriteLine($"Unknown property: {propertyName}");
                    break;
            }
        }
    }
}
