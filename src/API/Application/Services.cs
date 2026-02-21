using API.Domain;
using API.Infrastructure;
using Microsoft.EntityFrameworkCore;
namespace API.Application;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


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
    
    public async Task<ReservationResponseDto> UpdateReservationAsync(UpdateReservationDto dto)
    {
    var toEdit = await _context.Reservations
        .Include(r => r.Desk) 
        .FirstOrDefaultAsync(r => r.Id == dto.Id);
        
    if (toEdit == null) 
        throw new Exception("Reservation does not exist!");
    
    if (!string.IsNullOrEmpty(dto.UserName))
    {
        toEdit.UserName = dto.UserName;
    }
    
    if (dto.Date.HasValue || dto.DeskId.HasValue)
    {
        var dateToCheck = dto.Date.HasValue ? dto.Date.Value.Date : toEdit.Date.Date;
        var deskIdToCheck = dto.DeskId.HasValue ? dto.DeskId.Value : toEdit.DeskId;
        
        var isTaken = await _context.Reservations.AnyAsync(r => 
            r.DeskId == deskIdToCheck && 
            r.Date.Date == dateToCheck && 
            r.Id != dto.Id);

        if (isTaken) throw new Exception("Desk already taken for this day!");
        
        if (dto.DeskId.HasValue && dto.DeskId.Value != toEdit.DeskId)
        {
            var newDesk = await _context.Desks.FindAsync(dto.DeskId.Value);
            if (newDesk == null) throw new Exception("New desk does not exist!");
            toEdit.Desk = newDesk;
            toEdit.DeskId = dto.DeskId.Value;
        }
        
        if (dto.Date.HasValue)
        {
            toEdit.Date = dto.Date.Value;
        }
    }
    
    await _context.SaveChangesAsync();
    
    return new ReservationResponseDto
    {
        Id = toEdit.Id,
        DeskCode = toEdit.Desk.Code, 
        Date = toEdit.Date,
    };
}
    
    public async Task DeleteReservationAsync(int id)
    {
        var check = await _context.Reservations.FindAsync(id);
        if (check == null)
        {
            throw new Exception("Reservation does not exist!");
        }
        
        _context.Reservations.Remove(check);
        await _context.SaveChangesAsync();
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
    
    public async Task<List<DeskResponseDto>> GetDesksAsync()
    {
        var query = _context.Desks
            .Select(d => new DeskResponseDto 
            {
                Id = d.Id,
                Code = d.Code,
                HasDualMonitor = d.HasDualMonitor
            });
        
        var resultList = await query.ToListAsync(); 
        
        return resultList; 
    }
    
    public async Task<DeskResponseDto> UpdateDeskAsync(UpdateDeskDto dto)
    {
        var toEdit = await _context.Desks
            .FirstOrDefaultAsync(r => r.Id == dto.Id);
        
        if (toEdit == null) 
            throw new Exception("Desk does not exist!");
    
        if (!string.IsNullOrEmpty(dto.Code))
        {
            toEdit.Code = dto.Code;
        }
        
        if (dto.HasDualMonitor is bool newValue)
        {
            toEdit.HasDualMonitor = newValue;
        }
    
        await _context.SaveChangesAsync();
    
        return new DeskResponseDto
        {
            Id = toEdit.Id,
            Code = toEdit.Code, 
            HasDualMonitor = toEdit.HasDualMonitor,
        };
    }
    
    public async Task DeleteDeskAsync(int id)
    {
        var check = await _context.Desks.FindAsync(id);
        if (check == null)
        {
            throw new Exception("Desk does not exist!");
        }
        
        _context.Desks.Remove(check);
        await _context.SaveChangesAsync();
    }
}

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task RegisterAsync(RegisterDto dto)
    {
        var userExists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
        if (userExists)
            throw new Exception("email already used");
        
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        
        var user = new User
        {
            Email = dto.Email,
            PasswordHash = passwordHash,
            Role = "User"
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        {
            throw new Exception("Incorrect email  or password!");
        }
        
        var token = GenerateJwtToken(user);
        return new AuthResponseDto { Token = token };
    }

    private string GenerateJwtToken(User user)
    {
        var jwtKey = _configuration["Jwt:Key"];
        var jwtIssuer = _configuration["Jwt:Issuer"];
        var jwtAudience = _configuration["Jwt:Audience"];
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}