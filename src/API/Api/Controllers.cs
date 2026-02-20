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
            
            return Created($"api/reservations/{res.Id}", res);
        }
        catch (Exception ex)
        {
            
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("desk/{deskId}")]
    public async Task<IActionResult> GetAllReservationsForDesk(int deskId)
    {
        var res = await _service.GetDeskReservationsAsync(deskId);
        return Ok(res);
    }
    
    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(int id, UpdateReservationDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest("ID in URL and ID in data doesn't match");
        }
        
        try 
        {
            var res = await _service.UpdateReservationAsync(dto);
            
            return Ok(res);
        }
        catch (Exception ex)
        {
            
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReservation(int id)
    {
        try 
        {
            await _service.DeleteReservationAsync(id);
            return NoContent(); 
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
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
            
            return Created($"api/desk/{res.Id}", res);
        }
        catch (Exception ex)
        {
            
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllDesks()
    {
        var res = await _service.GetDesksAsync();
        return Ok(res);
    }
    
    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(int id, UpdateDeskDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest("ID in URL and ID in data doesn't match");
        }

        try 
        {
            var res = await _service.UpdateDeskAsync(dto);
            
            return Ok(res);
        }
        catch (Exception ex)
        {
            
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDesk(int id)
    {
        try 
        {
            await _service.DeleteDeskAsync(id);
            return NoContent(); 
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
