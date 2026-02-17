using Microsoft.AspNetCore.Mvc;
using API.Application;
using API.Infrastructure;
namespace API.Api;

[ApiController] 
[Route("api/[controller]")] 
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _service;
    
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
            
            return Created($"api/reservations/{id}", id);
        }
        catch (Exception ex)
        {
            
            return BadRequest(ex.Message);
        }
    }
}