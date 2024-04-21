using DiscountService.GRPC;
using DiscountService.Infrastructure.Contexts;
using DiscountService.Infrastructure.MappingProfile;
using DiscountService.Model.Services;
using Microsoft.EntityFrameworkCore;
using SayyehBanTools.ConfigureService;
using SayyehBanTools.ConnectionDB;
using SayyehBanTools.MessagingBus.RabbitMQ.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DiscountDataBaseContext>(o => o.UseSqlServer(SqlServerConnection.ConnectionString("lS/0QdfPVVsm8RW32uiOAg==", "FHSoIP0ocdF5pa41ewZO9w==", "qJ04Z6QkP1Klb16OXdMFJA==", "2q1aEqJs3l1O1BFHZfcDsg==", "u2ecg20z8m8bv67g", "x1bvxlpm78193fyw")));

builder.Services.AddGrpc();

builder.Services.AddAutoMapper(typeof(DiscountMappingProfile));
builder.Services.AddTransient<IDiscountService, RDiscountService>();
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

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapGrpcService<GRPCDiscountService>();
app.MapControllers();

app.Run();
