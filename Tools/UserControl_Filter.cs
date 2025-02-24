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
        private string PathSort;
        private FilterSettings filterSettings;

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

        private void Get_ClassValues()
        {
            if (filterSettings == null)
            {
                Debug.WriteLine("FilterSettings is not initialized.");
                return;
            }

            // Display Types Section
            Debug.WriteLine("=== Types ===");
            Debug.WriteLine($"Image: {filterSettings.Image}");
            Debug.WriteLine($"Video: {filterSettings.Video}");
            Debug.WriteLine($"Document: {filterSettings.Document}");
            Debug.WriteLine($"Audio: {filterSettings.Audio}");
            Debug.WriteLine($"Archive: {filterSettings.Archive}");
            Debug.WriteLine($"Executable: {filterSettings.Executable}");
            Debug.WriteLine($"Other: {filterSettings.Other}");

            Debug.WriteLine("Image_List: " + string.Join(", ", filterSettings.Image_List ?? new List<string>()));
            Debug.WriteLine("Video_List: " + string.Join(", ", filterSettings.Video_List ?? new List<string>()));
            Debug.WriteLine("Document_List: " + string.Join(", ", filterSettings.Document_List ?? new List<string>()));
            Debug.WriteLine("Audio_List: " + string.Join(", ", filterSettings.Audio_List ?? new List<string>()));
            Debug.WriteLine("Archive_List: " + string.Join(", ", filterSettings.Archive_List ?? new List<string>()));
            Debug.WriteLine("Executable_List: " + string.Join(", ", filterSettings.Executable_List ?? new List<string>()));
            Debug.WriteLine("Other_List: " + string.Join(", ", filterSettings.Other_List ?? new List<string>()));

            // Display Additional Section
            Debug.WriteLine("=== Additional ===");
            Debug.WriteLine($"Delete: {filterSettings.Delete}");
            Debug.WriteLine($"Subfolder: {filterSettings.Subfolder}");

            // Display Date Section
            Debug.WriteLine("=== Date ===");
            Debug.WriteLine($"Creation: {filterSettings.Creation}");
            Debug.WriteLine($"Access: {filterSettings.Access}");
            Debug.WriteLine($"Modify: {filterSettings.Modify}");

            // Display Media Section
            Debug.WriteLine("=== Media ===");
            Debug.WriteLine($"Duration: {filterSettings.Duration}");
            Debug.WriteLine($"Resolution: {filterSettings.Resolution}");
            Debug.WriteLine($"FrameRate: {filterSettings.FrameRate}");
            Debug.WriteLine($"Codec: {filterSettings.Codec}");
            Debug.WriteLine($"AspectRatio: {filterSettings.AspectRatio}");

            // Display Tags Section
            Debug.WriteLine("=== Tags ===");
            Debug.WriteLine("Tag_List: " + string.Join(", ", filterSettings.Tag_List ?? new List<string>()));

            // Display Size Section
            Debug.WriteLine("=== Size ===");
            Debug.WriteLine($"Range: {filterSettings.Range}");
            Debug.WriteLine($"Dynamic: {filterSettings.Dynamic}");
            if (filterSettings.Range_List != null)
            {
                foreach (var rangeDict in filterSettings.Range_List)
                {
                    Debug.WriteLine("---- Range Dictionary ----");
                    foreach (var key in rangeDict.Keys)
                    {
                        Debug.WriteLine($"{key}: " + string.Join(", ", rangeDict[key] ?? new List<string>()));
                    }
                }
            }

            // Display Name Section
            Debug.WriteLine("=== Name ===");
            Debug.WriteLine($"Caps: {filterSettings.Caps}");
            Debug.WriteLine($"Chars: {filterSettings.Chars}");
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
                }
            }
        }

        private void button_Play_Click(object sender, EventArgs e)
        {
            Get_ClassValues();
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

    // Change from List<Range> to List<Dictionary<string, List<string>>>
    public List<Dictionary<string, List<string>>> Range_List { get; set; }

    public bool Caps { get; set; }
    public bool Chars { get; set; }
}
