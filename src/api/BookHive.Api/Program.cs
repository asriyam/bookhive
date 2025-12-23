using BookHive.Api.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register data service for Phase 2 development
builder.Services.AddBookHive();

// Swagger 
builder.Services.AddEndpointsApiExplorer(); // Required for discovering endpoints
builder.Services.AddSwaggerGen();           // Registers the Swagger generator

// Configure CORS to allow requests from Angular client (localhost:4200)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   // Serves the generated JSON (e.g., /swagger/v1/swagger.json)
    app.UseSwaggerUI(); // Serves the interactive UI (e.g., /swagger/index.html)
}

app.UseHttpsRedirection();

// Apply CORS policy
app.UseCors("AllowAngularClient");

app.UseAuthorization();

app.MapControllers();

app.Run();
