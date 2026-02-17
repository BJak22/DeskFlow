using API.Infrastructure;
namespace API.Application;

public interface IReservationService
{
    Task<int> CreateReservationAsync(CreateReservationDto dto);
}