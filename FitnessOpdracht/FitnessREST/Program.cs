using FitnessDB;
using FitnessDomein.Interfaces;
using FitnessDomein.Services;
using Microsoft.EntityFrameworkCore;
using FitnessDB.Models;
using FitnessDB.Repositories;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
string defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton<IProgramRepositoryEF>(new ProgramRepositoryEF(defaultConnectionString));
builder.Services.AddSingleton<IMemberRepositoryEF>(new MemberRepositoryEF(defaultConnectionString));
builder.Services.AddSingleton<IReservationRepositoryEF>(new ReservationRepositoryEF(defaultConnectionString));
builder.Services.AddSingleton<IEquipmentRepositoryEF>(new EquipmentRepositoryEF(defaultConnectionString));
builder.Services.AddSingleton<ITimeSlotRepositoryEF>(new TimeSlotRepositoryEF(defaultConnectionString));
builder.Services.AddScoped<ProgramService>();
builder.Services.AddScoped<MemberService>();
builder.Services.AddScoped<EquipmentService>();
builder.Services.AddScoped<ReservationService>();
builder.Services.AddScoped<TimeSlotService>();

builder.Services.AddControllers();
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

app.MapControllers();

app.Run();
