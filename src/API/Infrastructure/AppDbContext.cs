using API.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure;



public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Desk> Desks { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Desk)            
            .WithMany(d => d.Reservations)  
            .HasForeignKey(r => r.DeskId);  
    }
}