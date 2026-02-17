using API.Infrastructure;
namespace API.Application;

// Interfejs - kontrakt, co nasz serwis potrafi robić
public interface IReservationService
{
    // Task oznacza, że metoda jest asynchroniczna (standard w webach)
    Task<int> CreateReservationAsync(CreateReservationDto dto);
}