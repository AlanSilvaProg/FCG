using Microsoft.EntityFrameworkCore;
using StudyCaseWebApi.Services.Entities;

namespace StudyCaseWebApi.Services;

public abstract class DataBaseService : IDataBaseService
{
    public DbContext DbContext => GetDbContext();
    
    public void SaveChanges() => DbContext.SaveChanges();
    public Task<int> SaveChangesAsync() => DbContext.SaveChangesAsync();

    public abstract DbSet<Users> Users();
    public abstract DbSet<GameRegistry> GameRegistry();
    public abstract DbSet<UserGames> UserGames();
    protected abstract DbContext GetDbContext();
}