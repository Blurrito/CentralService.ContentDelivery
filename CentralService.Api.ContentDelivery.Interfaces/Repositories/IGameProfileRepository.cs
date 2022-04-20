using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CentralService.Api.ContentDelivery.DTO.Api.ContentDelivery;

namespace CentralService.Api.ContentDelivery.Interfaces.Repositories
{
    public interface IGameProfileRepository : IRepository<DTO.Database.GameProfile>
    {
        Task<GameProfile?> GetGameProfile(int GameProfileId);
        Task<GameProfile?> GetGameProfile(string GameCode);
        Task<List<GameProfile>> GetGameProfiles();
        Task<List<GameProfile>> FindGameProfiles(Expression<Func<GameProfile, bool>> Predicate);
        Task AddGameProfile(GameProfile GameProfile);
        void UpdateGameProfile(GameProfile GameProfile);
        void RemoveGameProfile(GameProfile GameProfile);
    }
}