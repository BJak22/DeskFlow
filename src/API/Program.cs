using API.Infrastructure;
using API.Application;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- SEKCJA KONFIGURACJI DI (Dependency Injection) ---

// 1. Rejestracja Bazy Danych
// 1. Pobieramy stringa z pliku (zamiast na sztywno)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Zabezpieczenie (zostawiamy je, bo jest mądre)
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Couldn't find string 'DefaultConnection'. Check your appsettings.json. file");
}

// 3. Rejestracja bazy
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// 2. Rejestracja naszych Serwisów
// AddScoped = Nowa instancja serwisu jest tworzona dla każdego zapytania HTTP.
// To najczęstszy cykl życia dla serwisów biznesowych.
builder.Services.AddScoped<IReservationService, ReservationService>();

// 3. Dodanie kontrolerów
builder.Services.AddControllers();

// 4. Swagger (dokumentacja)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- SEKCJA PIPELINE (Jak aplikacja przetwarza zapytania) ---


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); // Mapuje nasze klasy Controller na adresy URL

app.Run();