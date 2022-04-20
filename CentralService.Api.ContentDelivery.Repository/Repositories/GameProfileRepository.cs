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
    public class GameProfileRepository : Repository<DTO.Database.GameProfile>, IGameProfileRepository
    {
        public GameProfileRepository() : base() { }

        public Task AddGameProfile(GameProfile GameProfile)
        {
            throw new NotImplementedException();
        }

        public Task<List<GameProfile>> FindGameProfiles(Expression<Func<GameProfile, bool>> Predicate)
        {
            throw new NotImplementedException();
        }

        public Task<GameProfile?> GetGameProfile(int GameProfileId)
        {
            throw new NotImplementedException();
        }

        public Task<GameProfile?> GetGameProfile(string GameCode)
        {
            throw new NotImplementedException();
        }

        public Task<List<GameProfile>> GetGameProfiles()
        {
            throw new NotImplementedException();
        }

        public void RemoveGameProfile(GameProfile GameProfile)
        {
            throw new NotImplementedException();
        }

        public void UpdateGameProfile(GameProfile GameProfile)
        {
            throw new NotImplementedException();
        }
    }
}
