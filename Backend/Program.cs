using Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Register services with their interfaces
builder.Services.AddSingleton<IPhoneService, PhoneService>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder => builder
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseCors("AllowAngularApp");
app.UseAuthorization();
app.MapControllers();

// Add a simple root endpoint
app.MapGet("/", () => "Mobile Phone Management API Server is running!");

// Set the server to run on the specified port
app.Run("http://localhost:5218");