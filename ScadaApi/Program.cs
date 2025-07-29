using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScadaApi;
using ScadaApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddResponseCaching();
builder.Services.AddControllers(options =>
{
    options.CacheProfiles.Add("Default5",
        new CacheProfile()
        {
            Duration = 5,
            Location = ResponseCacheLocation.Any,
            NoStore = false
        });
});

builder.Services.AddControllers();
builder.Services.AddDbContext<RtuDbContext>(opt =>
    opt.UseInMemoryDatabase("RtuDb"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<PointChangeSim>();



var app = builder.Build();


var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<RtuDbContext>();
var time = DateTime.Now;
context.Rtus.AddRange(
    new Rtu { RtuId = 1, Name = "Alpha", Points = [1, 2, 3, 4, 5] },
    new Rtu { RtuId = 2, Name = "Bravo", Points = [6, 7, 8, 9, 10] },
    new Rtu { RtuId = 3, Name = "Charlie", Points = [11, 12, 13, 14, 15] },
    new Rtu { RtuId = 4, Name = "Delta", Points = [16, 17, 18, 19, 20] }
);
context.Points.AddRange(
    new Point { PointId = 1, RtuId = 1, Name = "A", Value = 3, TimeStamp = time },
    new Point { PointId = 2, RtuId = 1, Name = "B", Value = 1, TimeStamp = time },
    new Point { PointId = 3, RtuId = 1, Name = "C", Value = 4, TimeStamp = time },
    new Point { PointId = 4, RtuId = 1, Name = "D", Value = 2, TimeStamp = time },
    new Point { PointId = 5, RtuId = 1, Name = "E", Value = 6, TimeStamp = time },
    new Point { PointId = 6, RtuId = 2, Name = "F", Value = 5, TimeStamp = time },
    new Point { PointId = 7, RtuId = 2, Name = "G", Value = 8, TimeStamp = time },
    new Point { PointId = 8, RtuId = 2, Name = "H", Value = 1, TimeStamp = time },
    new Point { PointId = 9, RtuId = 2, Name = "I", Value = 2, TimeStamp = time },
    new Point { PointId = 10, RtuId = 2, Name = "J", Value = 3, TimeStamp = time },
    new Point { PointId = 11, RtuId = 3, Name = "K", Value = 5, TimeStamp = time },
    new Point { PointId = 12, RtuId = 3, Name = "L", Value = 8, TimeStamp = time },
    new Point { PointId = 13, RtuId = 3, Name = "M", Value = 9, TimeStamp = time },
    new Point { PointId = 14, RtuId = 3, Name = "N", Value = 1, TimeStamp = time },
    new Point { PointId = 15, RtuId = 3, Name = "O", Value = 2, TimeStamp = time },
    new Point { PointId = 16, RtuId = 4, Name = "P", Value = 4, TimeStamp = time },
    new Point { PointId = 17, RtuId = 4, Name = "Q", Value = 5, TimeStamp = time },
    new Point { PointId = 18, RtuId = 4, Name = "R", Value = 8, TimeStamp = time },
    new Point { PointId = 19, RtuId = 4, Name = "S", Value = 9, TimeStamp = time },
    new Point { PointId = 20, RtuId = 4, Name = "T", Value = 6, TimeStamp = time }
);
context.SaveChanges();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseResponseCaching();

app.UseApiKeyAuth();

app.UseAuthorization();

app.MapControllers();

app.Run();
