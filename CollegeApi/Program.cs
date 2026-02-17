using CollegeApi.MyLogging;

var builder = WebApplication.CreateBuilder(args);

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
