using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralService.Api.ContentDelivery.Core
{
    public static class ScheduleCacheManager
    {
        private static List<ScheduleCache> _ScheduleCache = new List<ScheduleCache>();
        private static readonly object _ScheduleCacheLock = new object();

        public static ScheduleCache GetScheduleCache(int ScheduleId)
        {
            ScheduleCache FoundCache;
            lock (_ScheduleCacheLock)
            {
                _ScheduleCache.RemoveAll(x => x.StartDate.AddHours(x.ValidUntil.TotalHours) <= DateTime.Now);
                FoundCache = _ScheduleCache.FirstOrDefault(x => x.ScheduleId == ScheduleId);
            }
            return FoundCache;
        }

        public static ScheduleCache GetScheduleCache(string GameCode)
        {
            ScheduleCache FoundCache;
            lock (_ScheduleCacheLock)
            {
                _ScheduleCache.RemoveAll(x => x.StartDate.AddHours(x.ValidUntil.TotalHours) <= DateTime.Now);
                FoundCache = _ScheduleCache.FirstOrDefault(x => x.GameCodes.Contains(GameCode));
            }
            return FoundCache;
        }

        public static ScheduleCache CreateScheduleCache(string ScheduleFilePath)
        {
            ScheduleCache CreatedCache = null;
            if (File.Exists(ScheduleFilePath))
            {
                string ScheduleFileContent = string.Empty;
                using (StreamReader Reader = new StreamReader(new FileStream(ScheduleFilePath, FileMode.Open, FileAccess.Read)))
                    ScheduleFileContent = Reader.ReadToEnd();
                CreatedCache = new ScheduleCache(JsonConvert.DeserializeObject<Schedule>(ScheduleFileContent));
            }
            return CreatedCache;
        }
    }
}
