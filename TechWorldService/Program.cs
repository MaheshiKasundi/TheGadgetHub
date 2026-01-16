using Microsoft.EntityFrameworkCore;
using TechWorldService.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- NEW: Step A - Define the CORS Policy ---
// This tells the server it is okay to accept requests from your frontend website
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin() 
                  .AllowAnyMethod()  
                  .AllowAnyHeader(); 
        });
});

// 2. Connect the Database Bridge
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 3. Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowAll");

app.UseStaticFiles();

app.UseAuthorization();

// 4. Map your Controllers to the API
app.MapControllers();

app.Run();