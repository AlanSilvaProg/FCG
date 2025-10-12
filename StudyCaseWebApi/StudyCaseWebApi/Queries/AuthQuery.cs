using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using StudyCaseWebApi.Services;
using StudyCaseWebApi.Services.Entities;

namespace StudyCaseWebApi.Queries;

public class AuthQuery//IDistributedCache cache)
{
    //private readonly IDistributedCache _cache = cache;

    public IQueryable<Users> GetUsers([Service] IDataBaseService dataBaseService) => dataBaseService.Users();
    
    public Users? GetUserGames(Guid id, [Service] IDataBaseService dataBaseService)
    {
        var key = $"get-users-games-{id}";
        
        //ToDo: Make it works ( Redis example )
        // var cached = await _cache.GetStringAsync(key);
        //
        //  if (cached != null)
        //      return JsonSerializer.Deserialize<Users>(cached);
        
        var user = dataBaseService.Users()
            .Where(u => u.id == id)
            .Include(u => u.userGames)
            .FirstOrDefault();
        
        // if (user != null)
        //     await _cache.SetStringAsync(
        //         key,
        //         JsonSerializer.Serialize(user),
        //         new DistributedCacheEntryOptions
        //         {
        //             AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60)
        //         });
    
        return user;
    }
}