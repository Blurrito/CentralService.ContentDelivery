using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CentralService.Api.ContentDelivery.Core
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public IReadOnlyCollection<string> GameCodes => _GameCodes;
        public DateTime StartDate { get; set; }
        public TimeSpan TotalLength => Items.Max(x => x.EndTime);
        public bool Recursive { get; set; }
        public string DirectoryPath { get; private set; }
        public bool ReturnSingleFile { get; set; }
        public IReadOnlyCollection<ScheduleItem> Items => _Items;

        private List<string> _GameCodes = new List<string>();
        private List<ScheduleItem> _Items = new List<ScheduleItem>();

        [JsonConstructor]
        public Schedule(string DirectoryPath, List<string> GameCodes, List<ScheduleItem> Items)
        {
            this.DirectoryPath = DirectoryPath;
            _GameCodes = GameCodes;
            _Items = Items;
        }

        public Schedule(string DirectoryPath, DateTime StartDate, bool Recursive)
        {
            if (DirectoryPath == null)
                throw new ArgumentNullException(nameof(DirectoryPath), "The provided directory path is null.");
            if (DirectoryPath == string.Empty)
                throw new ArgumentException(nameof(DirectoryPath), "The provided directory path is empty.");
            if (!Directory.Exists(DirectoryPath))
                throw new DirectoryNotFoundException("No directory matching the provided directory path could be found.");
            this.DirectoryPath = DirectoryPath;
            this.StartDate = StartDate;
            this.Recursive = Recursive;

            string[] DirectoryInfo = GetDirectoryInfo();
            if (DirectoryInfo.Length > 0)
                GetDirectoryFileListingFromDirectoryInfo(DirectoryInfo);
            else
                GetDirectoryFileListingFromFileList();
        }

        public void AddItem(string FileName)
        {
            if (!File.Exists($"{ DirectoryPath }\\{ FileName }"))
                throw new FileNotFoundException($"No file with name { FileName } could be found in the current directory.");
            _Items.Add(new ScheduleItem(FileName, new List<string>()));
        }

        private string[] GetDirectoryInfo()
        {
            string DirectoryInfoFilePath = $"{ DirectoryPath }\\DirectoryInfo.txt";
            if (File.Exists(DirectoryInfoFilePath))
            {
                string[] DirectoryInfo;
                using (StreamReader Reader = new StreamReader(new FileStream(DirectoryInfoFilePath, FileMode.Open, FileAccess.Read)))
                    DirectoryInfo = Reader.ReadToEnd().Replace("\r", "").Split('\n').Where(x => x != string.Empty).ToArray();

                if (DirectoryInfo.Length > 0)
                    return DirectoryInfo;
            }
            return new string[0];
        }

        private void GetDirectoryFileListingFromDirectoryInfo(string[] DirectoryInfo)
        {
            foreach (string DirectoryFile in DirectoryInfo)
            {
                string[] SplitProperties = DirectoryFile.Split('\t');
                if (File.Exists($"{ DirectoryPath }\\{ SplitProperties[0] }"))
                {
                    List<string> Properties = new List<string>();
                    for (int i = 1; i < SplitProperties.Length; i++)
                        if (SplitProperties[i] != string.Empty)
                            Properties.Add(SplitProperties[i]);
                    _Items.Add(new ScheduleItem(SplitProperties[0], Properties));
                }
            }
        }

        private void GetDirectoryFileListingFromFileList()
        {
            string[] FileList = Directory.GetFiles(DirectoryPath);
            foreach (string DirectoryFile in FileList)
                _Items.Add(new ScheduleItem(DirectoryFile, new List<string>()));
        }
    }
}
