using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralService.Api.ContentDelivery.DTO.Database
{
    public class GameProfile
    {
        [Key]
        public int GameProfileId { get; set; }
        [Required]
        public string GameCode { get; set; }
        [Required]
        public string ScheduleFilePath { get; set; }

        public List<DeviceProfile> DeviceProfiles { get; set; }
    }
}
