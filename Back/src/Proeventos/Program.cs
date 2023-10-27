using Microsoft.EntityFrameworkCore;
using ProEventos.Application;
using ProEventos.Application.Interfaces;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence.Persist;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ILotService, LotService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IGeneralPersist, GeneralPersist>();
builder.Services.AddScoped<IEventPersist, EventPersist>();
builder.Services.AddScoped<ILotPersist, LotPersist>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProEventosContext>(
    context => context.UseSqlite((builder.Configuration.GetConnectionString("Default")))
);
builder.Services.AddCors();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(cors => cors.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.MapControllers();

app.Run();