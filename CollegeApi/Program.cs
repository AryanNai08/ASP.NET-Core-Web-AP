using CollegeApi.Configurations;
using CollegeApi.Data;
using CollegeApi.Data.Repository;
using CollegeApi.MyLogging;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Runtime.CompilerServices;

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
    options.UseSqlServer(builder.Configuration.GetConnectionString("CollegeAppDBConnection"));
});

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// 🔵 Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//auto mapper configuration
builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

builder.Services.AddScoped<IStudentRepository,StudentRepository>();

builder.Services.AddScoped(typeof(ICollegeRepository<>),typeof(CollegeRepository<>));


//added cors
builder.Services.AddCors(options => options.AddPolicy("MyTestCORS", policy =>
{
    //allowed all origin
    //policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();

    //allow only few origin
    policy.WithOrigins("http://localhost:5173");

}));



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
//this line need to be added after routes and before authorization
app.UseCors("MyTestCORS");

app.UseAuthorization();

app.MapControllers();

app.Run();
