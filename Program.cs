using ForkliftControl.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure JSON options to make ASP.NET always output the manufacture date in ISO 8601 (else we get Invalid Date).
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Register DbContext with SQLite
builder.Services.AddDbContext<ForkliftContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ForkliftDb")));

// Register services
builder.Services.AddScoped<ForkliftControl.Services.CommandParserService>();

// Configure CORS to allow requests from the frontend application
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", 
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var services = scope.ServiceProvider;
        ForkliftContext context = null!;
        try
        {
            context = services.GetRequiredService<ForkliftContext>();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Failed to resolve ForkliftContext from service provider.");
            throw;
        }

        try
        {
            DbInitializer.Initialize(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while initializing the database.");
        throw;
    }
}

app.MapControllers();         
app.Run();