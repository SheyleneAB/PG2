using FitnessDB;
using FitnessDomein.Interfaces;
using FitnessDomein.Services;
using Microsoft.EntityFrameworkCore;
using FitnessDB.Models;
using FitnessDB.Repositories; // Add this line to include the missing namespace

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IProgramRepositoryEF>(new ProgramRepositoryEF("Data Source=radion\\sqlexpress;Initial Catalog=GymTest;Integrated Security=True;Encrypt=False"));
builder.Services.AddScoped<ProgramService>();
builder.Services.AddSingleton<IMemberRepositoryEF>(new MemberRepositoryEF("Data Source=radion\\sqlexpress;Initial Catalog=GymTest;Integrated Security=True;Encrypt=False"));
builder.Services.AddScoped<MemberService>();
//builder.Services.AddScoped<IMemberService, MemberService>();

builder.Services.AddDbContext<GymTestContextEF>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
