using Microsoft.EntityFrameworkCore;
using StudyCaseWebApi.Services.Entities;

namespace StudyCaseWebApi.Services;

public interface IDataBaseService
{
    DbSet<Users> Users();
    DbSet<GameRegistry> GameRegistry();
    DbSet<UserGames> UserGames();
    
    DbContext DbContext { get; }
    void SaveChanges();
    Task<int> SaveChangesAsync();
}