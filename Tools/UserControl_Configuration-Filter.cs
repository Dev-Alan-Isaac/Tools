using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Tools
{
    public partial class UserControl_Configuration_Filter : UserControl
    {
        private Color defaultColor;

        public UserControl_Configuration_Filter()
        {
            InitializeComponent();
            defaultColor = button_RemoveTags.BackColor;
        }

        public void ReloadSettings()
        {
            if (File.Exists("appsettings.json"))
            {
                string settingPath = Path.GetFullPath("appsettings.json");
                Get_FilterSection(settingPath);
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
            ProcessDate(date);
            ProcessMedia(media);
            ProcessTags(tags);
            ProcessSize(size);
            ProcessName(name);
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
            bool isAnyTrue = false;

            foreach (var item in date)
            {
                foreach (var property in (item as JObject).Properties())
                {
                    if (property.Value.Type == JTokenType.Boolean && (bool)property.Value)
                    {
                        if (isAnyTrue)
                        {
                            // Handle the error for multiple true values
                            Debug.WriteLine("Error: More than one radio button is set to true.");
                            return;
                        }
                        isAnyTrue = true;
                        Set_RadioButtonStateDate(property.Name, true);
                    }
                    else if (property.Value.Type == JTokenType.Boolean)
                    {
                        Set_RadioButtonStateDate(property.Name, false);
                    }
                }
            }
        }

        private void Set_RadioButtonStateDate(string propertyName, bool state)
        {
            switch (propertyName)
            {
                case "Creation":
                    radioButton_Date_Creation.Checked = state;
                    break;
                case "Access":
                    radioButton_Date_Access.Checked = state;
                    break;
                case "Modify":
                    radioButton_Date_Modify.Checked = state;
                    break;
                default:
                    Debug.WriteLine($"Unknown property: {propertyName}");
                    break;
            }
        }

        private void ProcessMedia(JArray media)
        {
            bool isAnyTrue = false;

            foreach (var item in media)
            {
                foreach (var property in (item as JObject).Properties())
                {
                    if (property.Value.Type == JTokenType.Boolean && (bool)property.Value)
                    {
                        if (isAnyTrue)
                        {
                            // Handle the error for multiple true values
                            Debug.WriteLine("Error: More than one radio button is set to true.");
                            return;
                        }
                        isAnyTrue = true;
                        Set_RadioButtonStateMedia(property.Name, true);
                    }
                    else if (property.Value.Type == JTokenType.Boolean)
                    {
                        Set_RadioButtonStateMedia(property.Name, false);
                    }
                }
            }
        }

        private void Set_RadioButtonStateMedia(string propertyName, bool state)
        {
            switch (propertyName)
            {
                case "Duration":
                    radioButton_Media_Duration.Checked = state;
                    break;
                case "Resolution":
                    radioButton_Media_Resolution.Checked = state;
                    break;
                case "FrameRate":
                    radioButton_Media_FrameRate.Checked = state;
                    break;
                case "Codec":
                    radioButton_Media_Codec.Checked = state;
                    break;
                case "AspectRatio":
                    radioButton_Media_AspectRatio.Checked = state;
                    break;
                default:
                    Debug.WriteLine($"Unknown property: {propertyName}");
                    break;
            }
        }

        private void ProcessTags(JArray tags)
        {
            // Assuming you want to call Set_TreeViewNodeTags with the appropriate dictionary
            Dictionary<string, JArray> lists = new Dictionary<string, JArray>
            {
                { "Tags", tags }
            };

            Set_TreeViewNodeTags(lists);
        }

        private void Set_TreeViewNodeTags(Dictionary<string, JArray> lists)
        {
            treeView_Tags.Invoke(() =>
            {
                treeView_Tags.Nodes.Clear();

                foreach (var kvp in lists)
                {
                    foreach (var item in kvp.Value)
                    {
                        var tagObject = (JObject)item;
                        var tagList = tagObject["Tag_List"] as JArray;

                        if (tagList != null)
                        {
                            foreach (var tag in tagList)
                            {
                                TreeNode rootNode = new TreeNode(tag.ToString());
                                treeView_Tags.Nodes.Add(rootNode);
                            }
                        }
                    }
                }
            });
        }

        private void ProcessSize(JArray size)
        {
            bool isAnyTrue = false;

            foreach (var item in size)
            {
                foreach (var property in (item as JObject).Properties())
                {
                    if (property.Value.Type == JTokenType.Boolean && (bool)property.Value)
                    {
                        if (isAnyTrue)
                        {
                            // Handle the error for multiple true values
                            Debug.WriteLine("Error: More than one radio button is set to true.");
                            return;
                        }
                        isAnyTrue = true;
                        Set_RadioButtonStateSize(property.Name, true);
                    }
                    else if (property.Value.Type == JTokenType.Boolean)
                    {
                        Set_RadioButtonStateSize(property.Name, false);
                    }
                }
            }
        }

        private void Set_RadioButtonStateSize(string propertyName, bool state)
        {
            switch (propertyName)
            {
                case "Range":
                    radioButton_Size_Range.Checked = state;
                    break;
                case "Dynamic":
                    radioButton_Size_Dynamic.Checked = state;
                    break;
                default:
                    Debug.WriteLine($"Unknown property: {propertyName}");
                    break;
            }
        }

        private void ProcessName(JArray name)
        {
            foreach (var type in name)
            {
                foreach (var property in (type as JObject).Properties())
                {
                    if (property.Value.Type == JTokenType.Boolean)
                    {
                        // Process key-value pairs like "Image": false
                        Set_CheckboxStateName(property.Name, (bool)property.Value);
                    }
                }
            }
        }

        private void Set_CheckboxStateName(string propertyName, bool state)
        {
            switch (propertyName)
            {
                case "Caps":
                    radioButton_Caps.Checked = state;
                    break;
                case "Chars":
                    radioButton_ASCII.Checked = state;
                    break;
                default:
                    Debug.WriteLine($"Unknown property: {propertyName}");
                    break;
            }
        }

        public void Set_FilterJson()
        {
            if (File.Exists("appsettings.json"))
            {
                string settingPath = Path.GetFullPath("appsettings.json");
                string jsonContent = File.ReadAllText(settingPath);
                JObject jsonObject = JObject.Parse(jsonContent);
                JArray typesArray = (JArray)jsonObject["Filter"]["Types"];

                // Update the Types array based on checkboxes
                typesArray[0]["Image"] = checkBox_Type_Image.Checked;
                typesArray[1]["Video"] = checkBox_Type_Video.Checked;
                typesArray[2]["Document"] = checkBox_Type_Document.Checked;
                typesArray[3]["Audio"] = checkBox_Type_Audio.Checked;
                typesArray[4]["Archive"] = checkBox_Type_Archive.Checked;
                typesArray[5]["Executable"] = checkBox_Type_Executable.Checked;
                typesArray[6]["Other"] = checkBox_Type_Other.Checked;

                // Clear and update the lists based on the tree view nodes
                typesArray[7]["Image_List"] = new JArray();
                typesArray[8]["Video_List"] = new JArray();
                typesArray[9]["Document_List"] = new JArray();
                typesArray[10]["Audio_List"] = new JArray();
                typesArray[11]["Archive_List"] = new JArray();
                typesArray[12]["Executable_List"] = new JArray();
                typesArray[13]["Other_List"] = new JArray();

                foreach (TreeNode rootNode in treeView_Type.Nodes)
                {
                    foreach (TreeNode childNode in rootNode.Nodes)
                    {
                        string extension = childNode.Text;
                        switch (rootNode.Text)
                        {
                            case "Image":
                                ((JArray)typesArray[7]["Image_List"]).Add(extension);
                                break;
                            case "Video":
                                ((JArray)typesArray[8]["Video_List"]).Add(extension);
                                break;
                            case "Document":
                                ((JArray)typesArray[9]["Document_List"]).Add(extension);
                                break;
                            case "Audio":
                                ((JArray)typesArray[10]["Audio_List"]).Add(extension);
                                break;
                            case "Archive":
                                ((JArray)typesArray[11]["Archive_List"]).Add(extension);
                                break;
                            case "Executable":
                                ((JArray)typesArray[12]["Executable_List"]).Add(extension);
                                break;
                            case "Other":
                                ((JArray)typesArray[13]["Other_List"]).Add(extension);
                                break;
                        }
                    }
                }

                jsonObject["Filter"]["Types"] = typesArray;

                // Update the rest of the filter section based on other controls
                JObject filterSection = (JObject)jsonObject["Filter"];
                filterSection["Additional"][0]["Delete"] = checkBox_Additional_Delete.Checked;
                filterSection["Additional"][1]["Subfolder"] = checkBox_Additional_subfiles.Checked;
                filterSection["Date"][0]["Creation"] = radioButton_Date_Creation.Checked;
                filterSection["Date"][1]["Access"] = radioButton_Date_Access.Checked;
                filterSection["Date"][2]["Modify"] = radioButton_Date_Modify.Checked;
                filterSection["Media"][0]["Duration"] = radioButton_Media_Duration.Checked;
                filterSection["Media"][1]["Resolution"] = radioButton_Media_Resolution.Checked;
                filterSection["Media"][2]["FrameRate"] = radioButton_Media_FrameRate.Checked;
                filterSection["Media"][3]["Codec"] = radioButton_Media_Codec.Checked;
                filterSection["Media"][4]["AspectRatio"] = radioButton_Media_AspectRatio.Checked;

                // Clear and prepare the Tags array
                // Clear and prepare the Tags array
                filterSection["Tags"][0]["Tag_List"] = new JArray();

                foreach (TreeNode rootNode in treeView_Tags.Nodes) // Assuming you have a TreeView for Tags
                {
                    // Add each root node text directly to Tag_List
                    ((JArray)filterSection["Tags"][0]["Tag_List"]).Add(rootNode.Text);

                    // Add all child nodes' text as well
                    foreach (TreeNode childNode in rootNode.Nodes)
                    {
                        ((JArray)filterSection["Tags"][0]["Tag_List"]).Add(childNode.Text);
                    }
                }

                filterSection["Size"][0]["Range"] = radioButton_Size_Range.Checked;
                filterSection["Size"][1]["Dynamic"] = radioButton_Size_Dynamic.Checked;
                filterSection["Name"][0]["Caps"] = radioButton_Caps.Checked;
                filterSection["Name"][1]["Chars"] = radioButton_ASCII.Checked;

                File.WriteAllText(settingPath, jsonObject.ToString());

                MessageBox.Show("Configuration updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Configuration file not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_AddGroup_Click(object sender, EventArgs e)
        {
            // Prompt the user to enter the new extension
            string newExtension = Microsoft.VisualBasic.Interaction.InputBox("Enter the new extension:", "New Extension", "", -1, -1);

            // Check if the user entered a value
            if (!string.IsNullOrWhiteSpace(newExtension))
            {
                // Ensure a node is selected in the TreeView
                if (treeView_Type.SelectedNode != null)
                {
                    // Add the new extension as a child node under the selected parent node
                    TreeNode newNode = new TreeNode(newExtension);
                    treeView_Type.SelectedNode.Nodes.Add(newNode);

                    // Optionally, expand the parent node to show the new child
                    treeView_Type.SelectedNode.Expand();

                    // Inform the user about the successful addition
                    MessageBox.Show($"The extension \"{newExtension}\" was successfully added under \"{treeView_Type.SelectedNode.Text}\".",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Notify the user to select a parent node
                    MessageBox.Show("Please select a parent node in the TreeView before adding a new extension.",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                // Notify the user if no extension was entered
                MessageBox.Show("No extension was entered. Please try again.",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button_RemoveGroup_Click(object sender, EventArgs e)
        {
            // Ensure a node is selected in the TreeView
            if (treeView_Type.SelectedNode != null)
            {
                // Confirm with the user before removal
                DialogResult result = MessageBox.Show($"Are you sure you want to remove the node \"{treeView_Type.SelectedNode.Text}\" and all its child nodes?",
                    "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Remove the selected node
                    treeView_Type.Nodes.Remove(treeView_Type.SelectedNode);

                    // Notify the user about the removal
                    MessageBox.Show("The selected node and its children have been removed successfully.",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                // Notify the user to select a node before removing
                MessageBox.Show("Please select a node in the TreeView to remove.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_AddTags_Click(object sender, EventArgs e)
        {
            // Prompt the user to enter the new tag
            string newTag = Microsoft.VisualBasic.Interaction.InputBox("Enter the new tag:", "New Tag", "", -1, -1);

            // Check if the user entered a valid tag
            if (!string.IsNullOrWhiteSpace(newTag))
            {
                // Create a new root node with the tag and add it to the treeView_Tags
                TreeNode rootNode = new TreeNode(newTag);
                treeView_Tags.Nodes.Add(rootNode);

                // Optionally, expand the treeView_Tags to show the new root node
                treeView_Tags.ExpandAll();

                // Notify the user about the successful addition
                MessageBox.Show($"The tag \"{newTag}\" was successfully added as a root node.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Notify the user if no tag was entered
                MessageBox.Show("No tag was entered. Please try again.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_RemoveTags_Click(object sender, EventArgs e)
        {
            // Ensure a node is selected in the TreeView
            if (treeView_Tags.SelectedNode != null)
            {
                // Check if the selected node is a root node
                if (treeView_Tags.SelectedNode.Parent == null)
                {
                    // Confirm with the user before removing the root node
                    DialogResult result = MessageBox.Show($"Are you sure you want to remove the root node \"{treeView_Tags.SelectedNode.Text}\"?",
                        "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        // Remove the selected root node
                        treeView_Tags.Nodes.Remove(treeView_Tags.SelectedNode);

                        // Notify the user about the successful removal
                        MessageBox.Show("The selected root node has been removed successfully.",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    // Notify the user that only root nodes can be removed
                    MessageBox.Show("Please select a root node to remove. Child nodes cannot be removed directly with this action.",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                // Notify the user to select a node before removing
                MessageBox.Show("Please select a root node in the TreeView to remove.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button_AddGroup_MouseEnter(object sender, EventArgs e)
        {
            button_AddGroup.BackColor = Color.LightGreen; // Change to desired hover color
        }

        private void button_AddGroup_MouseLeave(object sender, EventArgs e)
        {
            button_AddGroup.BackColor = defaultColor; // Change to desired hover color
        }

        private void button_AddTags_MouseEnter(object sender, EventArgs e)
        {
            button_AddTags.BackColor = Color.LightGreen; // Change to desired hover color
        }

        private void button_AddTags_MouseLeave(object sender, EventArgs e)
        {
            button_AddTags.BackColor = defaultColor; // Change to desired hover color
        }

        private void button_RemoveGroup_MouseEnter(object sender, EventArgs e)
        {
            button_RemoveGroup.BackColor = Color.Red; // Change to desired hover color
        }

        private void button_RemoveGroup_MouseLeave(object sender, EventArgs e)
        {
            button_RemoveGroup.BackColor = defaultColor; // Change to desired hover color
        }

        private void button_RemoveTags_MouseEnter(object sender, EventArgs e)
        {
            button_RemoveTags.BackColor = Color.Red; // Change to desired hover color
        }

        private void button_RemoveTags_MouseLeave(object sender, EventArgs e)
        {
            button_RemoveTags.BackColor = defaultColor; // Change to desired hover color
        }
    }
}
