using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralService.Api.ContentDelivery.DTO.Database
{
    public class DeviceProfile
    {
        [Key]
        public int DeviceProfileId { get; set; }
        [Required]
        public long DeviceId { get; set; }
        [Required]
        public string GameCode { get; set; }
        [Required]
        public int GameProfileId { get; set; }
    }
}
