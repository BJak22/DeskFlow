using API.Domain;
using API.Infrastructure;
namespace API.Application;

// Implementacja serwisu
public class ReservationService : IReservationService
{
    private readonly AppDbContext _context;

    // DEPENDENCY INJECTION (Wstrzykiwanie Zależności)
    // Nie tworzymy tutaj "new AppDbContext()". 
    // Kontener DI sam nam go poda w konstruktorze!
    public ReservationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateReservationAsync(CreateReservationDto dto)
    {
        // 1. Walidacja biznesowa (przykład)
        // Sprawdzamy czy biurko istnieje
        var desk = await _context.Desks.FindAsync(dto.DeskId);
        if (desk == null) 
            throw new Exception("Biurko nie istnieje!");

        // 2. Mapowanie DTO na Entcję (ręczne - w pracy używa się AutoMappera)
        var reservation = new Reservation
        {
            DeskId = dto.DeskId,
            Date = dto.Date,
            UserName = dto.UserName
        };

        // 3. Dodanie do kontekstu (jeszcze nie do bazy!)
        _context.Reservations.Add(reservation);

        // 4. Zapis do bazy (tu leci INSERT INTO SQL)
        await _context.SaveChangesAsync();

        return reservation.Id;
    }
}