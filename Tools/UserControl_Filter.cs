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

        public UserControl_Filter()
        {
            InitializeComponent();
        }

        private void UserControl_Filter_Load(object sender, EventArgs e)
        {
            ReloadSettings();
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
            // Get the checkbox states
            Dictionary<string, bool> checkboxStates = Get_CheckboxState();

            // Define the filter actions
            Dictionary<string, Action> filterActions = new Dictionary<string, Action>
            {
                { "type", () => Task.Run(() => FilterType(files)) },
                { "size", () => Task.Run(() => FilterSize(files)) },
                { "date", () => Task.Run(() => FilterDate(files)) },
                { "name", () => Task.Run(() => FilterName(files)) },
                { "hash", () => Task.Run(() => FilterHash(files)) },
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

            if (filterSettings.Delete)
            {
                Delete_Folders(PathSort);
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
            checkboxStates.Add("hash", checkBox_hash.Checked);
            checkboxStates.Add("extension", checkBox_extension.Checked);
            checkboxStates.Add("tags", checkBox_tags.Checked);
            checkboxStates.Add("media", checkBox_media.Checked);
            checkboxStates.Add("duplicate", checkBox_Duplicates.Checked);
            checkboxStates.Add("scan", checkBox_scan.Checked);


            // Return the dictionary
            return checkboxStates;
        }

        public async Task Duplicate(string[] files)
        {
            const string hashFilePath = "Hashes.json";
            Dictionary<string, List<string>> hashDictionary;

            // Check if the hash file exists and has content
            if (File.Exists(hashFilePath) && new FileInfo(hashFilePath).Length > 0)
            {
                // Load the hash dictionary from the JSON file
                hashDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(File.ReadAllText(hashFilePath));
            }
            else
            {
                // Call the Scan method to create the hash file
                await Scan(files);

                // Load the hash dictionary from the JSON file
                hashDictionary = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(File.ReadAllText(hashFilePath));
            }

            // Reset the progress bar for the moving process
            Invoke(new Action(() =>
            {
                progressBar1.Value = 0;
                progressBar1.Maximum = hashDictionary.Values.Sum(list => list.Count) - hashDictionary.Count;
            }));

            foreach (var hashGroup in hashDictionary)
            {
                if (hashGroup.Value.Count > 1) // Only consider groups with more than one file
                {
                    foreach (string file in hashGroup.Value.Skip(1)) // Skip the first file to avoid moving it
                    {
                        string fileName = Path.GetFileName(file);
                        string destinationPath = Path.Combine(Path.GetDirectoryName(file), fileName);

                        // Move the file to the new directory using MoveFileAsync
                        await MoveFileAsync(file, destinationPath);
                        Debug.WriteLine($"{file} being moved to {destinationPath}");

                        // Update the progress bar
                        Invoke(new Action(() =>
                        {
                            progressBar1.Value++;
                        }));
                    }
                }
            }

            // Clear the hash dictionary to free up memory
            hashDictionary.Clear();

            // Force garbage collection to free up memory
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public async Task Scan(string[] files)
        {
            const string hashFilePath = "Hashes.json";
            Dictionary<string, List<string>> hashDictionary = new Dictionary<string, List<string>>();

            // Initialize the progress bar
            Invoke(new Action(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = files.Length;
                progressBar1.Value = 0;
            }));

            // Process files in batches
            var fileBatches = files.Select((file, index) => new { file, index })
                                    .GroupBy(x => x.index / 1000) // Adjust the batch size as needed
                                    .Select(g => g.Select(x => x.file).ToArray());

            foreach (var batch in fileBatches)
            {
                var tasks = batch.Select(async file =>
                {
                    string hash = await ComputeFileHashAsync(file);
                    lock (hashDictionary) // Ensure thread safety
                    {
                        if (!hashDictionary.ContainsKey(hash))
                        {
                            hashDictionary[hash] = new List<string>();
                        }
                        hashDictionary[hash].Add(file);
                    }

                    // Update the progress bar
                    Invoke(new Action(() =>
                    {
                        progressBar1.Value++;
                    }));
                });

                // Limit the number of concurrent tasks
                var throttler = new SemaphoreSlim(8); // Adjust the degree of parallelism as needed
                var throttledTasks = tasks.Select(async task =>
                {
                    await throttler.WaitAsync();
                    try
                    {
                        await task;
                    }
                    finally
                    {
                        throttler.Release();
                    }
                });

                await Task.WhenAll(throttledTasks);
            }

            // Save the hash dictionary to a JSON file
            File.WriteAllText(hashFilePath, JsonConvert.SerializeObject(hashDictionary));

            // Display a message after the hashing process is completed
            MessageBox.Show("Hashing process completed.", "Hashing Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

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

                        // Log the file processing
                        Debug.WriteLine($"File: {file}, Extension: {fileExtension}, Type: {type.Key}, Moved to: {destinationFilePath}");
                        break; // Exit the loop once a match is found
                    }
                }
            }
        }

        private async Task FilterSize(string[] files)
        {
            if (filterSettings.Range)
            {
                List<Dictionary<string, List<string>>> keyValuePairs = filterSettings.Range_List;

                // Initialize size range variables
                long smallMax = 0, mediumMin = 0, mediumMax = 0, largeMin = 0, largeMax = 0, extraLargeMin = 0;

                // Convert size values to bytes and assign to the corresponding variables
                foreach (var kvp in keyValuePairs)
                {
                    foreach (var key in kvp.Keys)
                    {
                        var sizeList = kvp[key];
                        long minSize = sizeList.Count > 1 ? ConvertToBytes(sizeList[0], sizeList[1]) : 0;
                        long maxSize = sizeList.Count > 3 ? ConvertToBytes(sizeList[2], sizeList[3]) : minSize;

                        switch (key)
                        {
                            case "Small":
                                smallMax = maxSize;
                                break;
                            case "Medium":
                                mediumMin = minSize;
                                mediumMax = maxSize;
                                break;
                            case "Large":
                                largeMin = minSize;
                                largeMax = maxSize;
                                break;
                            case "Extra_Large":
                                extraLargeMin = minSize;
                                break;
                        }
                    }
                }

                // Process files based on size ranges
                foreach (string file in files)
                {
                    long fileSize = new FileInfo(file).Length;
                    string folderName = "";

                    if (fileSize <= smallMax)
                    {
                        folderName = "Small";
                    }
                    else if (fileSize >= mediumMin && fileSize <= mediumMax)
                    {
                        folderName = "Medium";
                    }
                    else if (fileSize >= largeMin && fileSize <= largeMax)
                    {
                        folderName = "Large";
                    }
                    else if (fileSize >= extraLargeMin)
                    {
                        folderName = "Extra Large";
                    }

                    if (!string.IsNullOrEmpty(folderName))
                    {
                        string folderPath = Path.Combine(PathSort, folderName);
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        // Handle files with the same name
                        string destinationFilePath = Path.Combine(folderPath, Path.GetFileName(file));
                        int counter = 1;
                        while (File.Exists(destinationFilePath))
                        {
                            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                            string newFileName = $"{fileNameWithoutExtension} ({counter}){Path.GetExtension(file)}";
                            destinationFilePath = Path.Combine(folderPath, newFileName);
                            counter++;
                        }

                        // Move the file to the created folder
                        await MoveFileAsync(file, destinationFilePath);

                        // Log the file processing
                        Debug.WriteLine($"File: {file}, Size: {fileSize} bytes, Range: {folderName}, Moved to: {destinationFilePath}");
                    }
                }
            }
            else if (filterSettings.Dynamic)
            {
                // Define the file size thresholds in bytes for KB, MB, GB, and TB
                long kbThreshold = 1024; // 1 KB
                long mbThreshold = 1024 * kbThreshold; // 1 MB
                long gbThreshold = 1024 * mbThreshold; // 1 GB
                long tbThreshold = 1024 * gbThreshold; // 1 TB

                // Process each file
                foreach (string file in files)
                {
                    long fileSize = new FileInfo(file).Length;

                    // Determine the appropriate folder based on size
                    string sizeCategory = "";
                    long sizeValue = 0;

                    if (fileSize < mbThreshold)
                    {
                        sizeCategory = "KB";
                        sizeValue = fileSize / kbThreshold;
                    }
                    else if (fileSize < gbThreshold)
                    {
                        sizeCategory = "MB";
                        sizeValue = fileSize / mbThreshold;
                    }
                    else if (fileSize < tbThreshold)
                    {
                        sizeCategory = "GB";
                        sizeValue = fileSize / gbThreshold;
                    }
                    else
                    {
                        sizeCategory = "TB";
                        sizeValue = fileSize / tbThreshold;
                    }

                    // Build the directory path based on the size category and size value
                    string destinationFilePath = Path.Combine(PathSort, sizeCategory);
                    string targetFolder = Path.Combine(destinationFilePath, sizeValue.ToString());

                    // Create the target folder if it doesn't exist
                    if (!Directory.Exists(targetFolder))
                    {
                        Directory.CreateDirectory(targetFolder);
                    }

                    // Move the file into the appropriate folder
                    string targetFilePath = Path.Combine(targetFolder, Path.GetFileName(file));
                    int counter = 1;

                    // Handle files with the same name
                    while (File.Exists(targetFilePath))
                    {
                        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                        string newFileName = $"{fileNameWithoutExtension} ({counter}){Path.GetExtension(file)}";
                        targetFilePath = Path.Combine(targetFolder, newFileName);
                        counter++;
                    }

                    // Move the file to the created folder
                    await MoveFileAsync(file, destinationFilePath);

                    // Log the file processing
                    Debug.WriteLine($"File: {file}, Size: {fileSize} bytes, Category: {sizeCategory}, Size Value: {sizeValue}, Moved to: {targetFilePath}");
                }
            }
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
            // Base path for creation date, access date, and modify date folders
            string basePath = Path.GetDirectoryName(files[0]);

            if (filterSettings.Creation)
            {
                string creationDateParentFolder = Path.Combine(basePath, "Creation date");

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

                    await MoveFileAsync(file, newDestinationPath);
                }
            }
            else if (filterSettings.Access)
            {
                string accessDateParentFolder = Path.Combine(basePath, "Access date");

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

                    await MoveFileAsync(file, newDestinationPath);
                }
            }
            else if (filterSettings.Modify)
            {
                string modifyDateParentFolder = Path.Combine(basePath, "Modify date");

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

                    await MoveFileAsync(file, newDestinationPath);
                }
            }
        }

        private async Task FilterName(string[] files)
        {
            if (filterSettings.Chars && filterSettings.Caps)
            {
                // Apply both filters: non-UTF8 characters and capitalization
                foreach (var file in files)
                {
                    string fileName = Path.GetFileName(file);

                    if (ContainsNonUTF8Characters(fileName) && StartsWithUppercase(fileName))
                    {
                        string destinationPath = Path.Combine("FilteredCharsCaps", fileName);
                        await MoveFileAsync(file, destinationPath);
                    }
                }
            }
            else if (filterSettings.Chars)
            {
                // Apply filter by non-UTF8 characters only
                foreach (var file in files)
                {
                    string fileName = Path.GetFileName(file);

                    if (ContainsNonUTF8Characters(fileName))
                    {
                        string destinationPath = Path.Combine("FilteredChars", fileName);
                        await MoveFileAsync(file, destinationPath);
                    }
                }
            }
            else if (filterSettings.Caps)
            {
                // Apply filter by capitalization only
                foreach (var file in files)
                {
                    string fileName = Path.GetFileName(file);

                    if (StartsWithUppercase(fileName))
                    {
                        string destinationPath = Path.Combine("FilteredCaps", fileName);
                        await MoveFileAsync(file, destinationPath);
                    }
                }
            }
        }

        private bool ContainsNonUTF8Characters(string fileName)
        {
            Encoding utf8 = Encoding.UTF8;
            byte[] utf8Bytes = utf8.GetBytes(fileName);

            // If converting to UTF8 and back changes the string, it contains non-UTF8 characters
            string converted = utf8.GetString(utf8Bytes);
            return !string.Equals(fileName, converted, StringComparison.Ordinal);
        }

        private bool StartsWithUppercase(string fileName)
        {
            return !string.IsNullOrEmpty(fileName) && char.IsUpper(fileName[0]);
        }

        private async Task FilterHash(string[] files)
        {
            Dictionary<string, List<string>> hashDictionary = new Dictionary<string, List<string>>();

            // Compute the hash for each file and group by hash
            foreach (string file in files)
            {
                string hash = await ComputeFileHashAsync(file);

                if (!hashDictionary.ContainsKey(hash))
                {
                    hashDictionary[hash] = new List<string>();
                }

                hashDictionary[hash].Add(file);
            }

            // Create directories and move files with the same hash
            foreach (var hashGroup in hashDictionary)
            {
                if (hashGroup.Value.Count > 1) // Only consider groups with more than one file
                {
                    string directoryPath = Path.Combine(Path.GetDirectoryName(hashGroup.Value[0]), "Hash");

                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    foreach (string file in hashGroup.Value)
                    {
                        string fileName = Path.GetFileName(file);
                        string destinationPath = Path.Combine(directoryPath, fileName);

                        // Move the file to the new directory
                        await MoveFileAsync(file, destinationPath);
                    }
                }
            }
        }

        private async Task FilterExtension(string[] files)
        {
            // Base path for extension folders
            string basePath = Path.GetDirectoryName(files[0]);

            foreach (var file in files)
            {
                string fileExtension = Path.GetExtension(file).TrimStart('.').ToLowerInvariant();
                string extensionFolder = Path.Combine(basePath, fileExtension);

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

                await Task.Run(() => File.Move(file, newDestinationPath));
            }
        }

        private async Task FilterTags(string[] files)
        {
            var tagList = filterSettings.Tag_List;

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

                        await MoveFileAsync(file, newDestinationPath);
                        break; // If a tag is found, stop checking other tags for this file
                    }
                }
            }
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
            string extension = Path.GetExtension(filePath).ToLower();
            return filterSettings.Video_List.Contains(extension);
        }

        private async Task Duration(string[] files)
        {
            foreach (var file in files)
            {
                // Check if the file is a video
                if (!IsVideoFile(file))
                {
                    continue;
                }

                // Use NReco to get media info and determine the duration
                var mediaInfo = new NReco.VideoInfo.FFProbe().GetMediaInfo(file);
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

                // Move the file
                await MoveFileAsync(file, destinationPath);
            }
        }

        private async Task Resolution(string[] files)
        {
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

                // Move the file
                await MoveFileAsync(file, destinationPath);
            }
        }

        private async Task FrameRate(string[] files)
        {
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

                // Move the file
                await MoveFileAsync(file, destinationPath);
            }
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

                // Move the file
                await MoveFileAsync(file, destinationPath);
            }
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

                // Move the file
                await MoveFileAsync(file, destinationPath);
            }
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

        private void Delete_Folders(string path)
        {
            try
            {
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
                Console.WriteLine($"An error occurred: {ex.Message}");
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

            return files; // Return the list of file paths
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
