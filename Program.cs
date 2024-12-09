
using ProductsAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", builder =>
        builder.WithOrigins("http://localhost:4200")  // Angular app URL
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials());
});



// Add services to the container.
builder.Services.AddSingleton<ProductRepository>();

builder.Services.AddControllers();

var app = builder.Build();

// Use CORS
app.UseCors("AllowLocalhost");

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "API is running...");

app.Run();
