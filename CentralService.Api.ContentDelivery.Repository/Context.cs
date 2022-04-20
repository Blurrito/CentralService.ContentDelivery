using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CentralService.Api.ContentDelivery.DTO.Database;

namespace CentralService.Api.ContentDelivery.Repository
{
    public class Context : DbContext
    {
        public DbSet<DeviceProfile> DeviceProfiles { get; }
        public DbSet<GameProfile> GameProfiles { get; }

        public Context() : base(GetDbContextOptions()) { }

        protected override void OnModelCreating(ModelBuilder Builder) { }
        private static DbContextOptions<Context> GetDbContextOptions() => new DbContextOptionsBuilder<Context>().UseSqlServer(Properties.Resources.LocalConnectionString).Options;
    }
}
