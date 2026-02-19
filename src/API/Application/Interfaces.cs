using API.Infrastructure;
namespace API.Application;

public interface IReservationService
{
    Task<ReservationResponseDto> CreateReservationAsync(CreateReservationDto dto);
   Task<List<ReservationResponseDto>> GetDeskReservationsAsync(int deskId);
}

public interface IDeskService
{
    Task<DeskResponseDto> CreateDeskAsync(CreateDeskDto dto);
}