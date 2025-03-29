using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Tools
{
    public partial class UserControl_Configuration_Extract : UserControl
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
                    checkBox_Extract_Folder.Checked = state;
                    break;
                default:
                    Debug.WriteLine($"Unknown property: {propertyName}");
                    break;
            }
        }

        public void Set_ExtractJson()
        {
            if (File.Exists("appsettings.json"))
            {
                string settingPath = Path.GetFullPath("appsettings.json");
                string jsonContent = File.ReadAllText(settingPath);
                JObject jsonObject = JObject.Parse(jsonContent);

                // Modify the "Extract" section
                var extractSection = jsonObject["Extract"];
                if (extractSection != null)
                {
                    // Update "Delete" in "Additional"
                    extractSection["Additional"][0]["Delete"] = checkBox_Extract_Delete.Checked; // Example modification
                    extractSection["Additional"][1]["Subfolder"] = checkBox_Extract_Subfolder.Checked;
                    extractSection["Additional"][2]["Folder"] = checkBox_Extract_Folder.Checked;
                }

                // Save the modified JSON back to the file
                File.WriteAllText(settingPath, jsonObject.ToString());

                MessageBox.Show("Configuration updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Configuration file not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
