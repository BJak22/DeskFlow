using API.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure;


// Dziedziczymy po DbContext - to główna klasa EF Core
public class AppDbContext : DbContext
{
    // Konstruktor przekazuje opcje (np. ConnectionString) do klasy bazowej
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // To są nasze tabelki w bazie
    public DbSet<Desk> Desks { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    // Tutaj konfigurujemy szczegóły relacji (Fluent API)
    // To jest bardziej profesjonalne niż używanie atrybutów [ForeignKey] nad polami
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Desk)            // Rezerwacja ma jedno biurko
            .WithMany(d => d.Reservations)  // Biurko ma wiele rezerwacji
            .HasForeignKey(r => r.DeskId);  // Kluczem łączącym jest DeskId
    }
}