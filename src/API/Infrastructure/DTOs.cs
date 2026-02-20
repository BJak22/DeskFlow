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

public class UpdateReservationDto
{
    public int Id { get; set; }
    public string? UserName { get; set; } 
    public int? DeskId { get; set; } 
    public DateTime? Date { get; set; }
}

public class CreateDeskDto
{
    public string Code { get; set; }
    public bool HasDualMonitor { get; set; }
}

public class UpdateDeskDto
{
    public int Id { get; set; }
    public string? Code { get; set; }
    public bool? HasDualMonitor { get; set; }
}

public class DeskResponseDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public bool HasDualMonitor { get; set; }
}