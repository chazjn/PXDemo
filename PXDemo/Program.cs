using Microsoft.EntityFrameworkCore;
using PXDemo.Infrastructure.Persistance;
using PXDemo.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPooledDbContextFactory<DataContext>((sp, options) =>
{
    options.UseInMemoryDatabase("InMemoryDb");
});

builder.Services.AddTransient<IDeviceService, DeviceService>();


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
