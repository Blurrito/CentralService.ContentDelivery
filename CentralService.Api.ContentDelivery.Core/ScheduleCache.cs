using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralService.Api.ContentDelivery.Core
{
    public class ScheduleCache
    {
        public int ScheduleId { get; set; }
        public IReadOnlyCollection<string> GameCodes => _GameCodes;
        public DateTime StartDate { get; set; }
        public TimeSpan ValidUntil => Items.Min(x => x.EndTime);
        public string DirectoryPath { get; private set; }
        public bool ReturnSingleFile { get; set; }
        public IReadOnlyCollection<ScheduleItem> Items => _Items;

        private List<string> _GameCodes = new List<string>();
        private List<ScheduleItem> _Items = new List<ScheduleItem>();

        public ScheduleCache(Schedule Schedule)
        {
            if (Schedule == null)
                throw new ArgumentNullException(nameof(Schedule), "The provided schedule object is null.");

            StartDate = Schedule.StartDate;
            DirectoryPath = Schedule.DirectoryPath;
            ReturnSingleFile = Schedule.ReturnSingleFile;

            foreach (string GameCode in Schedule.GameCodes)
                _GameCodes.Add(GameCode);

            double CurrentHourTotal = DateTime.Now.Subtract(Schedule.StartDate).TotalHours % Schedule.TotalLength.TotalHours;
            _Items = Schedule.Items.Where(x => x.StartTime.TotalHours >= CurrentHourTotal && x.EndTime.TotalHours < CurrentHourTotal).ToList();
        }
    }
}
