using StripsBL.Interfaces;
using StripsBL.Services;
using StripsDL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IStripsRepository>(new StripsRepository("Data Source=Radion\\sqlexpress;Initial Catalog=Strips;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"));
builder.Services.AddScoped<StripService>();
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
