namespace API.Infrastructure;

// To wysyła użytkownik, żeby stworzyć rezerwację
public class CreateReservationDto
{
    public int DeskId { get; set; }
    public DateTime Date { get; set; }
    public string UserName { get; set; }
}

// To odsyłamy użytkownikowi (np. ukrywamy relacje, których nie potrzebuje)
public class ReservationResponseDto
{
    public int Id { get; set; }
    public string DeskCode { get; set; } // Zwracamy kod biurka, a nie całe biurko
    public DateTime Date { get; set; }
}