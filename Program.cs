using Microsoft.EntityFrameworkCore;
using SuperHeroApi.Data;
using SuperHeroApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDataContext>((options) =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DBConnection"));
});
builder.Services.AddControllers();
builder.Services.AddScoped<SuperHeroesService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.MapGet("/api/superheores-2", () => Results.Ok());
app.UseHttpsRedirection();

app.MapControllers();

app.Run();


