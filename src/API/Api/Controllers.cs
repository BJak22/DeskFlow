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
            var res = await _service.CreateReservationAsync(dto);
            
            return Created($"api/reservations/{res}", res);
        }
        catch (Exception ex)
        {
            
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllReservationsForDesk(int deskId)
    {
            var res = await _service.GetDeskReservationsAsync(deskId);
            return Ok(res);
    }
    
}

//Desk Controller
[ApiController] 
[Route("api/[controller]")] 
public class DeskController : ControllerBase
{
    private readonly IDeskService _service;
    
    public DeskController(IDeskService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDeskDto dto)
    {
        try 
        {
            var res = await _service.CreateDeskAsync(dto);
            
            return Created($"api/desk/{res}", res);
        }
        catch (Exception ex)
        {
            
            return BadRequest(ex.Message);
        }
    }
    
}
