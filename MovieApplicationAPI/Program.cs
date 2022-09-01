using Microsoft.EntityFrameworkCore;
using MovieApplicationAPI.Models.EFDataBase;
using MovieApplicationAPI.MovieData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DeltaXMovieApplicationContext>(item => item.UseSqlServer(builder.Configuration.GetConnectionString("MovieApplicationDBConnection")));
builder.Services.AddScoped<IMovieData, MockMovieData>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
