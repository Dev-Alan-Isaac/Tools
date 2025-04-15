using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Security.Cryptography;
using NReco.VideoInfo;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using RestSharp.Validation;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Font = System.Drawing.Font;
using System.Collections.Concurrent;
using Microsoft.VisualBasic.Devices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using System.Runtime.CompilerServices;
using System.Text;
using System;
using TheArtOfDev.HtmlRenderer.Adapters.Entities;
using Microsoft.VisualBasic.ApplicationServices;
using System.Windows.Forms;
using System.Security.Policy;

namespace Tools
{
    public partial class UserControl_Filter : UserControl
    {
        private string PathSort;
        private FilterSettings filterSettings;
        private int totalFiles = 0;
        private string[] files;

        public UserControl_Filter()
        {
            InitializeComponent();
            PathSort = string.Empty; // Initialize PathSort
            filterSettings = new FilterSettings(); // Initialize filterSettings
            files = Array.Empty<string>(); // Initialize files
        }

        private void UserControl_Filter_Load(object sender, EventArgs e)
        {
            ReloadSettings();
        }

        private void Get_FilterSection(string settingPath)
        {
            string jsonContent = File.ReadAllText(settingPath);
            JObject jsonObject = JObject.Parse(jsonContent);
            JObject filterSection = (JObject)jsonObject["Filter"];
            Set_ClassValues(filterSection);
        }

        private void Set_ClassValues(JObject filterSection)
        {
            filterSettings = new FilterSettings();

            // Map Types Section
            JArray typesArray = (JArray)filterSection["Types"];
            if (typesArray != null)
            {
                foreach (var type in typesArray)
                {
                    if (type["Image"] != null) filterSettings.Image = (bool)type["Image"];
                    if (type["Video"] != null) filterSettings.Video = (bool)type["Video"];
                    if (type["Document"] != null) filterSettings.Document = (bool)type["Document"];
                    if (type["Audio"] != null) filterSettings.Audio = (bool)type["Audio"];
                    if (type["Archive"] != null) filterSettings.Archive = (bool)type["Archive"];
                    if (type["Executable"] != null) filterSettings.Executable = (bool)type["Executable"];
                    if (type["Other"] != null) filterSettings.Other = (bool)type["Other"];

                    if (type["Image_List"] != null)
                        filterSettings.Image_List = type["Image_List"].ToObject<List<string>>();
                    if (type["Video_List"] != null)
                        filterSettings.Video_List = type["Video_List"].ToObject<List<string>>();
                    if (type["Document_List"] != null)
                        filterSettings.Document_List = type["Document_List"].ToObject<List<string>>();
                    if (type["Audio_List"] != null)
                        filterSettings.Audio_List = type["Audio_List"].ToObject<List<string>>();
                    if (type["Archive_List"] != null)
                        filterSettings.Archive_List = type["Archive_List"].ToObject<List<string>>();
                    if (type["Executable_List"] != null)
                        filterSettings.Executable_List = type["Executable_List"].ToObject<List<string>>();
                    if (type["Other_List"] != null)
                        filterSettings.Other_List = type["Other_List"].ToObject<List<string>>();
                }
            }

            // Map Additional Section
            JArray additionalArray = (JArray)filterSection["Additional"];
            if (additionalArray != null)
            {
                foreach (var additional in additionalArray)
                {
                    if (additional["Delete"] != null) filterSettings.Delete = (bool)additional["Delete"];
                    if (additional["Subfolder"] != null) filterSettings.Subfolder = (bool)additional["Subfolder"];
                }
            }

            // Map Date Section
            JArray dateArray = (JArray)filterSection["Date"];
            if (dateArray != null)
            {
                foreach (var date in dateArray)
                {
                    if (date["Creation"] != null) filterSettings.Creation = (bool)date["Creation"];
                    if (date["Access"] != null) filterSettings.Access = (bool)date["Access"];
                    if (date["Modify"] != null) filterSettings.Modify = (bool)date["Modify"];
                }
            }

            // Map Media Section
            JArray mediaArray = (JArray)filterSection["Media"];
            if (mediaArray != null)
            {
                foreach (var media in mediaArray)
                {
                    if (media["Duration"] != null) filterSettings.Duration = (bool)media["Duration"];
                    if (media["Resolution"] != null) filterSettings.Resolution = (bool)media["Resolution"];
                    if (media["FrameRate"] != null) filterSettings.FrameRate = (bool)media["FrameRate"];
                    if (media["Codec"] != null) filterSettings.Codec = (bool)media["Codec"];
                    if (media["AspectRatio"] != null) filterSettings.AspectRatio = (bool)media["AspectRatio"];
                }
            }

            // Map Tags Section
            JArray tagsArray = (JArray)filterSection["Tags"];
            if (tagsArray != null)
            {
                foreach (var tag in tagsArray)
                {
                    if (tag["Tag_List"] != null)
                        filterSettings.Tag_List = tag["Tag_List"].ToObject<List<string>>();
                }
            }

            // Map Size Section
            JArray sizeArray = (JArray)filterSection["Size"];
            if (sizeArray != null)
            {
                foreach (var size in sizeArray)
                {
                    if (size["Range"] != null) filterSettings.Range = (bool)size["Range"];
                    if (size["Dynamic"] != null) filterSettings.Dynamic = (bool)size["Dynamic"];

                    if (size["Range_List"] != null)
                    {
                        filterSettings.Range_List = new List<Dictionary<string, List<string>>>();
                        JArray rangeListArray = (JArray)size["Range_List"];
                        foreach (var range in rangeListArray)
                        {
                            var rangeDict = new Dictionary<string, List<string>>();

                            if (range["Small"] != null)
                                rangeDict["Small"] = range["Small"].ToObject<List<string>>();
                            if (range["Medium"] != null)
                                rangeDict["Medium"] = range["Medium"].ToObject<List<string>>();
                            if (range["Large"] != null)
                                rangeDict["Large"] = range["Large"].ToObject<List<string>>();
                            if (range["Extra_Large"] != null)
                                rangeDict["Extra_Large"] = range["Extra_Large"].ToObject<List<string>>();

                            filterSettings.Range_List.Add(rangeDict);
                        }
                    }
                }
            }

            // Map Name Section
            JArray nameArray = (JArray)filterSection["Name"];
            if (nameArray != null)
            {
                foreach (var name in nameArray)
                {
                    if (name["Caps"] != null) filterSettings.Caps = (bool)name["Caps"];
                    if (name["Chars"] != null) filterSettings.Chars = (bool)name["Chars"];
                }
            }

            Debug.WriteLine("Filter Settings Populated Successfully");
        }

        private void button_Path_Click(object sender, EventArgs e)
        {
            // Clear the TextBox logs before adding new lines
            textBox_Logs.Invoke(new Action(() =>
            {
                textBox_Logs.Clear();
            }));

            // Open a folder browser dialog to select the path
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
            // Get the checkbox states
            Dictionary<string, bool> checkboxStates = Get_CheckboxState();

            // Define the filter actions
            Dictionary<string, Action> filterActions = new Dictionary<string, Action>
            {
                { "type", () => Task.Run(() => FilterType(files)) },
                { "size", () => Task.Run(() => FilterSize(files)) },
                { "date", () => Task.Run(() => FilterDate(files)) },
                { "name", () => Task.Run(() => FilterName(files)) },
                { "extension", () => Task.Run(() => FilterExtension(files)) },
                { "tags", () => Task.Run(() => FilterTags(files)) },
                { "media", () => Task.Run(() => FilterMedia(files)) },
                { "scan", () => Task.Run(() => Scan(files)) },
                { "duplicate", () => Task.Run(() => Duplicate(files)) }
            };

            // Check if at least one checkbox is selected
            if (!checkboxStates.Values.Any(selected => selected))
            {
                MessageBox.Show("Please select at least one option.", "No Options Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Log the start of the process
            textBox_Logs.Invoke(new Action(() =>
            {
                textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                textBox_Logs.SelectionLength = 0;
                textBox_Logs.SelectionColor = Color.Blue; // Set log color
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                textBox_Logs.AppendText($"{Environment.NewLine}Processing started...{Environment.NewLine}");
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
            }));

            // Loop through the checkbox states and execute the corresponding actions
            foreach (var state in checkboxStates)
            {
                if (state.Value && filterActions.ContainsKey(state.Key))
                {
                    filterActions[state.Key].Invoke();
                }
            }

            // Perform delete action if enabled in settings
            if (filterSettings.Delete)
            {
                Delete_Folders(PathSort);
            }
        }

        // Filters - Functions
        private async Task FilterType(string[] files)
        {
            // Define a dictionary to map file types to their corresponding settings and lists
            var typeToSettings = new Dictionary<string, (bool IsEnabled, List<string> Extensions)>
            {
                { "Image", (filterSettings.Image, filterSettings.Image_List) },
                { "Video", (filterSettings.Video, filterSettings.Video_List) },
                { "Document", (filterSettings.Document, filterSettings.Document_List) },
                { "Audio", (filterSettings.Audio, filterSettings.Audio_List) },
                { "Archive", (filterSettings.Archive, filterSettings.Archive_List) },
                { "Executable", (filterSettings.Executable, filterSettings.Executable_List) },
                { "Other", (filterSettings.Other, filterSettings.Other_List) }
            };

            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = files.Length;
                progressBar1.Value = 0;
            }));

            foreach (string file in files)
            {
                string fileExtension = Path.GetExtension(file).TrimStart('.').ToLower();

                foreach (var type in typeToSettings)
                {
                    if (type.Value.IsEnabled && type.Value.Extensions.Contains(fileExtension))
                    {
                        // Create a folder based on the file type if it doesn't exist
                        string folderPath = Path.Combine(PathSort, type.Key);
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        // Determine the destination file path
                        string destinationFilePath = Path.Combine(folderPath, Path.GetFileName(file));
                        int counter = 1;

                        // If the file already exists, append a unique identifier to avoid conflicts
                        while (File.Exists(destinationFilePath))
                        {
                            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                            string newFileName = $"{fileNameWithoutExtension} ({counter}){Path.GetExtension(file)}";
                            destinationFilePath = Path.Combine(folderPath, newFileName);
                            counter++;
                        }

                        // Move the file to the appropriate folder
                        await MoveFileAsync(file, destinationFilePath);

                        // Log the moved file's details in textBox_Logs
                        textBox_Logs.Invoke(new Action(() =>
                        {
                            textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                            textBox_Logs.SelectionLength = 0;
                            textBox_Logs.SelectionColor = Color.Blue; // Set log color
                            textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                            textBox_Logs.AppendText($"{Environment.NewLine}File Moved Successfully!{Environment.NewLine}");
                            textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                            textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
                            textBox_Logs.AppendText($"File Name: {Path.GetFileName(file)}{Environment.NewLine}");
                            textBox_Logs.AppendText($"Type: {type.Key}{Environment.NewLine}");
                            textBox_Logs.AppendText($"New Path: {destinationFilePath}{Environment.NewLine}");
                        }));

                        // Update progress bar
                        progressBar1.Invoke(new Action(() =>
                        {
                            progressBar1.Value++;
                        }));

                        break; // Exit the loop once a match is found
                    }
                }
            }

            // Reset progress bar
            progressBar1.Invoke(new Action(() => { progressBar1.Value = 0; }));

            // Play a sound notification on completion
            System.Media.SystemSounds.Asterisk.Play(); // System sound (Windows default)

            // Log completion in `textBox_Logs`
            textBox_Logs.Invoke(new Action(() =>
            {
                textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                textBox_Logs.SelectionLength = 0;
                textBox_Logs.SelectionColor = Color.Green; // Green for success
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                textBox_Logs.AppendText($"{Environment.NewLine}File filtering and organization completed!");
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
            }));
        }

        private async Task FilterSize(string[] files)
        {
            if (filterSettings.Range)
            {
                await Range(files, filterSettings.Range_List);
            }
            else if (filterSettings.Dynamic)
            {
                await Dynamic(files);
            }
        }

        private async Task Range(string[] files, List<Dictionary<string, List<string>>> Range_List)
        {
            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = files.Length;
                progressBar1.Value = 0;
            }));

            foreach (var kvp in Range_List)
            {
                foreach (var key in kvp.Keys)
                {
                    var sizeList = kvp[key];

                    // Convert the range limits to bytes
                    long minSize = sizeList.Count > 1 ? ConvertToBytes(sizeList[0], sizeList[1]) : 0;
                    long maxSize = sizeList.Count > 3 ? ConvertToBytes(sizeList[2], sizeList[3]) : long.MaxValue;

                    // Process each file
                    foreach (string file in files)
                    {
                        long fileSize = new FileInfo(file).Length;

                        // Check if the file falls within the size range
                        if (fileSize >= minSize && fileSize <= maxSize)
                        {
                            // Determine the folder name based on the range category
                            string folderPath = Path.Combine(PathSort, key);

                            // Create the folder if it doesn't exist
                            if (!Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                            }

                            // Move the file to the folder
                            string destinationFilePath = Path.Combine(folderPath, Path.GetFileName(file));

                            // Create a size-specific subfolder
                            long sizeCategoryValue = ConvertToCategoryValue(fileSize);
                            string sizeSubfolderPath = Path.Combine(folderPath, sizeCategoryValue.ToString());

                            // Ensure the subfolder exists
                            if (!Directory.Exists(sizeSubfolderPath))
                            {
                                Directory.CreateDirectory(sizeSubfolderPath);
                            }

                            // Update the destination path for the specific subfolder
                            destinationFilePath = Path.Combine(sizeSubfolderPath, Path.GetFileName(file));
                            await MoveFileAsync(file, destinationFilePath);

                            // Log the moved file's details in `textBox_Logs`
                            textBox_Logs.Invoke(new Action(() =>
                            {
                                textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                                textBox_Logs.SelectionLength = 0;
                                textBox_Logs.SelectionColor = Color.Blue; // Set log color
                                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                                textBox_Logs.AppendText($"{Environment.NewLine}File Moved Successfully!{Environment.NewLine}");
                                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                                textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
                                textBox_Logs.AppendText($"File Name: {Path.GetFileName(file)}{Environment.NewLine}");
                                textBox_Logs.AppendText($"Size Category: {key}{Environment.NewLine}");
                                textBox_Logs.AppendText($"New Path: {destinationFilePath}{Environment.NewLine}");
                            }));

                            // Update progress bar
                            progressBar1.Invoke(new Action(() =>
                            {
                                progressBar1.Value++;
                            }));
                        }
                    }
                }
            }

            // Reset progress bar
            progressBar1.Invoke(new Action(() => { progressBar1.Value = 0; }));

            // Play a sound notification on completion
            System.Media.SystemSounds.Asterisk.Play(); // Windows default system sound

            // Log completion in `textBox_Logs`
            textBox_Logs.Invoke(new Action(() =>
            {
                textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                textBox_Logs.SelectionLength = 0;
                textBox_Logs.SelectionColor = Color.Green; // Green for success
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                textBox_Logs.AppendText($"{Environment.NewLine}File filtering and organization completed!");
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
            }));
        }

        private long ConvertToCategoryValue(long fileSize)
        {
            long kbThreshold = 1024; // 1 KB
            long mbThreshold = 1024 * kbThreshold; // 1 MB
            long gbThreshold = 1024 * mbThreshold; // 1 GB

            if (fileSize < mbThreshold)
            {
                return fileSize / kbThreshold; // KB
            }
            else if (fileSize < gbThreshold)
            {
                return fileSize / mbThreshold; // MB
            }
            else
            {
                return fileSize / gbThreshold; // GB
            }
        }

        private async Task Dynamic(string[] files)
        {
            // Define size thresholds in bytes
            long kbThreshold = 1024; // 1 KB
            long mbThreshold = 1024 * kbThreshold; // 1 MB
            long gbThreshold = 1024 * mbThreshold; // 1 GB
            long tbThreshold = 1024 * gbThreshold; // 1 TB

            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = files.Length;
                progressBar1.Value = 0;
            }));

            foreach (string file in files)
            {
                // Get the file size
                long fileSize = new FileInfo(file).Length;

                // Determine the size category and size value
                string sizeCategory;
                long sizeValue;

                if (fileSize < mbThreshold)
                {
                    sizeCategory = "KB";
                    sizeValue = fileSize / kbThreshold; // Convert to KB
                }
                else if (fileSize < gbThreshold)
                {
                    sizeCategory = "MB";
                    sizeValue = fileSize / mbThreshold; // Convert to MB
                }
                else if (fileSize < tbThreshold)
                {
                    sizeCategory = "GB";
                    sizeValue = fileSize / gbThreshold; // Convert to GB
                }
                else
                {
                    sizeCategory = "TB";
                    sizeValue = fileSize / tbThreshold; // Convert to TB
                }

                // Build the directory path: ParentFolder > Size Category (e.g., GB) > Size Value (e.g., 1)
                string sizeCategoryFolder = Path.Combine(PathSort, sizeCategory);
                string sizeValueFolder = Path.Combine(sizeCategoryFolder, sizeValue.ToString());

                // Create the folders if they don't exist
                if (!Directory.Exists(sizeValueFolder))
                {
                    Directory.CreateDirectory(sizeValueFolder);
                }

                // Move the file to the appropriate folder
                string destinationFilePath = Path.Combine(sizeValueFolder, Path.GetFileName(file));
                await MoveFileAsync(file, destinationFilePath);

                // Log the moved file's details in `textBox_Logs`
                textBox_Logs.Invoke(new Action(() =>
                {
                    textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                    textBox_Logs.SelectionLength = 0;
                    textBox_Logs.SelectionColor = Color.Blue; // Set log color
                    textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                    textBox_Logs.AppendText($"{Environment.NewLine}File Moved Successfully!{Environment.NewLine}");
                    textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                    textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
                    textBox_Logs.AppendText($"File Name: {Path.GetFileName(file)}{Environment.NewLine}");
                    textBox_Logs.AppendText($"Size Category: {sizeCategory}{Environment.NewLine}");
                    textBox_Logs.AppendText($"New Path: {destinationFilePath}{Environment.NewLine}");
                }));

                progressBar1.Invoke(new Action(() => { progressBar1.Value++; }));
            }

            progressBar1.Invoke(new Action(() => { progressBar1.Value = 0; }));

            // Play a sound notification on completion
            System.Media.SystemSounds.Asterisk.Play(); // Windows default system sound

            // Log completion in `textBox_Logs`
            textBox_Logs.Invoke(new Action(() =>
            {
                textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                textBox_Logs.SelectionLength = 0;
                textBox_Logs.SelectionColor = Color.Green; // Green for success
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                textBox_Logs.AppendText($"{Environment.NewLine}File filtering and organization completed!");
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
            }));
        }

        private long ConvertToBytes(string sizeValue, string unit)
        {
            double size = double.Parse(sizeValue);

            switch (unit.ToUpper())
            {
                case "KB":
                    return (long)(size * 1024);
                case "MB":
                    return (long)(size * 1024 * 1024);
                case "GB":
                    return (long)(size * 1024 * 1024 * 1024);
                case "TB":
                    return (long)(size * 1024 * 1024 * 1024 * 1024);
                default:
                    return (long)size;
            }
        }

        private async Task FilterDate(string[] files)
        {
            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = files.Length;
                progressBar1.Value = 0;
            }));

            if (filterSettings.Creation)
            {
                await Creation(files);
            }
            else if (filterSettings.Access)
            {
                await Access(files);
            }
            else if (filterSettings.Modify)
            {
                await Modify(files);
            }
        }

        private async Task Creation(string[] files)
        {
            string creationDateParentFolder = Path.Combine(PathSort, "Creation date");

            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = files.Length;
                progressBar1.Value = 0;
            }));

            foreach (var file in files)
            {
                DateTime creationDate = File.GetCreationTime(file);
                string creationDateFolder = Path.Combine(creationDateParentFolder, creationDate.ToString("yyyy-MM-dd"));

                if (!Directory.Exists(creationDateFolder))
                {
                    Directory.CreateDirectory(creationDateFolder);
                }

                string fileName = Path.GetFileName(file);
                string destinationPath = Path.Combine(creationDateFolder, fileName);

                // Handle files with the same name
                int fileCount = 1;
                string newDestinationPath = destinationPath;
                while (File.Exists(newDestinationPath))
                {
                    string fileExtension = Path.GetExtension(fileName);
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                    newDestinationPath = Path.Combine(creationDateFolder, $"{fileNameWithoutExtension} ({fileCount++}){fileExtension}");
                }

                progressBar1.Invoke(new Action(() => { progressBar1.Value++; }));

                await MoveFileAsync(file, newDestinationPath);

                // Log the moved file's details in `textBox_Logs`
                textBox_Logs.Invoke(new Action(() =>
                {
                    textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                    textBox_Logs.SelectionLength = 0;
                    textBox_Logs.SelectionColor = Color.Blue; // Set log color
                    textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                    textBox_Logs.AppendText($"{Environment.NewLine}File Moved Successfully!{Environment.NewLine}");
                    textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                    textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
                    textBox_Logs.AppendText($"File Name: {fileName}{Environment.NewLine}");
                    textBox_Logs.AppendText($"Creation Date: {creationDate:yyyy-MM-dd}{Environment.NewLine}");
                    textBox_Logs.AppendText($"New Path: {newDestinationPath}{Environment.NewLine}");
                }));
            }

            progressBar1.Invoke(new Action(() => { progressBar1.Value = 0; }));

            // Play a sound notification on completion
            System.Media.SystemSounds.Asterisk.Play(); // Windows default system sound

            // Log completion in `textBox_Logs`
            textBox_Logs.Invoke(new Action(() =>
            {
                textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                textBox_Logs.SelectionLength = 0;
                textBox_Logs.SelectionColor = Color.Green; // Green for success
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                textBox_Logs.AppendText($"{Environment.NewLine}File filtering and organization completed!");
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
            }));
        }

        private async Task Access(string[] files)
        {
            string accessDateParentFolder = Path.Combine(PathSort, "Access date");

            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = files.Length;
                progressBar1.Value = 0;
            }));

            foreach (var file in files)
            {
                DateTime accessDate = File.GetLastAccessTime(file);
                string accessDateFolder = Path.Combine(accessDateParentFolder, accessDate.ToString("yyyy-MM-dd"));

                if (!Directory.Exists(accessDateFolder))
                {
                    Directory.CreateDirectory(accessDateFolder);
                }

                string fileName = Path.GetFileName(file);
                string destinationPath = Path.Combine(accessDateFolder, fileName);

                // Handle files with the same name
                int fileCount = 1;
                string newDestinationPath = destinationPath;
                while (File.Exists(newDestinationPath))
                {
                    string fileExtension = Path.GetExtension(fileName);
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                    newDestinationPath = Path.Combine(accessDateFolder, $"{fileNameWithoutExtension} ({fileCount++}){fileExtension}");
                }

                progressBar1.Invoke(new Action(() => { progressBar1.Value++; }));

                await MoveFileAsync(file, newDestinationPath);

                // Log the moved file's details in `textBox_Logs`
                textBox_Logs.Invoke(new Action(() =>
                {
                    textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                    textBox_Logs.SelectionLength = 0;
                    textBox_Logs.SelectionColor = Color.Blue; // Set log color
                    textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                    textBox_Logs.AppendText($"{Environment.NewLine}File Moved Successfully!{Environment.NewLine}");
                    textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                    textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
                    textBox_Logs.AppendText($"File Name: {fileName}{Environment.NewLine}");
                    textBox_Logs.AppendText($"Access Date: {accessDate:yyyy-MM-dd}{Environment.NewLine}");
                    textBox_Logs.AppendText($"New Path: {newDestinationPath}{Environment.NewLine}");
                }));
            }

            progressBar1.Invoke(new Action(() => { progressBar1.Value = 0; }));

            // Play a sound notification on completion
            System.Media.SystemSounds.Asterisk.Play(); // Windows default system sound

            // Log completion in `textBox_Logs`
            textBox_Logs.Invoke(new Action(() =>
            {
                textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                textBox_Logs.SelectionLength = 0;
                textBox_Logs.SelectionColor = Color.Green; // Green for success
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                textBox_Logs.AppendText($"{Environment.NewLine}File filtering and organization completed!");
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
            }));
        }

        private async Task Modify(string[] files)
        {
            string modifyDateParentFolder = Path.Combine(PathSort, "Modify date");

            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = files.Length;
                progressBar1.Value = 0;
            }));

            foreach (var file in files)
            {
                DateTime modifyDate = File.GetLastWriteTime(file);
                string modifyDateFolder = Path.Combine(modifyDateParentFolder, modifyDate.ToString("yyyy-MM-dd"));

                if (!Directory.Exists(modifyDateFolder))
                {
                    Directory.CreateDirectory(modifyDateFolder);
                }

                string fileName = Path.GetFileName(file);
                string destinationPath = Path.Combine(modifyDateFolder, fileName);

                // Handle files with the same name
                int fileCount = 1;
                string newDestinationPath = destinationPath;
                while (File.Exists(newDestinationPath))
                {
                    string fileExtension = Path.GetExtension(fileName);
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                    newDestinationPath = Path.Combine(modifyDateFolder, $"{fileNameWithoutExtension} ({fileCount++}){fileExtension}");
                }

                progressBar1.Invoke(new Action(() => { progressBar1.Value++; }));

                await MoveFileAsync(file, newDestinationPath);

                // Log the moved file's details in `textBox_Logs`
                textBox_Logs.Invoke(new Action(() =>
                {
                    textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                    textBox_Logs.SelectionLength = 0;
                    textBox_Logs.SelectionColor = Color.Blue; // Set log color
                    textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                    textBox_Logs.AppendText($"{Environment.NewLine}File Moved Successfully!{Environment.NewLine}");
                    textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                    textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
                    textBox_Logs.AppendText($"File Name: {fileName}{Environment.NewLine}");
                    textBox_Logs.AppendText($"Modify Date: {modifyDate:yyyy-MM-dd}{Environment.NewLine}");
                    textBox_Logs.AppendText($"New Path: {newDestinationPath}{Environment.NewLine}");
                }));
            }

            progressBar1.Invoke(new Action(() => { progressBar1.Value = 0; }));

            // Play a sound notification on completion
            System.Media.SystemSounds.Asterisk.Play(); // Windows default system sound

            // Log completion in `textBox_Logs`
            textBox_Logs.Invoke(new Action(() =>
            {
                textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                textBox_Logs.SelectionLength = 0;
                textBox_Logs.SelectionColor = Color.Green; // Green for success
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                textBox_Logs.AppendText($"{Environment.NewLine}File filtering and organization completed!");
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
            }));
        }

        private async Task FilterName(string[] files)
        {
            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = files.Length;
                progressBar1.Value = 0;
            }));

            if (filterSettings.Chars)
            {
                await NonASCII(files);
            }
            else if (filterSettings.Caps)
            {
                await IgnoreCaps(files);
            }
        }

        private async Task NonASCII(string[] files)
        {
            string folderPath = Path.Combine(PathSort, "Characters");

            // Create the main folder if it doesn't exist
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Create subfolders for ASCII and Non-ASCII characters
            string asciiFolder = Path.Combine(folderPath, "ASCII");
            string nonAsciiFolder = Path.Combine(folderPath, "Non-ASCII");

            if (!Directory.Exists(asciiFolder))
            {
                Directory.CreateDirectory(asciiFolder);
            }

            if (!Directory.Exists(nonAsciiFolder))
            {
                Directory.CreateDirectory(nonAsciiFolder);
            }

            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = files.Length;
                progressBar1.Value = 0;
            }));

            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file); // Declare it outside the try block

                try
                {
                    bool isAscii = fileName.All(c => c <= 127); // Check if filename contains non-ASCII characters

                    string destinationFolder = isAscii ? asciiFolder : nonAsciiFolder;
                    string destinationPath = Path.Combine(destinationFolder, fileName);

                    await MoveFileAsync(file, destinationPath);

                    // Log successful move
                    textBox_Logs.Invoke(new Action(() =>
                    {
                        textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                        textBox_Logs.SelectionLength = 0;
                        textBox_Logs.SelectionColor = isAscii ? Color.Green : Color.Red;
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                        textBox_Logs.AppendText($"{Environment.NewLine}File Moved Successfully!{Environment.NewLine}");
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                        textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
                        textBox_Logs.AppendText($"File Name: {fileName}{Environment.NewLine}");
                        textBox_Logs.AppendText($"Character Type: {(isAscii ? "ASCII" : "Non-ASCII")}{Environment.NewLine}");
                        textBox_Logs.AppendText($"New Path: {destinationPath}{Environment.NewLine}");
                    }));
                }
                catch (Exception ex)
                {
                    // Log errors without message box
                    textBox_Logs.Invoke(new Action(() =>
                    {
                        textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                        textBox_Logs.SelectionLength = 0;
                        textBox_Logs.SelectionColor = Color.Red;
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                        textBox_Logs.AppendText($"{Environment.NewLine}Error processing file:{Environment.NewLine}");
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                        textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
                        textBox_Logs.AppendText($"File Name: {fileName}{Environment.NewLine}"); // fileName is now accessible
                        textBox_Logs.AppendText($"Error: {ex.Message}{Environment.NewLine}");
                    }));
                }
            }

            progressBar1.Invoke(new Action(() => { progressBar1.Value = 0; }));

            // Play a sound notification on completion
            System.Media.SystemSounds.Asterisk.Play(); // Windows default system sound

            // Log completion in `textBox_Logs`
            textBox_Logs.Invoke(new Action(() =>
            {
                textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                textBox_Logs.SelectionLength = 0;
                textBox_Logs.SelectionColor = Color.Blue;
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                textBox_Logs.AppendText($"{Environment.NewLine}File filtering and organization completed!");
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
            }));
        }

        private async Task IgnoreCaps(string[] files)
        {
            string folderPath = Path.Combine(PathSort, "Characters");

            // Create the main folder if it doesn't exist
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = files.Length;
                progressBar1.Value = 0;
            }));

            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file); // Define fileName outside try block

                try
                {
                    // Determine the first letter (ignoring case)
                    char firstLetter = char.ToUpper(fileName[0]);

                    // Create a subfolder for this letter
                    string letterFolder = Path.Combine(folderPath, firstLetter.ToString());
                    if (!Directory.Exists(letterFolder))
                    {
                        Directory.CreateDirectory(letterFolder);
                    }

                    // Move the file to the corresponding folder
                    string destinationPath = Path.Combine(letterFolder, fileName);
                    await MoveFileAsync(file, destinationPath);

                    // Log the moved file's details in `textBox_Logs`
                    textBox_Logs.Invoke(new Action(() =>
                    {
                        textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                        textBox_Logs.SelectionLength = 0;
                        textBox_Logs.SelectionColor = Color.Blue; // Set log color
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                        textBox_Logs.AppendText($"{Environment.NewLine}File Moved Successfully!{Environment.NewLine}");
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                        textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
                        textBox_Logs.AppendText($"File Name: {fileName}{Environment.NewLine}");
                        textBox_Logs.AppendText($"First Letter (Ignoring Case): {firstLetter}{Environment.NewLine}");
                        textBox_Logs.AppendText($"New Path: {destinationPath}{Environment.NewLine}");
                    }));

                    progressBar1.Invoke(new Action(() => { progressBar1.Value++; }));
                }
                catch (Exception ex)
                {
                    // Log errors instead of using a message box
                    textBox_Logs.Invoke(new Action(() =>
                    {
                        textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                        textBox_Logs.SelectionLength = 0;
                        textBox_Logs.SelectionColor = Color.Red;
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                        textBox_Logs.AppendText($"{Environment.NewLine}Error processing file:{Environment.NewLine}");
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                        textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
                        textBox_Logs.AppendText($"File Name: {fileName}{Environment.NewLine}"); // fileName now accessible
                        textBox_Logs.AppendText($"Error: {ex.Message}{Environment.NewLine}");
                    }));
                }
            }

            progressBar1.Invoke(new Action(() => { progressBar1.Value = 0; }));

            // Play a sound notification on completion
            System.Media.SystemSounds.Asterisk.Play(); // Windows default system sound

            // Log completion in `textBox_Logs`
            textBox_Logs.Invoke(new Action(() =>
            {
                textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                textBox_Logs.SelectionLength = 0;
                textBox_Logs.SelectionColor = Color.Green;
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                textBox_Logs.AppendText($"{Environment.NewLine}File filtering and organization completed!");
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
            }));
        }

        private async Task FilterExtension(string[] files)
        {
            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = files.Length;
                progressBar1.Value = 0;
            }));

            foreach (var file in files)
            {
                string fileExtension = Path.GetExtension(file).TrimStart('.').ToLowerInvariant();
                string extensionFolder = Path.Combine(PathSort, fileExtension);

                if (!Directory.Exists(extensionFolder))
                {
                    Directory.CreateDirectory(extensionFolder);
                }

                string fileName = Path.GetFileName(file);
                string destinationPath = Path.Combine(extensionFolder, fileName);

                // Handle files with the same name
                int fileCount = 1;
                string newDestinationPath = destinationPath;
                while (File.Exists(newDestinationPath))
                {
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                    newDestinationPath = Path.Combine(extensionFolder, $"{fileNameWithoutExtension} ({fileCount++}).{fileExtension}");
                }

                progressBar1.Invoke(new Action(() => { progressBar1.Value++; }));

                await MoveFileAsync(file, newDestinationPath);

                // Log the moved file's details in `textBox_Logs`
                textBox_Logs.Invoke(new Action(() =>
                {
                    textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                    textBox_Logs.SelectionLength = 0;
                    textBox_Logs.SelectionColor = Color.Blue; // Set log color
                    textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                    textBox_Logs.AppendText($"{Environment.NewLine}File Moved Successfully!{Environment.NewLine}");
                    textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                    textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
                    textBox_Logs.AppendText($"File Name: {fileName}{Environment.NewLine}");
                    textBox_Logs.AppendText($"Extension Type: {fileExtension}{Environment.NewLine}");
                    textBox_Logs.AppendText($"New Path: {newDestinationPath}{Environment.NewLine}");
                }));
            }

            progressBar1.Invoke(new Action(() => { progressBar1.Value = 0; }));

            // Play a sound notification on completion
            System.Media.SystemSounds.Asterisk.Play(); // Windows default system sound

            // Log completion in `textBox_Logs`
            textBox_Logs.Invoke(new Action(() =>
            {
                textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                textBox_Logs.SelectionLength = 0;
                textBox_Logs.SelectionColor = Color.Green; // Green for success
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                textBox_Logs.AppendText($"{Environment.NewLine}File filtering and organization completed!");
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
            }));
        }

        private async Task FilterTags(string[] files)
        {
            var tagList = filterSettings.Tag_List;

            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = files.Length;
                progressBar1.Value = 0;
            }));

            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);

                foreach (var tag in tagList)
                {
                    if (fileName.IndexOf(tag, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        string tagsFolder = Path.Combine(Path.GetDirectoryName(file), "Tags", tag);

                        if (!Directory.Exists(tagsFolder))
                        {
                            Directory.CreateDirectory(tagsFolder);
                        }

                        string destinationPath = Path.Combine(tagsFolder, fileName);

                        // Handle files with the same name
                        int fileCount = 1;
                        string newDestinationPath = destinationPath;
                        while (File.Exists(newDestinationPath))
                        {
                            string fileExtension = Path.GetExtension(fileName);
                            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                            newDestinationPath = Path.Combine(tagsFolder, $"{fileNameWithoutExtension} ({fileCount++}){fileExtension}");
                        }

                        progressBar1.Invoke(new Action(() => { progressBar1.Value++; }));

                        await MoveFileAsync(file, newDestinationPath);

                        // Log the moved file's details in `textBox_Logs`
                        textBox_Logs.Invoke(new Action(() =>
                        {
                            textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                            textBox_Logs.SelectionLength = 0;
                            textBox_Logs.SelectionColor = Color.Blue; // Set log color
                            textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                            textBox_Logs.AppendText($"{Environment.NewLine}File Moved Successfully!{Environment.NewLine}");
                            textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                            textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
                            textBox_Logs.AppendText($"File Name: {fileName}{Environment.NewLine}");
                            textBox_Logs.AppendText($"Tag: {tag}{Environment.NewLine}");
                            textBox_Logs.AppendText($"New Path: {newDestinationPath}{Environment.NewLine}");
                        }));

                        break; // If a tag is found, stop checking other tags for this file
                    }
                }
            }

            progressBar1.Invoke(new Action(() => { progressBar1.Value = 0; }));

            // Play a sound notification on completion
            System.Media.SystemSounds.Asterisk.Play(); // Windows default system sound

            // Log completion in `textBox_Logs`
            textBox_Logs.Invoke(new Action(() =>
            {
                textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                textBox_Logs.SelectionLength = 0;
                textBox_Logs.SelectionColor = Color.Green;
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                textBox_Logs.AppendText($"{Environment.NewLine}File filtering and organization completed!");
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
            }));
        }

        private async Task FilterMedia(string[] files)
        {
            if (filterSettings.Duration)
            {
                await Duration(files);
            }
            else if (filterSettings.Resolution)
            {
                await Resolution(files);
            }
            else if (filterSettings.FrameRate)
            {
                await FrameRate(files);
            }
            else if (filterSettings.Codec)
            {
                await Codec(files);
            }
            else if (filterSettings.AspectRatio)
            {
                await AspectRatio(files);
            }
        }

        private bool IsVideoFile(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower().TrimStart('.'); // Remove the leading period
            return filterSettings.Video_List.Contains(extension);
        }

        private async Task Duration(string[] files)
        {
            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = files.Length;
                progressBar1.Value = 0;
            }));

            foreach (var file in files)
            {
                if (!IsVideoFile(file))
                {
                    continue;
                }

                string fileName = Path.GetFileName(file); // Define fileName outside try block

                try
                {
                    // Use NReco to get media info and determine the duration
                    var mediaInfo = new FFProbe().GetMediaInfo(file);
                    var duration = mediaInfo.Duration;

                    // Format duration as HH-mm-ss
                    var folderName = $"{duration:hh\\-mm\\-ss}";

                    var destinationFolder = Path.Combine(PathSort, folderName);
                    var destinationPath = Path.Combine(destinationFolder, fileName);

                    // Create the destination directory if it doesn't exist
                    if (!Directory.Exists(destinationFolder))
                    {
                        Directory.CreateDirectory(destinationFolder);
                    }

                    progressBar1.Invoke(new Action(() => { progressBar1.Value++; }));

                    // Move the file
                    await MoveFileAsync(file, destinationPath);

                    // Log the moved file's details in `textBox_Logs`
                    textBox_Logs.Invoke(new Action(() =>
                    {
                        textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                        textBox_Logs.SelectionLength = 0;
                        textBox_Logs.SelectionColor = Color.Blue; // Set log color
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                        textBox_Logs.AppendText($"{Environment.NewLine}File Moved Successfully!{Environment.NewLine}");
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                        textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
                        textBox_Logs.AppendText($"File Name: {fileName}{Environment.NewLine}");
                        textBox_Logs.AppendText($"Video Duration: {duration:hh\\:mm\\:ss}{Environment.NewLine}");
                        textBox_Logs.AppendText($"New Path: {destinationPath}{Environment.NewLine}");
                    }));
                }
                catch (Exception ex)
                {
                    // Log errors instead of using a message box
                    textBox_Logs.Invoke(new Action(() =>
                    {
                        textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                        textBox_Logs.SelectionLength = 0;
                        textBox_Logs.SelectionColor = Color.Red;
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                        textBox_Logs.AppendText($"{Environment.NewLine}Error processing file:{Environment.NewLine}");
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                        textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
                        textBox_Logs.AppendText($"File Name: {fileName}{Environment.NewLine}");
                        textBox_Logs.AppendText($"Error: {ex.Message}{Environment.NewLine}");
                    }));
                }
            }

            progressBar1.Invoke(new Action(() => { progressBar1.Value = 0; }));

            // Play a sound notification on completion
            System.Media.SystemSounds.Asterisk.Play(); // Windows default system sound

            // Log completion in `textBox_Logs`
            textBox_Logs.Invoke(new Action(() =>
            {
                textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                textBox_Logs.SelectionLength = 0;
                textBox_Logs.SelectionColor = Color.Green;
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                textBox_Logs.AppendText($"{Environment.NewLine}File filtering and organization completed!");
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
            }));
        }

        private async Task Resolution(string[] files)
        {
            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = files.Length;
                progressBar1.Value = 0;
            }));

            foreach (var file in files)
            {
                // Check if the file is a video
                if (!IsVideoFile(file))
                {
                    continue;
                }

                string fileName = Path.GetFileName(file); // Define fileName outside try block

                try
                {
                    // Use NReco to get media info and determine the resolution
                    var mediaInfo = new NReco.VideoInfo.FFProbe().GetMediaInfo(file);
                    var resolution = $"{mediaInfo.Streams.First().Width}x{mediaInfo.Streams.First().Height}";

                    var folderName = resolution;
                    var destinationFolder = Path.Combine(PathSort, folderName);
                    var destinationPath = Path.Combine(destinationFolder, fileName);

                    // Create the destination directory if it doesn't exist
                    if (!Directory.Exists(destinationFolder))
                    {
                        Directory.CreateDirectory(destinationFolder);
                    }

                    progressBar1.Invoke(new Action(() => { progressBar1.Value++; }));

                    // Move the file
                    await MoveFileAsync(file, destinationPath);

                    // Log the moved file's details in `textBox_Logs`
                    textBox_Logs.Invoke(new Action(() =>
                    {
                        textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                        textBox_Logs.SelectionLength = 0;
                        textBox_Logs.SelectionColor = Color.Blue; // Set log color
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                        textBox_Logs.AppendText($"{Environment.NewLine}File Moved Successfully!{Environment.NewLine}");
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                        textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
                        textBox_Logs.AppendText($"File Name: {fileName}{Environment.NewLine}");
                        textBox_Logs.AppendText($"Resolution: {resolution}{Environment.NewLine}");
                        textBox_Logs.AppendText($"New Path: {destinationPath}{Environment.NewLine}");
                    }));
                }
                catch (Exception ex)
                {
                    // Log errors instead of using a message box
                    textBox_Logs.Invoke(new Action(() =>
                    {
                        textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                        textBox_Logs.SelectionLength = 0;
                        textBox_Logs.SelectionColor = Color.Red;
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                        textBox_Logs.AppendText($"{Environment.NewLine}Error processing file:{Environment.NewLine}");
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                        textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
                        textBox_Logs.AppendText($"File Name: {fileName}{Environment.NewLine}");
                        textBox_Logs.AppendText($"Error: {ex.Message}{Environment.NewLine}");
                    }));
                }
            }

            progressBar1.Invoke(new Action(() => { progressBar1.Value = 0; }));

            // Play a sound notification on completion
            System.Media.SystemSounds.Asterisk.Play(); // Windows default system sound

            // Log completion in `textBox_Logs`
            textBox_Logs.Invoke(new Action(() =>
            {
                textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                textBox_Logs.SelectionLength = 0;
                textBox_Logs.SelectionColor = Color.Green;
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                textBox_Logs.AppendText($"{Environment.NewLine}File filtering and organization completed!");
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
            }));
        }

        private async Task FrameRate(string[] files)
        {
            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = files.Length;
                progressBar1.Value = 0;
            }));

            foreach (var file in files)
            {
                // Check if the file is a video
                if (!IsVideoFile(file))
                {
                    continue;
                }

                string fileName = Path.GetFileName(file); // Define fileName outside try block

                try
                {
                    // Get the frame rate of the video file
                    string framerate = GetFramerate(file);

                    // Create a folder with the frame rate name
                    string destinationFolder = Path.Combine(PathSort, framerate);
                    string destinationPath = Path.Combine(destinationFolder, fileName);

                    // Create the destination directory if it doesn't exist
                    if (!Directory.Exists(destinationFolder))
                    {
                        Directory.CreateDirectory(destinationFolder);
                    }

                    progressBar1.Invoke(new Action(() => { progressBar1.Value++; }));

                    // Move the file
                    await MoveFileAsync(file, destinationPath);

                    // Log the moved file's details in `textBox_Logs`
                    textBox_Logs.Invoke(new Action(() =>
                    {
                        textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                        textBox_Logs.SelectionLength = 0;
                        textBox_Logs.SelectionColor = Color.Blue; // Set log color
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                        textBox_Logs.AppendText($"{Environment.NewLine}File Moved Successfully!{Environment.NewLine}");
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                        textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
                        textBox_Logs.AppendText($"File Name: {fileName}{Environment.NewLine}");
                        textBox_Logs.AppendText($"Frame Rate: {framerate} FPS{Environment.NewLine}");
                        textBox_Logs.AppendText($"New Path: {destinationPath}{Environment.NewLine}");
                    }));
                }
                catch (Exception ex)
                {
                    // Log errors instead of using a message box
                    textBox_Logs.Invoke(new Action(() =>
                    {
                        textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                        textBox_Logs.SelectionLength = 0;
                        textBox_Logs.SelectionColor = Color.Red;
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                        textBox_Logs.AppendText($"{Environment.NewLine}Error processing file:{Environment.NewLine}");
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                        textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
                        textBox_Logs.AppendText($"File Name: {fileName}{Environment.NewLine}");
                        textBox_Logs.AppendText($"Error: {ex.Message}{Environment.NewLine}");
                    }));
                }
            }

            progressBar1.Invoke(new Action(() => { progressBar1.Value = 0; }));

            // Play a sound notification on completion
            System.Media.SystemSounds.Asterisk.Play(); // Windows default system sound

            // Log completion in `textBox_Logs`
            textBox_Logs.Invoke(new Action(() =>
            {
                textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                textBox_Logs.SelectionLength = 0;
                textBox_Logs.SelectionColor = Color.Green;
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                textBox_Logs.AppendText($"{Environment.NewLine}File filtering and organization completed!");
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
            }));
        }

        private string GetFramerate(string videoFilePath)
        {
            var ffProbe = new FFProbe();
            var videoInfo = ffProbe.GetMediaInfo(videoFilePath);
            var framerate = videoInfo.Streams.FirstOrDefault(s => s.CodecType == "video")?.FrameRate;

            return framerate.HasValue ? framerate.Value.ToString() : "unknown";
        }

        private async Task Codec(string[] files)
        {
            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = files.Length;
                progressBar1.Value = 0;
            }));

            foreach (var file in files)
            {
                // Check if the file is a video
                if (!IsVideoFile(file))
                {
                    continue;
                }

                string fileName = Path.GetFileName(file); // Define fileName outside try block

                try
                {
                    // Get the codec of the video file
                    string codec = GetCodec(file);

                    // Create a folder with the codec name
                    string destinationFolder = Path.Combine(PathSort, codec);
                    string destinationPath = Path.Combine(destinationFolder, fileName);

                    // Create the destination directory if it doesn't exist
                    if (!Directory.Exists(destinationFolder))
                    {
                        Directory.CreateDirectory(destinationFolder);
                    }

                    progressBar1.Invoke(new Action(() => { progressBar1.Value++; }));

                    // Move the file
                    await MoveFileAsync(file, destinationPath);

                    // Log the moved file's details in `textBox_Logs`
                    textBox_Logs.Invoke(new Action(() =>
                    {
                        textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                        textBox_Logs.SelectionLength = 0;
                        textBox_Logs.SelectionColor = Color.Blue; // Set log color
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                        textBox_Logs.AppendText($"{Environment.NewLine}File Moved Successfully!{Environment.NewLine}");
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                        textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
                        textBox_Logs.AppendText($"File Name: {fileName}{Environment.NewLine}");
                        textBox_Logs.AppendText($"Codec: {codec}{Environment.NewLine}");
                        textBox_Logs.AppendText($"New Path: {destinationPath}{Environment.NewLine}");
                    }));
                }
                catch (Exception ex)
                {
                    // Log errors instead of using a message box
                    textBox_Logs.Invoke(new Action(() =>
                    {
                        textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                        textBox_Logs.SelectionLength = 0;
                        textBox_Logs.SelectionColor = Color.Red;
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                        textBox_Logs.AppendText($"{Environment.NewLine}Error processing file:{Environment.NewLine}");
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                        textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
                        textBox_Logs.AppendText($"File Name: {fileName}{Environment.NewLine}");
                        textBox_Logs.AppendText($"Error: {ex.Message}{Environment.NewLine}");
                    }));
                }
            }

            progressBar1.Invoke(new Action(() => { progressBar1.Value = 0; }));

            // Play a sound notification on completion
            System.Media.SystemSounds.Asterisk.Play(); // Windows default system sound

            // Log completion in `textBox_Logs`
            textBox_Logs.Invoke(new Action(() =>
            {
                textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                textBox_Logs.SelectionLength = 0;
                textBox_Logs.SelectionColor = Color.Green;
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                textBox_Logs.AppendText($"{Environment.NewLine}File filtering and organization completed!");
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
            }));
        }

        private string GetCodec(string videoFilePath)
        {
            var ffProbe = new FFProbe();
            var videoInfo = ffProbe.GetMediaInfo(videoFilePath);
            var codec = videoInfo.Streams.FirstOrDefault(s => s.CodecType == "video")?.CodecName;

            return codec ?? "unknown";
        }

        private async Task AspectRatio(string[] files)
        {
            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = files.Length;
                progressBar1.Value = 0;
            }));

            foreach (var file in files)
            {
                // Check if the file is a video
                if (!IsVideoFile(file))
                {
                    continue;
                }

                string fileName = Path.GetFileName(file); // Define fileName outside try block

                try
                {
                    // Get the aspect ratio of the video file
                    string aspectRatio = GetAspectRatio(file);

                    // Create a folder with the aspect ratio name
                    string destinationFolder = Path.Combine(PathSort, aspectRatio);
                    string destinationPath = Path.Combine(destinationFolder, fileName);

                    // Create the destination directory if it doesn't exist
                    if (!Directory.Exists(destinationFolder))
                    {
                        Directory.CreateDirectory(destinationFolder);
                    }

                    progressBar1.Invoke(new Action(() => { progressBar1.Value++; }));

                    // Move the file
                    await MoveFileAsync(file, destinationPath);

                    // Log the moved file's details in `textBox_Logs`
                    textBox_Logs.Invoke(new Action(() =>
                    {
                        textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                        textBox_Logs.SelectionLength = 0;
                        textBox_Logs.SelectionColor = Color.Blue; // Set log color
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                        textBox_Logs.AppendText($"{Environment.NewLine}File Moved Successfully!{Environment.NewLine}");
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                        textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
                        textBox_Logs.AppendText($"File Name: {fileName}{Environment.NewLine}");
                        textBox_Logs.AppendText($"Aspect Ratio: {aspectRatio}{Environment.NewLine}");
                        textBox_Logs.AppendText($"New Path: {destinationPath}{Environment.NewLine}");
                    }));
                }
                catch (Exception ex)
                {
                    // Log errors instead of using a message box
                    textBox_Logs.Invoke(new Action(() =>
                    {
                        textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                        textBox_Logs.SelectionLength = 0;
                        textBox_Logs.SelectionColor = Color.Red;
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                        textBox_Logs.AppendText($"{Environment.NewLine}Error processing file:{Environment.NewLine}");
                        textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                        textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
                        textBox_Logs.AppendText($"File Name: {fileName}{Environment.NewLine}");
                        textBox_Logs.AppendText($"Error: {ex.Message}{Environment.NewLine}");
                    }));
                }
            }

            progressBar1.Invoke(new Action(() => { progressBar1.Value = 0; }));

            // Play a sound notification on completion
            System.Media.SystemSounds.Asterisk.Play(); // Windows default system sound

            // Log completion in `textBox_Logs`
            textBox_Logs.Invoke(new Action(() =>
            {
                textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                textBox_Logs.SelectionLength = 0;
                textBox_Logs.SelectionColor = Color.Green;
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                textBox_Logs.AppendText($"{Environment.NewLine}File filtering and organization completed!");
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
            }));
        }

        private string GetAspectRatio(string videoFilePath)
        {
            var ffProbe = new FFProbe();
            var videoInfo = ffProbe.GetMediaInfo(videoFilePath);
            var width = videoInfo.Streams.FirstOrDefault(s => s.CodecType == "video")?.Width;
            var height = videoInfo.Streams.FirstOrDefault(s => s.CodecType == "video")?.Height;

            if (width.HasValue && height.HasValue)
            {
                double aspectRatio = (double)width.Value / height.Value;
                return $"{width.Value}:{height.Value} ({aspectRatio:F2})";
            }

            return "unknown";
        }

        public async Task Duplicate(string[] files)
        {
            var fileHashCounts = new Dictionary<string, int>();
            var duplicateFiles = new List<(string file, string hash)>();

            // Limit concurrent tasks to prevent excessive memory usage
            using (var semaphore = new SemaphoreSlim(4)) // Adjust thread count based on system capacity
            {
                // Configure ProgressBar
                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = files.Length;
                    progressBar1.Value = 0;
                }));

                // Log Start
                textBox_Logs.Invoke(new Action(() =>
                {
                    textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                    textBox_Logs.SelectionLength = 0;
                    textBox_Logs.SelectionColor = Color.Blue;
                    textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                    textBox_Logs.AppendText($"{Environment.NewLine}Compute hashes and counting occurrences");
                    textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                    textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
                }));

                // Compute hashes dynamically with controlled concurrency
                await Task.WhenAll(files.Select(async file =>
                {
                    await semaphore.WaitAsync(); // Limit concurrency

                    try
                    {
                        string fileHash = await ComputeFileHashAsync(file);

                        lock (fileHashCounts)
                        {
                            if (fileHashCounts.ContainsKey(fileHash))
                                fileHashCounts[fileHash]++;
                            else
                                fileHashCounts[fileHash] = 1;
                        }

                        // Track duplicates for later processing
                        lock (duplicateFiles)
                        {
                            if (fileHashCounts[fileHash] > 1)
                                duplicateFiles.Add((file, fileHash));
                        }

                        // Update ProgressBar Efficiently
                        progressBar1.Invoke(new Action(() => progressBar1.Value++));
                    }
                    finally
                    {
                        semaphore.Release(); // Release slot for next task
                    }
                }));

                if (duplicateFiles.Count > 0)
                {
                    string duplicatesFolder = Path.Combine(PathSort, "Duplicates");
                    Directory.CreateDirectory(duplicatesFolder);

                    foreach (var (file, hash) in duplicateFiles)
                    {
                        string destinationPath = Path.Combine(duplicatesFolder, Path.GetFileName(file));
                        await MoveFileAsync(file, destinationPath);

                        // Log duplicate detection efficiently
                        textBox_Logs.Invoke(new Action(() =>
                       {
                           textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                           textBox_Logs.SelectionLength = 0;
                           textBox_Logs.SelectionColor = Color.Red;
                           textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                           textBox_Logs.AppendText($"{Environment.NewLine}Duplicate found! Hash: {hash}");
                           textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                           textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
                       }));
                    }
                }
            }

            // Final log message
            textBox_Logs.Invoke(new Action(() =>
            {
                textBox_Logs.SelectionStart = textBox_Logs.TextLength;
                textBox_Logs.SelectionLength = 0;
                textBox_Logs.SelectionColor = Color.Green;
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Bold);
                textBox_Logs.AppendText($"{Environment.NewLine}File filtering and organization completed!");
                textBox_Logs.SelectionFont = new Font(textBox_Logs.Font, FontStyle.Regular);
                textBox_Logs.SelectionColor = textBox_Logs.ForeColor;
            }));

            progressBar1.Invoke(new Action(() => progressBar1.Value = 0));
        }

        public async Task Scan(string[] files)
        {
            const string hashFilePath = "Hashes.json";

            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = files.Length;
                progressBar1.Value = 0;
            }));
        }

        // Global
        public void ReloadSettings()
        {
            if (File.Exists("appsettings.json"))
            {
                string settingPath = Path.GetFullPath("appsettings.json");
                Get_FilterSection(settingPath);
            }
        }

        private Dictionary<string, bool> Get_CheckboxState()
        {
            // Create a dictionary to store the checkbox names and their states
            Dictionary<string, bool> checkboxStates = new Dictionary<string, bool>();

            // Add each checkbox state to the dictionary
            checkboxStates.Add("type", checkBox_type.Checked);
            checkboxStates.Add("size", checkBox_size.Checked);
            checkboxStates.Add("date", checkBox_date.Checked);
            checkboxStates.Add("name", checkBox_name.Checked);
            checkboxStates.Add("extension", checkBox_extension.Checked);
            checkboxStates.Add("tags", checkBox_tags.Checked);
            checkboxStates.Add("media", checkBox_media.Checked);
            checkboxStates.Add("duplicate", checkBox_Duplicates.Checked);
            checkboxStates.Add("scan", checkBox_scan.Checked);

            // Return the dictionary
            return checkboxStates;
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

        private async Task<string[]> ProcessFiles(string parentPath)
        {
            bool processSubfolders = filterSettings.Subfolder;

            // Get files based on whether subfolder processing is allowed
            var files = processSubfolders
                ? Directory.GetFiles(parentPath, "*.*", SearchOption.AllDirectories)
                : Directory.GetFiles(parentPath);

            totalFiles = files.Count();

            await Task.CompletedTask; // Ensure the method is awaited

            return files; // Return the list of file paths
        }

        private async Task MoveFileAsync(string sourcePath, string destinationPath)
        {
            int fileCount = 1;
            string newDestinationPath = destinationPath;

            while (File.Exists(newDestinationPath))
            {
                string fileExtension = Path.GetExtension(destinationPath);
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(destinationPath);
                newDestinationPath = Path.Combine(Path.GetDirectoryName(destinationPath), $"{fileNameWithoutExtension} ({fileCount++}){fileExtension}");
            }

            await Task.Run(() => File.Move(sourcePath, newDestinationPath));
        }

        private async Task<string> ComputeFileHashAsync(string filePath)
        {
            using (var sha256 = SHA256.Create())
            {
                const int bufferSize = 8 * 1024 * 1024; // 8 MB buffer size

                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, useAsync: true))
                {
                    byte[] buffer = new byte[bufferSize];
                    int bytesRead;
                    while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        sha256.TransformBlock(buffer, 0, bytesRead, buffer, 0);
                    }
                    sha256.TransformFinalBlock(buffer, 0, 0);

                    byte[] hashBytes = sha256.Hash;
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }
}

public class FilterSettings
{
    public bool Image { get; set; }
    public bool Video { get; set; }
    public bool Document { get; set; }
    public bool Audio { get; set; }
    public bool Archive { get; set; }
    public bool Executable { get; set; }
    public bool Other { get; set; }
    public List<string> Image_List { get; set; }
    public List<string> Video_List { get; set; }
    public List<string> Document_List { get; set; }
    public List<string> Audio_List { get; set; }
    public List<string> Archive_List { get; set; }
    public List<string> Executable_List { get; set; }
    public List<string> Other_List { get; set; }
    public bool Delete { get; set; }
    public bool Subfolder { get; set; }
    public bool Creation { get; set; }
    public bool Access { get; set; }
    public bool Modify { get; set; }
    public bool Duration { get; set; }
    public bool Resolution { get; set; }
    public bool FrameRate { get; set; }
    public bool Codec { get; set; }
    public bool AspectRatio { get; set; }
    public List<string> Tag_List { get; set; }
    public bool Range { get; set; }
    public bool Dynamic { get; set; }
    public List<Dictionary<string, List<string>>> Range_List { get; set; }
    public bool Caps { get; set; }
    public bool Chars { get; set; }
}
