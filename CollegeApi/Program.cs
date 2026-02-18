using CollegeApi.Data;
using CollegeApi.MyLogging;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("Log/log.txt", rollingInterval: RollingInterval.Minute)
    .CreateLogger();

//use serilog along with built-in logger
//builder.Logging.AddSerilog();


//use this line to override the built-in logger
//builder.Host.UseSerilog();



//db configuration
builder.Services.AddDbContext<CollegeDBContext>(options =>
{
options.UseSqlServer(builder.Configuration.GetConnectionString("CollegeDBConnection"));


// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// 🔵 Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Singleton: Same instance throughout the app
//builder.Services.AddSingleton<IMyLogger, LogToFile>();

// Scoped: New instance per HTTP request
builder.Services.AddScoped<IMyLogger, LogToDB>();

// Transient: New instance every time DI container is asked
//builder.Services.AddTransient<IMyLogger, LogToServerMemory>();


var app = builder.Build();

// 🔵 Enable Swagger only in development (best practice)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
