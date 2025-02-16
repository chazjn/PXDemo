using Microsoft.EntityFrameworkCore;
using PXDemo.Infrastructure.Features.DateTimeResolver;
using PXDemo.Infrastructure.Features.Ordering;
using PXDemo.Infrastructure.Models;
using PXDemo.Infrastructure.Persistance;
using PXDemo.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

builder.Services.AddPooledDbContextFactory<DeviceDbContext>((serviceProvider, options) =>
{
    options.UseInMemoryDatabase("InMemoryDb");
});

builder.Services.AddTransient<IDateTimeResolver, UtcDateTimeResolver>();
builder.Services.AddTransient<IDeviceService, DeviceService>();
builder.Services.AddTransient<IOrderStrategy<Device>>(serviceProvider =>
{
    var dateTimeResolver = serviceProvider.GetService<IDateTimeResolver>();
    return new DeviceOrderBySignalStrengthAndLastCommunication(dateTimeResolver, TimeSpan.FromMinutes(2));
});

var app = builder.Build();

using var scope = app.Services.CreateScope();
var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<DeviceDbContext>>();
var context = factory.CreateDbContext();

if(context.Database.IsInMemory())
{
    context.DeviceTypes.AddRange(
        new DeviceType { Id = 1, Name = "Video Doorbell" },
        new DeviceType { Id = 2, Name = "RFID Door Lock" },
        new DeviceType { Id = 3, Name = "Biometric Access Control" },
        new DeviceType { Id = 4, Name = "CCTV Camera" },
        new DeviceType { Id = 5, Name = "Motion Sensor" },
        new DeviceType { Id = 6, Name = "Smoke Sensor" },
        new DeviceType { Id = 7, Name = "CO Detector" }
        );

    context.Devices.AddRange(
        new Device { Id = Guid.NewGuid(), Name = "Front Doorbell", DeviceTypeId = 1 },
        new Device { Id = Guid.NewGuid(), Name = "Upstairs Door", DeviceTypeId = 2 },
        new Device { Id = Guid.NewGuid(), Name = "Basement Door", DeviceTypeId = 3 },
        new Device { Id = Guid.NewGuid(), Name = "Outside CCTV", DeviceTypeId = 4 },
        new Device { Id = Guid.NewGuid(), Name = "Inside CCTV", DeviceTypeId = 4 },
        new Device { Id = Guid.NewGuid(), Name = "East Hallway", DeviceTypeId = 5 },
        new Device { Id = Guid.NewGuid(), Name = "West Hallway", DeviceTypeId = 5 }
        );

    context.SaveChanges();
}
else
{
    context.Database.Migrate();
}
    
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();
