using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CentralService.Api.ContentDelivery.DTO.Api.ContentDelivery;

namespace CentralService.Api.ContentDelivery.Interfaces.Repositories
{
    public interface IDeviceProfileRepository : IRepository<DTO.Database.DeviceProfile>
    {
        Task<DeviceProfile?> GetDeviceProfile(long DeviceId, string GameCode);
        Task<List<DeviceProfile>> GetDeviceProfiles();
        Task<List<DeviceProfile>> FindDeviceProfiles(Expression<Func<DeviceProfile, bool>> Predicate);
        Task AddDeviceProfile(DeviceProfile DeviceProfile);
        void UpdateDeviceProfile(DeviceProfile DeviceProfile);
        void RemoveDeviceProfile(DeviceProfile DeviceProfile);
    }
}
