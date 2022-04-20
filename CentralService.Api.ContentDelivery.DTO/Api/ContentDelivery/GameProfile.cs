using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralService.Api.ContentDelivery.DTO.Api.ContentDelivery
{
    public struct GameProfile
    {
        public int GameProfileId { get; set; }
        public string GameCode { get; set; }
        public string ScheduleFilePath { get; set; }
    }
}

