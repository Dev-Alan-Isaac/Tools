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
    public partial class UserControl_Configuration_Convert : UserControl
    {
        public UserControl_Configuration_Convert()
        {
            InitializeComponent();
            comboBox_img.SelectedIndex = 0;
            comboBox_Aud.SelectedIndex = 0;
            comboBox_Doc.SelectedIndex = 0;
            comboBox_Vid.SelectedIndex = 0;
        }

        public void ReloadSettings()
        {
            if (File.Exists("appsettings.json"))
            {
                string settingPath = Path.GetFullPath("appsettings.json");
                Get_ConvertSection(settingPath);
            }
        }

        private void Get_ConvertSection(string settingPath)
        {
            string jsonContent = File.ReadAllText(settingPath);
            JObject jsonObject = JObject.Parse(jsonContent);
            JObject filterSection = (JObject)jsonObject["Convert"];
            Split_ConvertSection(filterSection);
        }

        private void Split_ConvertSection(JObject filterSection)
        {
            var additional = filterSection["Additional"] as JArray;
            var selections = filterSection["Selection"] as JArray;

            ProcessAdditional(additional);
            ProcessSelects(selections);
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

        private void ProcessSelects(JArray additional)
        {
            foreach (var type in additional)
            {
                foreach (var property in (type as JObject).Properties())
                {
                    if (property.Value.Type == JTokenType.String)
                    {
                        if (property.Name == "Image")
                        {
                            label_Opt_Img.Text = (string)property.Value;
                        }
                        if (property.Name == "Audio")
                        {
                            label_Opt_Audio.Text = (string)property.Value;
                        }
                        if (property.Name == "Video")
                        {
                            label_Opt_Vid.Text = (string)property.Value;
                        }
                        if (property.Name == "Document")
                        {
                            label_Opt_Doc.Text = (string)property.Value;
                        }
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
                case "Keep":
                    checkBox_Extract_Folder.Checked = state;
                    break;
                default:
                    Debug.WriteLine($"Unknown property: {propertyName}");
                    break;
            }
        }

        public void Set_ConvertJson()
        {
            if (File.Exists("appsettings.json"))
            {
                string settingPath = Path.GetFullPath("appsettings.json");
                string jsonContent = File.ReadAllText(settingPath);
                JObject jsonObject = JObject.Parse(jsonContent);

                // Modify the "Extract" section
                var extractSection = jsonObject["Convert"];
                if (extractSection != null)
                {
                    // Update "Delete" in "Additional"
                    extractSection["Additional"][0]["Delete"] = checkBox_Extract_Delete.Checked; 
                    extractSection["Additional"][1]["Subfolder"] = checkBox_Extract_Subfolder.Checked;
                    extractSection["Additional"][2]["Keep"] = checkBox_Extract_Folder.Checked;

                    // Update "Selection" in "Convert"
                    extractSection["Selection"][0]["Image"] = comboBox_img.SelectedItem.ToString();
                    extractSection["Selection"][1]["Audio"] = comboBox_Aud.SelectedItem.ToString();
                    extractSection["Selection"][2]["Video"] = comboBox_Vid.SelectedItem.ToString();
                    extractSection["Selection"][3]["Document"] = comboBox_Doc.SelectedItem.ToString();
                }

                // Save the modified JSON back to the file
                File.WriteAllText(settingPath, jsonObject.ToString());

                label_Opt_Img.Text = comboBox_img.SelectedItem.ToString();
                label_Opt_Audio.Text = comboBox_Aud.SelectedItem.ToString();
                label_Opt_Vid.Text = comboBox_Vid.SelectedItem.ToString();
                label_Opt_Doc.Text = comboBox_Doc.SelectedItem.ToString();

                MessageBox.Show("Configuration updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Configuration file not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
