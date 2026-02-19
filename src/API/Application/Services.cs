using API.Domain;
using API.Infrastructure;
using Microsoft.EntityFrameworkCore;
namespace API.Application;


public class ReservationService : IReservationService
{
    private readonly AppDbContext _context;
    public ReservationService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<ReservationResponseDto> CreateReservationAsync(CreateReservationDto dto)
    {
        
        var desk = await _context.Desks.FindAsync(dto.DeskId);
        if (desk == null) 
            throw new Exception("Desk does not exist!");
        
        var reservationDay = dto.Date.Date;
        var isTaken = await _context.Reservations.AnyAsync(r => 
            r.DeskId == dto.DeskId && 
            r.Date.Date == reservationDay);

        if (isTaken)
        {
            throw new Exception("Desk already taken for this day");
        }
        
        var reservation = new Reservation
        {
            DeskId = dto.DeskId,
            Date = dto.Date,
            UserName = dto.UserName
        };
        
        _context.Reservations.Add(reservation);

        
        await _context.SaveChangesAsync();
        
        var respond = new ReservationResponseDto
        {
            Id = reservation.Id,
            DeskCode = reservation.Desk.Code,
            Date = reservation.Date,
        };

        return respond;
    }
    public async Task<List<ReservationResponseDto>> GetDeskReservationsAsync(int deskId)
    {
        var query = _context.Reservations
            .Where(r => r.DeskId == deskId)
            .Select(r => new ReservationResponseDto 
            {
                Id = r.Id,
                DeskCode = r.Desk.Code,
                Date = r.Date
            });
        
        var resultList = await query.ToListAsync(); 
        
        return resultList; 
    }
}


public class DeskService : IDeskService
{
    private readonly AppDbContext _context;
    public DeskService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<DeskResponseDto> CreateDeskAsync(CreateDeskDto dto)
    {
        
        var check = await _context.Desks.FirstOrDefaultAsync(d => d.Code == dto.Code);
        if (check != null) 
            throw new Exception("Desk code already taken!");
        
        var desk = new Desk
        {
            Code = dto.Code,
            HasDualMonitor = dto.HasDualMonitor
        };
        
        _context.Desks.Add(desk);
        
        await _context.SaveChangesAsync();
        
        var respond = new DeskResponseDto
        {
            Id = desk.Id,
            Code = desk.Code,
            HasDualMonitor = desk.HasDualMonitor,
        };
        
        return respond;
    }
}