using CentralService.Api.ContentDelivery.DTO.Api.ContentDelivery;
using CentralService.Api.ContentDelivery.Factories.Repositories;
using CentralService.Api.ContentDelivery.Interfaces.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralService.Api.ContentDelivery.Core
{
    public class ScheduleManager : IDisposable
    {
        private static readonly object _ScheduleWriteLock = new object();

        public ScheduleManager()
        {

        }

        public async Task<int> GetFileCount(long DeviceId, string GameCode)
        {
            ScheduleCache Cache = await GetSchedule(DeviceId, GameCode);
            if (Cache != null)
                return Cache.ReturnSingleFile ? Math.Min(1, Cache.Items.Count) : Cache.Items.Count;
            return 0;
        }

        public async Task<string> GetFileList(long DeviceId, string GameCode)
        {
            ScheduleCache Cache = await GetSchedule(DeviceId, GameCode);
            if (Cache != null)
            {
                string FileList = string.Empty;
                foreach (ScheduleItem Item in Cache.Items)
                {
                    FileList += Item.FileName;
                    foreach (string Attribute in Item.Attributes)
                        FileList += $"\t{ Attribute }";
                    FileList += "\r\n";
                }
                return FileList;
            }
            return "\r\n";
        }

        public async Task<byte[]> GetFileContent(long DeviceId, string GameCode, string FileName)
        {
            ScheduleCache Cache = await GetSchedule(DeviceId, GameCode);
            if (Cache != null)
            {
                string FilePath = $"{ Cache.DirectoryPath }\\{ FileName }";
                if (!File.Exists(FilePath))
                    throw new FileNotFoundException($"Could not find { FilePath }.");

                byte[] FileContent;
                using (BinaryReader Reader = new BinaryReader(new FileStream(FilePath, FileMode.Open, FileAccess.Read)))
                    FileContent = Reader.ReadBytes((int)Reader.BaseStream.Length);
                return FileContent;
            }
            return null;
        }

        public async Task<ScheduleCache> GetSchedule(long DeviceId, string GameCode)
        {
            DeviceProfile? ExistingProfile;
            await using (IDeviceProfileRepository Repository = DeviceProfileRepositoryFactory.GetRepository())
                ExistingProfile = await Repository.GetDeviceProfile(DeviceId, GameCode);

            if (ExistingProfile.HasValue)
                return await FindScheduleCache(ExistingProfile.Value.DeviceProfileId);
            else
                return await FindScheduleCache(GameCode);
        }

        private async Task<ScheduleCache> FindScheduleCache(int GameProfileId)
        {
            ScheduleCache FoundCache = ScheduleCacheManager.GetScheduleCache(GameProfileId);
            if (FoundCache != null)
                return FoundCache;

            string ScheduleFilePath = await GetScheduleFilePath(GameProfileId);
            if (ScheduleFilePath != string.Empty)
                return ScheduleCacheManager.CreateScheduleCache(ScheduleFilePath);
            return null;
        }

        private async Task<ScheduleCache> FindScheduleCache(string GameCode)
        {
            ScheduleCache FoundCache = ScheduleCacheManager.GetScheduleCache(GameCode);
            if (FoundCache != null)
                return FoundCache;

            string ScheduleFilePath = await GetScheduleFilePath(GameCode);
            if (ScheduleFilePath != string.Empty)
                return ScheduleCacheManager.CreateScheduleCache(ScheduleFilePath);
            return null;
        }

        private async Task<string> GetScheduleFilePath(int GameProfileId)
        {
            GameProfile? ExistingGameProfile;
            await using (IGameProfileRepository Repository = GameProfileRepositoryFactory.GetRepository())
                ExistingGameProfile = await Repository.GetGameProfile(GameProfileId);
            if (ExistingGameProfile.HasValue)
                return ExistingGameProfile.Value.ScheduleFilePath;
            return string.Empty;
        }

        private async Task<string> GetScheduleFilePath(string GameCode)
        {
            GameProfile? ExistingGameProfile;
            await using (IGameProfileRepository Repository = GameProfileRepositoryFactory.GetRepository())
                ExistingGameProfile = await Repository.GetGameProfile(GameCode);
            if (ExistingGameProfile.HasValue)
                return ExistingGameProfile.Value.ScheduleFilePath;
            return string.Empty;
        }

        public void Dispose() { }
    }
}
