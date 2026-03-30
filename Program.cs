using expense_tracker.Models;
using Microsoft.EntityFrameworkCore;
using expense_tracker.DTO.Requests;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ExpenseDBContext>(options => 
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Makes use of data annotations to ensure passed in data meet's DTO requirements
builder.Services.AddValidation();

// Setup session information
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;   // Disallow JS, and mitigate XSS attacks
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.None; // Change this to work with HTTPs before finishing
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// account options
// Create 
app.MapPost("/api/credentials/create", async (HttpContext ctx, CreateUserRequest dto, ExpenseDBContext db) =>
{
    // Check for presence of username or email being present and unique
    // If not unique, reject request
    var existingDetails = await db.Credentials
        .Where(c => c.Username == dto.Username || c.Email == dto.Email)
        .FirstOrDefaultAsync();

    if (existingDetails != null)
    {
        return Results.Conflict();
    }

    // Hash the password
    string hashed_password = Sha256Hasher.HashString(dto.Password);

    // Create Model
    Credentials credentials = new Credentials
    {
        Username = dto.Username,
        HashedPassword = hashed_password,
        Email = dto.Email,
        Created = DateTime.UtcNow
    };

    try
    {
        // Create row in database
        await db.AddAsync(credentials);

        // If failed for, reject request
        
        await db.SaveChangesAsync();
    } catch (Exception ex)
    {
        Console.WriteLine("There was an exception: ", ex);
    }

    // Create session ID (Lifetime: 10 minutes)
    // Assoicate session ID with username
    // return session ID to the user
});

// Login
app.MapPost("/api/credentials/login", (ExpenseDBContext ctx) =>
{
    // Validate passed details
    // If not valid, reject request

    // Hash passed in password

    // Search Database for either username + password
    // if failed, reject request

    // Create session ID (Lifetime: 10 minutes)
    // Associate session ID with account ID
    // Return session ID to the user
});

// Sign out
app.MapPost("/api/credentials/sign-out", (ExpenseDBContext ctx) =>
{
    // Check for existance of passed in sessin id
    // If exists, remove it from the session
});


// Delete
app.MapPost("/api/credentials/delete", (ExpenseDBContext ctx) =>
{
    // Validate passed detailsn
    // If not valid, reject request

    // Hash passed in password

    // Search and delete username + password combination row
    // If not valie, reject request

    // Delete any associated session ID's for the id that you can
});


app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
