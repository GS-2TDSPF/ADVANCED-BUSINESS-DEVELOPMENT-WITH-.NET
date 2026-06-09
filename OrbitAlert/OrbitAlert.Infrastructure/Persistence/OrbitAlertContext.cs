using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Infrastructure.Persistence;

public class OrbitAlertContext(DbContextOptions<OrbitAlertContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Municipio> Municipios { get; set; }
    public DbSet<UsuarioMunicipio> UsuariosMunicipios { get; set; }
    public DbSet<ZonaRisco> ZonasRisco { get; set; }
    public DbSet<EstacaoIot> EstacoesIot { get; set; }
    public DbSet<LeituraIot> LeiturasIot { get; set; }
    public DbSet<TipoAlerta> TiposAlerta { get; set; }
    public DbSet<Alerta> Alertas { get; set; }
    public DbSet<AnaliseIa> AnalisesIa { get; set; }
    public DbSet<HistoricoAlerta> HistoricosAlerta { get; set; }
    public DbSet<Notificacao> Notificacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    
        modelBuilder.HasSequence<long>("SEQ_ALERTA");
        modelBuilder.HasSequence<long>("SEQ_ZONA_RISCO");
        modelBuilder.HasSequence<long>("SEQ_TIPO_ALERTA");
        modelBuilder.HasSequence<long>("SEQ_ESTACAO_IOT");
        modelBuilder.HasSequence<long>("SEQ_MUNICIPIO");
        modelBuilder.HasSequence<long>("SEQ_USUARIO");
        modelBuilder.HasSequence<long>("SEQ_LEITURA_IOT");
        modelBuilder.HasSequence<long>("SEQ_HISTORICO_ALERTA");
        modelBuilder.HasSequence<long>("SEQ_ANALISE_IA");
        modelBuilder.HasSequence<long>("SEQ_NOTIFICACAO");
        modelBuilder.HasSequence<long>("SEQ_USUARIO_MUNICIPIO");
    
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}