using System.Text;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Security.Cryptography;
using NReco.VideoInfo;
using Newtonsoft.Json;

namespace Tools
{
    public partial class UserControl_Filter : UserControl
    {
        private string PathSort;
        private FilterSettings filterSettings;
        private int totalFiles = 0;
        private string[] files;
        private HashSet<string> globalHashes;

        public UserControl_Filter()
        {
            InitializeComponent();
            LoadGlobalJson();
            PathSort = string.Empty; // Initialize PathSort
            filterSettings = new FilterSettings(); // Initialize filterSettings
            files = Array.Empty<string>(); // Initialize files
            globalHashes = new HashSet<string>(); // Initialize globalHashes
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
                string fileExtension = System.IO.Path.GetExtension(file).TrimStart('.').ToLower();

                foreach (var type in typeToSettings)
                {
                    if (type.Value.IsEnabled && type.Value.Extensions.Contains(fileExtension))
                    {
                        // Create a folder based on the type.Key if it doesn't exist
                        string folderPath = Path.Combine(PathSort, type.Key);
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        // Determine the destination file path
                        string destinationFilePath = Path.Combine(folderPath, Path.GetFileName(file));
                        int counter = 1;

                        // If the file already exists, append a unique identifier to the file name
                        while (File.Exists(destinationFilePath))
                        {
                            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                            string newFileName = $"{fileNameWithoutExtension} ({counter}){Path.GetExtension(file)}";
                            destinationFilePath = Path.Combine(folderPath, newFileName);
                            counter++;
                        }

                        // Move the file to the created folder
                        await MoveFileAsync(file, destinationFilePath);

                        // Increment the progress bar
                        progressBar1.Invoke(new Action(() =>
                        {
                            progressBar1.Value++;
                        }));

                        break; // Exit the loop once a match is found
                    }
                }
            }

            // Reset the progress bar and show a completion message
            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Value = 0;
            }));

            MessageBox.Show("File filtering and organization completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async Task FilterSize(string[] files)
        {
            if (filterSettings.Range)
            {
                await Range(files, filterSettings.Range_List); // Assuming Range is synchronous
            }
            else if (filterSettings.Dynamic)
            {
                await Dynamic(files); // Add 'await' to call the async method correctly
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

                    // Convert the ranges to bytes
                    long minSize = sizeList.Count > 1 ? ConvertToBytes(sizeList[0], sizeList[1]) : 0;
                    long maxSize = sizeList.Count > 3 ? ConvertToBytes(sizeList[2], sizeList[3]) : long.MaxValue;

                    // Process each file
                    foreach (string file in files)
                    {
                        long fileSize = new FileInfo(file).Length;

                        // Check if the file falls within the range
                        if (fileSize >= minSize && fileSize <= maxSize)
                        {
                            // Determine the folder name
                            string folderPath = Path.Combine(PathSort, key);

                            // Create the folder if it doesn't exist
                            if (!Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                            }

                            // Move the file to the folder
                            string destinationFilePath = Path.Combine(folderPath, Path.GetFileName(file));

                            // Create the size-specific subfolder
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

                            progressBar1.Invoke(new Action(() =>
                            {
                                progressBar1.Value++;
                            }));
                        }
                    }
                }
            }

            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Value = 0;
            }));

            MessageBox.Show("File filtering and organization completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                string sizeCategory = "";
                long sizeValue = 0;

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

                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Value++;
                }));
            }

            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Value = 0;
            }));

            MessageBox.Show("File filtering and organization completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Value++;
                }));

                await MoveFileAsync(file, newDestinationPath);
            }

            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Value = 0;
            }));

            MessageBox.Show("File filtering and organization completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async Task Access(string[] files)
        {
            string accessDateParentFolder = Path.Combine(PathSort, "Access date");

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

                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Value++;
                }));

                await MoveFileAsync(file, newDestinationPath);
            }

            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Value = 0;
            }));

            MessageBox.Show("File filtering and organization completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async Task Modify(string[] files)
        {
            string modifyDateParentFolder = Path.Combine(PathSort, "Modify date");

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

                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Value++;
                }));

                await MoveFileAsync(file, newDestinationPath);
            }

            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Value = 0;
            }));

            MessageBox.Show("File filtering and organization completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            // Initialize progress bar
            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = files.Length;
                progressBar1.Value = 0;
            }));

            // Process each file
            foreach (var file in files)
            {
                try
                {
                    // Read the file name
                    string fileName = Path.GetFileName(file);

                    // Determine whether the file name contains any non-ASCII characters
                    bool isAscii = fileName.All(c => c <= 127);

                    // Move the file to the corresponding folder
                    string destinationFolder = isAscii ? asciiFolder : nonAsciiFolder;
                    string destinationPath = Path.Combine(destinationFolder, fileName);

                    await MoveFileAsync(file, destinationPath);

                    // Update the progress bar
                    progressBar1.Invoke(new Action(() =>
                    {
                        progressBar1.Value++;
                    }));
                }
                catch (Exception ex)
                {
                    // Handle errors (e.g., logging, notifying the user)
                    MessageBox.Show($"Error processing file {file}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Reset progress bar
            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Value = 0;
            }));

            // Display completion message
            MessageBox.Show("File filtering and organization completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async Task IgnoreCaps(string[] files)
        {
            string folderPath = Path.Combine(PathSort, "Characters");

            // Create the main folder if it doesn't exist
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Initialize progress bar
            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = files.Length;
                progressBar1.Value = 0;
            }));

            // Process each file
            foreach (var file in files)
            {
                try
                {
                    // Get the file name
                    string fileName = Path.GetFileName(file);

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

                    // Update the progress bar
                    progressBar1.Invoke(new Action(() =>
                    {
                        progressBar1.Value++;
                    }));
                }
                catch (Exception ex)
                {
                    // Handle errors
                    MessageBox.Show($"Error processing file {file}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Reset the progress bar
            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Value = 0;
            }));

            // Display completion message
            MessageBox.Show("File filtering and organization completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Value++;
                }));

                await MoveFileAsync(file, newDestinationPath);
            }

            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Value = 0;
            }));

            MessageBox.Show("File filtering and organization completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                        progressBar1.Invoke(new Action(() =>
                        {
                            progressBar1.Value++;
                        }));

                        await MoveFileAsync(file, newDestinationPath);
                        break; // If a tag is found, stop checking other tags for this file
                    }
                }
            }

            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Value = 0;
            }));

            MessageBox.Show("File filtering and organization completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                // Use NReco to get media info and determine the duration
                var mediaInfo = new FFProbe().GetMediaInfo(file);
                var duration = mediaInfo.Duration;

                // Format duration as HH-mm-ss
                var folderName = $"{duration:hh\\-mm\\-ss}";

                var destinationFolder = Path.Combine(PathSort, folderName);
                var destinationPath = Path.Combine(destinationFolder, Path.GetFileName(file));

                // Create the destination directory if it doesn't exist
                if (!Directory.Exists(destinationFolder))
                {
                    Directory.CreateDirectory(destinationFolder);
                }

                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Value++;
                }));

                // Move the file
                await MoveFileAsync(file, destinationPath);
            }

            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Value = 0;
            }));

            MessageBox.Show("File filtering and organization completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                // Use NReco to get media info and determine the resolution
                var mediaInfo = new NReco.VideoInfo.FFProbe().GetMediaInfo(file);
                var resolution = $"{mediaInfo.Streams.First().Width}x{mediaInfo.Streams.First().Height}";

                var folderName = resolution;
                var destinationPath = Path.Combine(PathSort, folderName, Path.GetFileName(file));

                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Value++;
                }));
                // Move the file
                await MoveFileAsync(file, destinationPath);
            }

            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Value = 0;
            }));

            MessageBox.Show("File filtering and organization completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                // Get the framerate of the video file
                string framerate = GetFramerate(file);

                // Create a folder with the framerate name
                string destinationPath = Path.Combine(PathSort, framerate);
                Directory.CreateDirectory(destinationPath);

                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Value++;
                }));
                // Move the file
                await MoveFileAsync(file, destinationPath);
            }

            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Value = 0;
            }));

            MessageBox.Show("File filtering and organization completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                // Get the codec of the video file
                string codec = GetCodec(file);

                // Create a folder with the codec name
                string destinationPath = Path.Combine(PathSort, codec);
                Directory.CreateDirectory(destinationPath);

                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Value++;
                }));
                // Move the file
                await MoveFileAsync(file, destinationPath);
            }

            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Value = 0;
            }));

            MessageBox.Show("File filtering and organization completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                // Get the aspect ratio of the video file
                string aspectRatio = GetAspectRatio(file);

                // Create a folder with the aspect ratio name
                string destinationPath = Path.Combine(PathSort, aspectRatio);
                Directory.CreateDirectory(destinationPath);

                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Value++;
                }));
                // Move the file
                await MoveFileAsync(file, destinationPath);
            }

            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Value = 0;
            }));

            MessageBox.Show("File filtering and organization completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            // Ensure the "Duplicates" folder exists
            string duplicatesFolder = Path.Combine(PathSort, "Duplicates");
            Directory.CreateDirectory(duplicatesFolder);

            var fileHashCounts = new Dictionary<string, int>();

            // Configure the ProgressBar for the first pass
            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = files.Length;
                progressBar1.Value = 0;
            }));

            // First pass: Compute hashes and count duplicates
            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                string fileHash = await ComputeFileHashAsync(file);

                if (fileHashCounts.ContainsKey(fileHash))
                {
                    fileHashCounts[fileHash]++;
                }
                else
                {
                    fileHashCounts[fileHash] = 1;
                }

                // Update progress bar using Invoke
                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Value = i + 1;
                }));
            }

            // Reset ProgressBar for the second pass
            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Value = 0;
            }));

            // Second pass: Process files and move duplicates
            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                string fileHash = await ComputeFileHashAsync(file);

                // Check if the hash occurs more than once
                if (fileHashCounts[fileHash] > 1)
                {
                    // Define the destination path in the "Duplicates" folder
                    string destinationPath = Path.Combine(duplicatesFolder, Path.GetFileName(file));
                    await MoveFileAsync(file, destinationPath);

                    // Do not decrement the count to allow all duplicates to be moved
                }

                // Update progress bar using Invoke
                progressBar1.Invoke(new Action(() =>
                {
                    progressBar1.Value = i + 1;
                }));
            }

            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Value = 0;
            }));

            MessageBox.Show("Duplicate file processing completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public async Task Scan(string[] files)
        {
            const string hashFilePath = "Hashes.json";

            // Initialize the progress bar
            Invoke(new Action(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = files.Length;
                progressBar1.Value = 0;
            }));

            // A set to store unique hashes (load existing hashes from the JSON file)
            var uniqueHashes = new HashSet<string>();

            if (File.Exists(hashFilePath))
            {
                using (var reader = new StreamReader(hashFilePath))
                {
                    var existingHashes = JsonConvert.DeserializeObject<HashSet<string>>(reader.ReadToEnd());
                    if (existingHashes != null)
                    {
                        foreach (var hash in existingHashes)
                        {
                            uniqueHashes.Add(hash); // Add existing hashes to the set
                        }
                    }
                }
            }

            // Process files in batches
            var fileBatches = files.Select((file, index) => new { file, index })
                                     .GroupBy(x => x.index / 1000) // Adjust the batch size as needed
                                     .Select(g => g.Select(x => x.file).ToArray());

            foreach (var batch in fileBatches)
            {
                var throttler = new SemaphoreSlim(8); // Adjust the degree of parallelism as needed

                // Process the batch with limited parallelism
                var tasks = batch.Select(async file =>
                {
                    await throttler.WaitAsync();
                    try
                    {
                        string hash = await ComputeFileHashAsync(file);

                        // Lock to ensure thread safety when accessing shared resources
                        lock (uniqueHashes)
                        {
                            uniqueHashes.Add(hash); // `HashSet` ensures no duplicates
                        }

                        // Update the progress bar safely
                        Invoke(new Action(() =>
                        {
                            progressBar1.Value++;
                        }));
                    }
                    finally
                    {
                        throttler.Release(); // Release semaphore
                    }
                });

                await Task.WhenAll(tasks);
            }

            // Save the updated list of unique hashes to the JSON file
            using (var writer = new StreamWriter(hashFilePath))
            using (var jsonWriter = new JsonTextWriter(writer) { Formatting = Formatting.Indented })
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(jsonWriter, uniqueHashes.ToList());
            }

            progressBar1.Invoke(new Action(() =>
            {
                progressBar1.Value = 0;
            }));

            MessageBox.Show("Hashing process completed.", "Hashing Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void LoadGlobalJson()
        {
            const string hashFilePath = "Hashes.json";

            globalHashes = new HashSet<string>();

            if (File.Exists(hashFilePath))
            {
                try
                {
                    using (var reader = new StreamReader(hashFilePath))
                    {
                        var existingHashes = JsonConvert.DeserializeObject<HashSet<string>>(reader.ReadToEnd());
                        if (existingHashes != null)
                        {
                            globalHashes = new HashSet<string>(existingHashes); // Initialize with existing hashes
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading JSON: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public async Task AddFileIfMissingAsync(string filePath)
        {
            const string hashFilePath = "Hashes.json";

            // Check if the globalHashes variable contains the hash for the file
            if (globalHashes == null)
            {
                MessageBox.Show("Global variable is not initialized. Please load the JSON first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Compute the hash for the given file
            string fileHash;
            try
            {
                fileHash = await ComputeFileHashAsync(filePath); // Assuming ComputeFileHash is a synchronous hash function
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error computing hash for file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if the hash is already in the globalHashes set
            if (globalHashes.Contains(fileHash))
            {
                MessageBox.Show("The file is already present in the global JSON file.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Add the hash to the globalHashes set
            lock (globalHashes)
            {
                globalHashes.Add(fileHash);
            }

            // Update the JSON file with the new data
            try
            {
                using (var writer = new StreamWriter(hashFilePath))
                using (var jsonWriter = new JsonTextWriter(writer) { Formatting = Formatting.Indented })
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(jsonWriter, globalHashes.ToList());
                }

                MessageBox.Show("The file was successfully added to the JSON file.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating JSON file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

            // Log the destination path to the TextBox
            textBox_Logs.Invoke(new Action(() =>
            {
                string fileName = Path.GetFileName(sourcePath); // Get the file name
                textBox_Logs.AppendText($"{Environment.NewLine}"); // Add a separator line
                textBox_Logs.AppendText($"Moving file \"{fileName}\" to: {newDestinationPath}{Environment.NewLine}");
            }));

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

        private async Task PopulateTreeView()
        {
            treeView1.Invoke(new Action(() => treeView1.Nodes.Clear())); // Clear the TreeView

            string rootPath = PathSort;
            string rootNodeName = new DirectoryInfo(rootPath).Name;

            TreeNode rootNode = new TreeNode(rootNodeName);
            treeView1.Invoke(new Action(() => treeView1.Nodes.Add(rootNode)));

            // Recursively populate nodes based on the current state of the directory structure
            await Task.Run(() => PopulateNodes(rootNode, rootPath));
        }

        private void PopulateNodes(TreeNode parentNode, string parentPath)
        {
            try
            {
                string[] directories = Directory.GetDirectories(parentPath);

                foreach (string directory in directories)
                {
                    string dirName = new DirectoryInfo(directory).Name;
                    TreeNode childNode = new TreeNode(dirName);

                    // Add child node using Invoke
                    treeView1.Invoke(new Action(() => parentNode.Nodes.Add(childNode)));

                    // Recursively populate child directories
                    PopulateNodes(childNode, directory);
                }

                string[] files = Directory.GetFiles(parentPath);

                // Add the count of files as a leaf node
                if (files.Length > 0)
                {
                    TreeNode fileCountNode = new TreeNode($"Files: {files.Length}");

                    // Add file count node using Invoke
                    treeView1.Invoke(new Action(() => parentNode.Nodes.Add(fileCountNode)));
                }
            }
            catch (Exception ex)
            {
                // Show error message using Invoke
                treeView1.Invoke(new Action(() =>
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));
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
