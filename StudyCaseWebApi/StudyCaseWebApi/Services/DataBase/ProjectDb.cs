using Microsoft.EntityFrameworkCore;
using StudyCaseWebApi.Services.Entities;

namespace StudyCaseWebApi.Services;

public class ProjectDb() : DbContext
{
    public DbSet<Users> Users { get; set; }
    public DbSet<GameRegistry> GameRegistry { get; set; }
    public DbSet<UserGames> UserGames { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>()
            .HasKey(u => u.id);
        modelBuilder.Entity<GameRegistry>()
            .HasKey(u => u.id);
        modelBuilder.Entity<UserGames>()
            .HasKey(u => u.user_id);
        base.OnModelCreating(modelBuilder);
    }
}