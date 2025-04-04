using Newtonsoft.Json.Linq;
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

        private async void button_Play_Click(object sender, EventArgs e)
        {
            // Clear the TextBox logs before adding new lines
            textBox_Logs.Invoke(new Action(() =>
            {
                textBox_Logs.Clear();
            }));

            Task processingTask = null;

            if (radioButton_Extract.Checked)
            {
                processingTask = Task.Run(() => Extract(files));
            }
            else if (radioButton_Metadata.Checked)
            {
                processingTask = Task.Run(() => Metadata(files));
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
                if (extractSettings.Delete)
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
        private async Task Extract(string[] files)
        {
            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = files.Length;
                progressBar1.Value = 0;
            }));

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

            // Check if the folder was deleted by the Delete_Folders function
            if (!Directory.Exists(destinationPath))
            {
                textBox_Logs.Invoke(new Action(() =>
                {
                    textBox_Logs.AppendText($"Destination path \"{destinationPath}\" has been deleted.");
                }));
                return;
            }

            foreach (var file in files)
            {
                if (File.Exists(file)) // Check if the file exists before moving it
                {

                    // Increment the progress bar
                    progressBar1.Invoke(new Action(() =>
                    {
                        progressBar1.Value++;
                    }));

                    await MoveFileAsync(file, destinationPath); // Move all files to the determined destination
                }
                else
                {
                    textBox_Logs.Invoke(new Action(() =>
                    {
                        textBox_Logs.AppendText($"Source file \"{file}\" does not exist or has already been moved.");
                    }));
                }
            }

            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Value = 0;
            }));

            MessageBox.Show("Files organization completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async Task Metadata(string[] files)
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
                Directory.CreateDirectory(destinationDirectory); // Re-create the destination directory if needed
            }

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
            try
            {
                await Task.Run(() => File.Move(sourcePath, newDestinationPath));
            }
            catch (Exception ex)
            {
                textBox_Logs.Invoke(new Action(() =>
                {
                    textBox_Logs.AppendText($"Error moving file \"{fileName}\": {ex.Message}{Environment.NewLine}");
                }));
            }
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

public class ExtractSettings
{
    public bool Delete { get; set; }
    public bool Subfolder { get; set; }
    public bool Folder { get; set; }
}
