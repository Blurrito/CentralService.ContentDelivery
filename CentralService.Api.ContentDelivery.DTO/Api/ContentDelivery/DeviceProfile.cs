using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralService.Api.ContentDelivery.DTO.Api.ContentDelivery
{
    public struct DeviceProfile
    {
        public int DeviceProfileId { get; set; }
        public long DeviceId { get; set; }
        public string GameCode { get; set; }
        public int GameProfileId { get; set; }
    }
}
