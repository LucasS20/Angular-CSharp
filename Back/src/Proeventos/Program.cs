using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ProEventos.Application;
using ProEventos.Application.Interfaces;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence.Persist;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddScoped<ILotService, LotService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IGeneralPersist, GeneralPersist>();
builder.Services.AddScoped<IEventPersist, EventPersist>();
builder.Services.AddScoped<ILotPersist, LotPersist>();

builder.Services.AddScoped<ISpeakerPersist, SpeakerPersist>();
builder.Services.AddScoped<ISpeakerService, SpeakerService>();
builder.Services.AddScoped<ISocialMediaPersist, SocialMediaPersist>();
builder.Services.AddScoped<ISocialMediaService, SocialMediaService>();

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
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
    RequestPath = new PathString("/Resources")
});
app.MapControllers();

app.Run();