using API.Domain;
using API.Infrastructure;
namespace API.Application;


public class ReservationService : IReservationService
{
    private readonly AppDbContext _context;
    public ReservationService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<int> CreateReservationAsync(CreateReservationDto dto)
    {
        
        
        var desk = await _context.Desks.FindAsync(dto.DeskId);
        if (desk == null) 
            throw new Exception("Desk does not exist!");
        
        var reservation = new Reservation
        {
            DeskId = dto.DeskId,
            Date = dto.Date,
            UserName = dto.UserName
        };
        
        _context.Reservations.Add(reservation);

        
        await _context.SaveChangesAsync();

        return reservation.Id;
    }
}