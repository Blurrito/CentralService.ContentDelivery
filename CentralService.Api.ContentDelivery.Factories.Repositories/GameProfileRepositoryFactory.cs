using CentralService.Api.ContentDelivery.Interfaces.Repositories;
using CentralService.Api.ContentDelivery.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralService.Api.ContentDelivery.Factories.Repositories
{
    public static class GameProfileRepositoryFactory
    {
        public static IGameProfileRepository GetRepository() => new GameProfileRepository();
    }
}
