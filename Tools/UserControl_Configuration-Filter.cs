﻿using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Tools
{
    public partial class UserControl_Configuration_Filter : UserControl
    {
        public UserControl_Configuration_Filter()
        {
            InitializeComponent();
        }

        private void UserControl_Configuration_Filter_Load(object sender, EventArgs e)
        {
            while (true)
            {
                if (File.Exists("appsettings.json"))
                {
                    string settingPath = Path.GetFullPath("appsettings.json");
                    Get_FilterSection(settingPath);
                    break;
                }
            }
        }

        private void Get_FilterSection(string settingPath)
        {
            string jsonContent = File.ReadAllText(settingPath);
            JObject jsonObject = JObject.Parse(jsonContent);
            JObject filterSection = (JObject)jsonObject["Filter"];
            Split_FilterSection(filterSection);
        }

        private void Split_FilterSection(JObject filterSection)
        {
            var types = filterSection["Types"] as JArray;
            var additional = filterSection["Additional"] as JArray;
            var date = filterSection["Date"] as JArray;
            var media = filterSection["Media"] as JArray;
            var tags = filterSection["Tags"] as JArray;
            var size = filterSection["Size"] as JArray;
            var name = filterSection["Name"] as JArray;

            ProcessTypes(types);
            ProcessAdditional(additional);
            //ProcessDate(date);
            //ProcessMedia(media);
            //ProcessTags(tags);
            //ProcessSize(size);
            //ProcessName(name);
        }

        private void ProcessTypes(JArray types)
        {
            var lists = new Dictionary<string, JArray>();

            foreach (var type in types)
            {
                foreach (var property in (type as JObject).Properties())
                {
                    if (property.Value.Type == JTokenType.Boolean)
                    {
                        // Process key-value pairs like "Image": false
                        Set_CheckboxStateType(property.Name, (bool)property.Value);
                    }
                    else if (property.Value.Type == JTokenType.Array)
                    {
                        // Collect lists like "Image_List": [...]
                        lists[property.Name] = (JArray)property.Value;
                    }
                }
            }

            // Call Set_TreeViewNodesType after collecting all lists
            Set_TreeViewNodesType(lists);
        }

        private void Set_CheckboxStateType(string propertyName, bool state)
        {
            switch (propertyName)
            {
                case "Image":
                    checkBox_Type_Image.Checked = state;
                    break;
                case "Video":
                    checkBox_Type_Video.Checked = state;
                    break;
                case "Document":
                    checkBox_Type_Document.Checked = state;
                    break;
                case "Audio":
                    checkBox_Type_Audio.Checked = state;
                    break;
                case "Archive":
                    checkBox_Type_Archive.Checked = state;
                    break;
                case "Executable":
                    checkBox_Type_Executable.Checked = state;
                    break;
                case "Other":
                    checkBox_Type_Other.Checked = state;
                    break;
                // Add more cases as needed for other checkboxes
                default:
                    Debug.WriteLine($"Unknown property: {propertyName}");
                    break;
            }
        }

        private void Set_TreeViewNodesType(Dictionary<string, JArray> lists)
        {
            treeView_Type.Invoke(() => treeView_Type.Nodes.Clear());

            foreach (var list in lists)
            {
                // Remove "_List" from the root node name
                string rootNodeName = list.Key.Replace("_List", "");
                TreeNode rootNode = new TreeNode(rootNodeName);

                foreach (var item in list.Value)
                {
                    rootNode.Nodes.Add(new TreeNode(item.ToString()));
                }

                treeView_Type.Invoke(() => treeView_Type.Nodes.Add(rootNode));
            }
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
                    checkBox_Additional_Delete.Checked = state;
                    break;
                case "Subfolder":
                    checkBox_Additional_subfiles.Checked = state;
                    break;
                default:
                    Debug.WriteLine($"Unknown property: {propertyName}");
                    break;
            }
        }


        private void ProcessDate(JArray date)
        {
            // Process the "Date" section
            foreach (var item in date)
            {
                Debug.WriteLine(item);
            }
        }

        private void ProcessMedia(JArray media)
        {
            // Process the "Media" section
            foreach (var item in media)
            {
                Debug.WriteLine(item);
            }
        }

        private void ProcessTags(JArray tags)
        {
            // Process the "Tags" section
            foreach (var item in tags)
            {
                Debug.WriteLine(item);
            }
        }

        private void ProcessSize(JArray size)
        {
            // Process the "Size" section
            foreach (var item in size)
            {
                Debug.WriteLine(item);
            }
        }

        private void ProcessName(JArray name)
        {
            // Process the "Name" section
            foreach (var item in name)
            {
                Debug.WriteLine(item);
            }
        }

    }
}
