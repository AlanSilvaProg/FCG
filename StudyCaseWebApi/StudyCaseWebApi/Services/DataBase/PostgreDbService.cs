using Microsoft.EntityFrameworkCore;
using StudyCaseWebApi.Services.Entities;

namespace StudyCaseWebApi.Services;

public class PostgreDbService(IConfiguration configuration) : DataBaseService
{ 
    private readonly ProjectDb _dbContext = new PostgreDb(configuration);
    
    public override DbSet<Users> Users() => _dbContext.Users;
    public override DbSet<GameRegistry> GameRegistry() => _dbContext.GameRegistry;
    public override DbSet<UserGames> UserGames() => _dbContext.UserGames;

    protected override DbContext GetDbContext() => _dbContext;
}

public class PostgreDb(IConfiguration configuration) : ProjectDb
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseNpgsql(@$"Host={configuration["DbConnection:Host"]};Username={configuration["DbConnection:Username"]};Password={configuration["DbConnection:Password"]};Database={configuration["DbConnection:Database"]}");
}