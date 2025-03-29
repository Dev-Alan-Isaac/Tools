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

namespace Tools
{
    public partial class UserControl_Convert : UserControl
    {
        private ConvertSettings convertSettings;
        private string PathSort;
        private string[] files;
        private int totalFiles = 0;

        public UserControl_Convert()
        {
            InitializeComponent();
        }

        private void UserControl_Convert_Load(object sender, EventArgs e)
        {
            ReloadSettings();
        }

        private void button_Path_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    PathSort = fbd.SelectedPath;
                    textBox_Path.Text = PathSort;

                    // Run the ProcessFiles method asynchronously and update the textBox_Files after completion
                    Task.Run(async () =>
                    {
                        files = await ProcessFiles(PathSort);
                        Invoke(new Action(() => textBox_Files.Text = "Files: " + totalFiles.ToString()));
                    });
                }
            }
        }

        private async void button_Play_Click(object sender, EventArgs e)
        {
            // Clear the TextBox logs before adding new lines
            textBox_Logs.Invoke(new Action(() =>
            {
                textBox_Logs.Clear();
            }));

            Task processingTask = null;

            if (radioButton_Img.Checked)
            {
                processingTask = Task.Run(() => Image(files));
            }
            else if (radioButton_Aud.Checked)
            {
                processingTask = Task.Run(() => Audio(files));
            }
            else if (radioButton_Vid.Checked)
            {
                processingTask = Task.Run(() => Video(files));
            }
            else if (radioButton_Doc.Checked)
            {
                processingTask = Task.Run(() => Document(files));
            }
            else
            {
                MessageBox.Show("Please select an option to proceed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (processingTask != null)
                {
                    await processingTask; // Wait for the task to finish
                }

                // Perform delete action if enabled in settings
                if (convertSettings.Delete)
                {
                    Delete_Folders(PathSort);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Private Functions
        private async Task Image(string[] files)
        {

        }

        private async Task Audio(string[] files)
        {

        }
        private async Task Video(string[] files)
        {

        }
        private async Task Document(string[] files)
        {

        }

        // Global Functions
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
            JObject extractSection = (JObject)jsonObject["Convert"];
            Set_ClassValues(extractSection);
        }

        private void Set_ClassValues(JObject extractSection)
        {
            convertSettings = new ConvertSettings
            {
                // Extracting values from "Additional"
                Delete = extractSection["Additional"]?[0]?["Delete"]?.ToObject<bool>() ?? false,
                Subfolder = extractSection["Additional"]?[1]?["Subfolder"]?.ToObject<bool>() ?? false,
                Folder = extractSection["Additional"]?[2]?["Folder"]?.ToObject<bool>() ?? false,

                // Extracting values from "Selection"
                Image = extractSection["Selection"]?[0]?["Image"]?.ToString() ?? "",
                Video = extractSection["Selection"]?[1]?["Video"]?.ToString() ?? "",
                Audio = extractSection["Selection"]?[2]?["Audio"]?.ToString() ?? "",
                Document = extractSection["Selection"]?[3]?["Document"]?.ToString() ?? ""
            };
        }

        private async Task<string[]> ProcessFiles(string parentPath)
        {
            bool processSubfolders = convertSettings.Subfolder;

            // Get files based on whether subfolder processing is allowed
            var files = processSubfolders
                ? Directory.GetFiles(parentPath, "*.*", SearchOption.AllDirectories)
                : Directory.GetFiles(parentPath);

            totalFiles = files.Count();

            await Task.CompletedTask; // Ensure the method is awaited

            return files; // Return the list of file paths
        }

        public void Delete_Folders(string folderPath)
        {
            foreach (var directory in Directory.GetDirectories(folderPath))
            {
                Delete_Folders(directory); // Recursively delete empty subfolders

                // If the directory is empty after processing subfolders, delete it
                if (Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0)
                {
                    Directory.Delete(directory);
                }
            }
        }

    }


}
public class ConvertSettings
{
    public bool Delete { get; set; }
    public bool Subfolder { get; set; }
    public bool Folder { get; set; }
    public string Image { get; set; }
    public string Video { get; set; }
    public string Audio { get; set; }
    public string Document { get; set; }
}
