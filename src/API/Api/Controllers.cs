using Microsoft.AspNetCore.Mvc;
using API.Application;
using API.Infrastructure;
namespace API.Api;

[ApiController] // Automatyczna walidacja modelu
[Route("api/[controller]")] // Adres np. /api/reservations
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _service;

    // Znowu Dependency Injection. Wstrzykujemy INTERFEJS, nie konkretną klasę.
    // Dzięki temu łatwo to potem przetestować (mockowanie).
    public ReservationsController(IReservationService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateReservationDto dto)
    {
        try 
        {
            var id = await _service.CreateReservationAsync(dto);
            // Zwracamy kod 201 Created
            return Created($"api/reservations/{id}", id);
        }
        catch (Exception ex)
        {
            // W prawdziwym projekcie używa się globalnego middleware do błędów
            return BadRequest(ex.Message);
        }
    }
}