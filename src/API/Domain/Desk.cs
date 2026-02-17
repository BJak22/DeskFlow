namespace API.Domain;

// Desk.cs
public class Desk
{
    public int Id { get; set; }
    public string Code { get; set; } // np. "BIO-01"
    public bool HasDualMonitor { get; set; }

    // Relacja: Jedno biurko ma wiele rezerwacji.
    // Używamy 'virtual' dla Entity Framework (lazy loading), choć w nowoczesnym .NET często się to pomija na rzecz Eager Loading.
    public ICollection<Reservation> Reservations { get; set; } 
}