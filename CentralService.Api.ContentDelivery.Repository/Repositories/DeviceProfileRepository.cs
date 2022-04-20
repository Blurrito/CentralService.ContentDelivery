using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CentralService.Api.ContentDelivery.Interfaces.Repositories;
using CentralService.Api.ContentDelivery.DTO.Api.ContentDelivery;

namespace CentralService.Api.ContentDelivery.Repository.Repositories
{
    public class DeviceProfileRepository : Repository<DTO.Database.DeviceProfile>, IDeviceProfileRepository
    {
        public DeviceProfileRepository() : base() { }

        public Task AddDeviceProfile(DeviceProfile DeviceProfile)
        {
            throw new NotImplementedException();
        }

        public Task<List<DeviceProfile>> FindDeviceProfiles(Expression<Func<DeviceProfile, bool>> Predicate)
        {
            throw new NotImplementedException();
        }

        public Task<DeviceProfile?> GetDeviceProfile(long DeviceId, string GameCode)
        {
            throw new NotImplementedException();
        }

        public Task<List<DeviceProfile>> GetDeviceProfiles()
        {
            throw new NotImplementedException();
        }

        public void RemoveDeviceProfile(DeviceProfile DeviceProfile)
        {
            throw new NotImplementedException();
        }

        public void UpdateDeviceProfile(DeviceProfile DeviceProfile)
        {
            throw new NotImplementedException();
        }
    }
}
