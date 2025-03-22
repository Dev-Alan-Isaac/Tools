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
using System.Windows.Forms.VisualStyles;
using System.Diagnostics;

namespace Tools
{
    public partial class UserControl_Extract : UserControl
    {
        private ExtractSettings extractSettings;
        private string PathSort;
        private string[] files;
        private int totalFiles = 0;

        public UserControl_Extract()
        {
            InitializeComponent();
        }

        private void UserControl_Extract_Load(object sender, EventArgs e)
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

        private void button_Play_Click(object sender, EventArgs e)
        {
            // Clear the TextBox logs before adding new lines
            textBox_Logs.Invoke(new Action(() =>
            {
                textBox_Logs.Clear();
            }));

            if (radioButton_Extract.Checked)
            {
                Task.Run(() => Extract(files));
            }
            else if (radioButton_Metadata.Checked)
            {
                Task.Run(() => Metadata(files));
            }
            else
            {
                MessageBox.Show("Please select an option to proceed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Perform delete action if enabled in settings
            if (extractSettings.Delete)
            {
                Delete_Folders(PathSort);
            }
        }

        // Private Functions
        private async Task Extract(string[] files)
        {
            if (string.IsNullOrWhiteSpace(PathSort) || !Directory.Exists(PathSort))
            {
                Debug.WriteLine("PathSort is invalid or does not exist.");
                return;
            }

            string destinationPath;

            if (extractSettings.Folder)
            {
                destinationPath = Path.Combine(PathSort, "Extracted");

                if (!Directory.Exists(destinationPath))
                {
                    Directory.CreateDirectory(destinationPath); // Create the "Extracted" folder once
                }
            }
            else
            {
                destinationPath = PathSort; // Use PathSort directly as the destination
            }

            foreach (var file in files)
            {
                await MoveFileAsync(file, destinationPath); // Move all files to the determined destination
            }
        }

        private async Task Metadata(string[] files)
        {
            if (extractSettings.Text)
            {
                foreach (var file in files)
                {
                    try
                    {
                        string metadataFilePath = Path.Combine(PathSort, $"{Path.GetFileNameWithoutExtension(file)}_metadata.txt");

                        using (StreamWriter writer = new StreamWriter(metadataFilePath, false))
                        {
                            if (file.EndsWith(".doc") || file.EndsWith(".docx"))
                            {
                                // Example for reading metadata from Word documents
                                writer.WriteLine($"File: {file}");
                                writer.WriteLine("Metadata: Word document example");
                                // You can use libraries like OpenXML or Aspose for detailed metadata extraction
                            }
                            else if (file.EndsWith(".png") || file.EndsWith(".jpg"))
                            {
                                // Example for reading metadata from images
                                writer.WriteLine($"File: {file}");
                                writer.WriteLine("Metadata: Image example");
                                // You can use libraries like ImageProcessor or MetadataExtractor here
                            }
                            else
                            {
                                writer.WriteLine($"File: {file}");
                                writer.WriteLine("Metadata extraction not supported for this file type.");
                            }
                        }

                        Debug.WriteLine($"Metadata saved to: {metadataFilePath}");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Error processing file {file}: {ex.Message}");
                    }
                }
            }
            if (extractSettings.Window)
            {

            }
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
            JObject extractSection = (JObject)jsonObject["Extract"];
            Set_ClassValues(extractSection);
        }

        private void Set_ClassValues(JObject extractSection)
        {
            extractSettings = new ExtractSettings
            {
                // Extracting values from "Additional"
                Delete = extractSection["Additional"]?[0]?["Delete"]?.ToObject<bool>() ?? false,
                Subfolder = extractSection["Additional"]?[1]?["Subfolder"]?.ToObject<bool>() ?? false,
                Folder = extractSection["Additional"]?[2]?["Folder"]?.ToObject<bool>() ?? false,

                // Extracting values from "Metadata"
                Window = extractSection["Metadata"]?[0]?["Window"]?.ToObject<bool>() ?? false,
                Text = extractSection["Metadata"]?[1]?["Text"]?.ToObject<bool>() ?? false
            };
        }

        private async Task<string[]> ProcessFiles(string parentPath)
        {
            bool processSubfolders = extractSettings.Subfolder;

            // Get files based on whether subfolder processing is allowed
            var files = processSubfolders
                ? Directory.GetFiles(parentPath, "*.*", SearchOption.AllDirectories)
                : Directory.GetFiles(parentPath);

            totalFiles = files.Count();

            await Task.CompletedTask; // Ensure the method is awaited

            return files; // Return the list of file paths
        }

        private async Task MoveFileAsync(string sourcePath, string destinationDirectory)
        {
            // Ensure the source file exists
            if (!File.Exists(sourcePath))
            {
                textBox_Logs.Invoke(new Action(() =>
                {
                    textBox_Logs.AppendText($"Source file \"{sourcePath}\" does not exist.{Environment.NewLine}");
                }));
                return;
            }

            // Ensure the destination directory exists
            if (!Directory.Exists(destinationDirectory))
            {
                Directory.CreateDirectory(destinationDirectory);
            }

            // Construct the destination file path (using the same file name)
            string fileName = Path.GetFileName(sourcePath);
            string newDestinationPath = Path.Combine(destinationDirectory, fileName);

            // Handle duplicate file names by appending a counter
            int fileCount = 1;
            while (File.Exists(newDestinationPath))
            {
                string fileExtension = Path.GetExtension(fileName);
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                newDestinationPath = Path.Combine(destinationDirectory, $"{fileNameWithoutExtension} ({fileCount++}){fileExtension}");
            }

            // Log the destination path to the TextBox
            textBox_Logs.Invoke(new Action(() =>
            {
                textBox_Logs.AppendText($"{Environment.NewLine}"); // Add a separator line
                textBox_Logs.AppendText($"Moving file \"{fileName}\" to: {newDestinationPath}{Environment.NewLine}");
            }));

            // Move the file
            await Task.Run(() => File.Move(sourcePath, newDestinationPath));
        }

        private void Delete_Folders(string path)
        {
            try
            {
                // Check if the directory exists
                if (!Directory.Exists(path))
                {
                    MessageBox.Show($"The path \"{path}\" does not exist or has already been deleted.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var directory in Directory.GetDirectories(path))
                {
                    Delete_Folders(directory);

                    // Check if the directory is empty
                    if (Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0)
                    {
                        Directory.Delete(directory);
                    }
                }

                // Check if the root directory is empty (optional)
                if (Directory.GetFiles(path).Length == 0 && Directory.GetDirectories(path).Length == 0)
                {
                    Directory.Delete(path);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

public class ExtractSettings
{
    public bool Delete { get; set; }
    public bool Subfolder { get; set; }
    public bool Folder { get; set; }
    public bool Window { get; set; }
    public bool Text { get; set; }
}
