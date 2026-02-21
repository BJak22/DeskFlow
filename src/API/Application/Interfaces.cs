using API.Infrastructure;
namespace API.Application;

public interface IReservationService
{
    Task<ReservationResponseDto> CreateReservationAsync(CreateReservationDto dto);
   Task<List<ReservationResponseDto>> GetDeskReservationsAsync(int deskId);
   Task<ReservationResponseDto> UpdateReservationAsync(UpdateReservationDto dto);
   Task DeleteReservationAsync(int id);
}

public interface IDeskService
{
    Task<DeskResponseDto> CreateDeskAsync(CreateDeskDto dto);
    Task<List<DeskResponseDto>> GetDesksAsync();
    Task<DeskResponseDto> UpdateDeskAsync(UpdateDeskDto dto);
    Task DeleteDeskAsync(int id);
}

public interface IAuthService
{
    Task RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
}