namespace API.Domain;
public class Reservation
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string UserName { get; set; } // Uproszczenie zamiast tabeli User

    // Klucz obcy (Foreign Key)
    public int DeskId { get; set; }
    
    // Właściwość nawigacyjna (Navigation Property)
    // Dzięki temu w kodzie możesz zrobić: reservation.Desk.Code
    public Desk Desk { get; set; }
}