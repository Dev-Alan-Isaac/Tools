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
    public partial class UserControl_Filter : UserControl
    {
        private string PathSort, CountFile;
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

            // Print the checkbox states for debugging
            foreach (var state in checkboxStates)
            {
                Debug.WriteLine($"{state.Key}: {state.Value}");
            }

            // Run the FilterType method asynchronously
            //Task.Run(() => FilterType(files));
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

            // Return the dictionary
            return checkboxStates;
        }

        public async Task<string[]> ProcessFiles(string parentPath)
        {
            bool processSubfolders = filterSettings.Subfolder;

            // Get files based on whether subfolder processing is allowed
            var files = processSubfolders
                ? Directory.GetFiles(parentPath, "*.*", SearchOption.AllDirectories)
                : Directory.GetFiles(parentPath);

            totalFiles = files.Count();

            return files; // Return the list of file paths
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
                        File.Move(file, destinationFilePath);

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
                        File.Move(file, destinationFilePath);

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
                    string parentFolder = Path.Combine("RootDirectory", sizeCategory);
                    string targetFolder = Path.Combine(parentFolder, sizeValue.ToString());

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

                    // Move the file
                    File.Move(file, targetFilePath);

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
            if (filterSettings.Caps)
            {

            }
            else if (filterSettings.Chars)
            {

            }
        }

        private async Task FilterName(string[] files)
        {

        }

        private async Task FilterHash(string[] files)
        {

        }

        private async Task FilterExtension(string[] files)
        {

        }

        private async Task FilterTags(string[] files)
        {

        }

        private async Task FilterMedia(string[] files)
        {

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
