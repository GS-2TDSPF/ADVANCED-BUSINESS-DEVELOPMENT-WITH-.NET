using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrbitAlert.API.Exceptions;
using OrbitAlert.API.Swagger;
using OrbitAlert.Application.Interfaces.Repositories;
using OrbitAlert.Application.Interfaces.Services;
using OrbitAlert.Infrastructure.Persistence;
using OrbitAlert.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IMunicipioRepository, MunicipioRepository>();
builder.Services.AddScoped<IUsuarioMunicipioRepository, UsuarioMunicipioRepository>();
builder.Services.AddScoped<IZonaRiscoRepository, ZonaRiscoRepository>();
builder.Services.AddScoped<IEstacaoIotRepository, EstacaoIotRepository>();
builder.Services.AddScoped<ILeituraIotRepository, LeituraIotRepository>();
builder.Services.AddScoped<ITipoAlertaRepository, TipoAlertaRepository>();
builder.Services.AddScoped<IAlertaRepository, AlertaRepository>();
builder.Services.AddScoped<IAnaliseIaRepository, AnaliseIaRepository>();
builder.Services.AddScoped<IHistoricoAlertaRepository, HistoricoAlertaRepository>();
builder.Services.AddScoped<INotificacaoRepository, NotificacaoRepository>();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IMunicipioService, MunicipioService>();
builder.Services.AddScoped<IUsuarioMunicipioService, UsuarioMunicipioService>();
builder.Services.AddScoped<IZonaRiscoService, ZonaRiscoService>();
builder.Services.AddScoped<IEstacaoIotService, EstacaoIotService>();
builder.Services.AddScoped<ILeituraIotService, LeituraIotService>();
builder.Services.AddScoped<ITipoAlertaService, TipoAlertaService>();
builder.Services.AddScoped<IAlertaService, AlertaService>();
builder.Services.AddScoped<IAnaliseIaService, AnaliseIaService>();
builder.Services.AddScoped<IHistoricoAlertaService, HistoricoAlertaService>();
builder.Services.AddScoped<INotificacaoService, NotificacaoService>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddDbContext<OrbitAlertContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("OrbitAlertOracle");
    options.UseOracle(connectionString).UseLazyLoadingProxies();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SchemaFilter<SwaggerExampleSchemaFilter>();
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "🛰️ OrbitAlert API",
        Version = "v1",
        Description = "Plataforma de alertas precoces de desastres naturais com dados orbitais Sentinel-1 e IA generativa. FIAP Global Solution 2026/1.",
        Contact = new OpenApiContact
        {
            Name = "OrbitAlert — 2TDS Fevereiro"
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});

var app = builder.Build();

app.UseExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "OrbitAlert API v1");
    options.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
