using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralService.Api.ContentDelivery.Core
{
    public class ScheduleItem
    {
        public IReadOnlyCollection<string> GameCodes => _GameCodes;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string FileName { get; private set; }
        public IReadOnlyCollection<string> Attributes => _Attributes;

        private List<string> _GameCodes = new List<string>();
        private List<string> _Attributes = new List<string>();

        [JsonConstructor]
        public ScheduleItem(string FileName, List<string> GameCodes, List<string> Attributes)
        {
            this.FileName = FileName;
            _GameCodes = GameCodes;
            _Attributes = Attributes;
        }

        public ScheduleItem(string FileName, List<string> Attributes)
        {
            if (FileName == null || Attributes == null)
                throw new ArgumentException("One or more provided arguments are null.");
            this.FileName = FileName;
            _Attributes = Attributes;
            _Attributes.RemoveAll(x => x == null);
            _Attributes.RemoveAll(x => x == string.Empty);
        }

        public void AddGameCode(string GameCode)
        {
            if (GameCode == null)
                throw new ArgumentNullException(nameof(GameCode), "Provided game code is null.");
            if (GameCode.Length < 3)
                throw new ArgumentException(nameof(GameCode), "Game code needs to contain at least three characters.");
            string ProcessedGameCode = GameCode.Substring(0, 3).ToUpper();
            if (!GameCodes.Contains(ProcessedGameCode))
                _GameCodes.Add(ProcessedGameCode);
        }
        
        public void RemoveGameCode(string GameCode)
        {
            if (GameCode != null)
                _GameCodes.Remove(GameCode);
        }
    }
}
