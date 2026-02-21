namespace API.Domain;


public class Desk
{
    public int Id { get; set; }
    public string Code { get; set; } 
    public bool HasDualMonitor { get; set; }
    
    public ICollection<Reservation> Reservations { get; set; } 
}