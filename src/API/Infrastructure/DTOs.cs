namespace API.Infrastructure;


public class CreateReservationDto
{
    public int DeskId { get; set; }
    public DateTime Date { get; set; }
    public string UserName { get; set; }
}

public class ReservationResponseDto
{
    public int Id { get; set; }
    public string DeskCode { get; set; } 
    public DateTime Date { get; set; }
}