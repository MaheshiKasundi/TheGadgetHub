using Microsoft.EntityFrameworkCore;
using GadgetCentralService.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- NEW: Step A - Define the CORS Policy ---
// This allows your frontend website to safely request data from this API.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()   // Allows your website port to connect.
                  .AllowAnyMethod()   // Allows all actions like GET and POST.
                  .AllowAnyHeader();  // Allows all request headers.
        });
});

// 2. Register Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 3. Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// --- NEW: Step B - Enable the CORS Policy ---
// This MUST be placed before MapControllers.
app.UseCors("AllowAll");

app.UseStaticFiles();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();