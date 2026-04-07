using expense_tracker.Models;
using Microsoft.EntityFrameworkCore;
using expense_tracker.DTO.Requests;

var builder = WebApplication.CreateBuilder(args);

var environment = builder.Configuration.GetConnectionString("Environment");

builder.Services.AddDbContext<ExpenseDBContext>(options => 
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DbDefaultConnection")
    ));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Makes use of data annotations to ensure passed in data meet's DTO requirements
builder.Services.AddValidation();

// Cache for sessions
builder.Services.AddDistributedMemoryCache();

// Setup session information, the tokens are passed automatically and retrieved automatically before the endpoints
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
app.MapPost("/api/credentials/create", async (HttpContext ctx, ExpenseDBContext db, CreateUserRequest dto) =>
{
    // Check for presence of username or email being present and unique
    // If not unique, reject request
    var existingDetails = await db.Credentials
        .Where(c => c.Username == dto.Username || c.Email == dto.Email)
        .FirstOrDefaultAsync();

    if (existingDetails != null)
    {
        return Results.Conflict("Username or Email is already taken");
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
        
        // save changes
        await db.SaveChangesAsync();
    } catch (Exception ex)
    {
        // If failed for, reject request with 500
        Console.WriteLine("There was an exception when addign new user details to the database: ", ex);
        return Results.InternalServerError("There was an error on the server, please try again");
    }

    // Create session ID 
    ctx.Session.SetString("user_id", credentials.Id.ToString());
    ctx.Session.SetString("username", credentials.Username);

    // return success to user
    return Results.Created();
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


// This handles my produciton migration since I don't want to include another step in my CICD pipeline. 
if (app.Environment.IsProduction())
{
    try
    {
        using var scope = app.Services.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<ExpenseDBContext>();

        db.Database.Migrate();
    } catch (Exception ex) {
        Console.WriteLine($"There was an error with migration: {ex}");
    }
}


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


app.UseSession();   // This must be after UseRouting
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
