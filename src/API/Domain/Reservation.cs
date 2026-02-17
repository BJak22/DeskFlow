namespace API.Domain;
public class Reservation
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string UserName { get; set; } 
    public int DeskId { get; set; }
    public Desk Desk { get; set; }
}