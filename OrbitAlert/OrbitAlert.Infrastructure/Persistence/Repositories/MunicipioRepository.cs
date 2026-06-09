using Microsoft.EntityFrameworkCore;
using OrbitAlert.Application.Interfaces.Repositories;
using OrbitAlert.Domain.Entities;
using OrbitAlert.Infrastructure.Persistence;

namespace OrbitAlert.Infrastructure.Persistence.Repositories;

public class MunicipioRepository(OrbitAlertContext context) : IMunicipioRepository
{
    public IReadOnlyList<Municipio> GetAll() =>
        context.Municipios.AsNoTracking().ToList();

    public Municipio? GetById(long id) =>
        context.Municipios.AsNoTracking().FirstOrDefault(m => m.Id == id);

    public IReadOnlyList<Municipio> GetByEstado(string nmEstado) =>
        context.Municipios.AsNoTracking()
            .Where(m => m.NmEstado == nmEstado)
            .ToList();

    public void Add(Municipio municipio)
    {
        context.Entry(municipio).State = EntityState.Added;
        context.SaveChanges();
    }

    public void Update(Municipio municipio)
    {
        context.Entry(municipio).State = EntityState.Modified;
        context.SaveChanges();
    }

    public bool Delete(long id)
    {
        // ✅ FIX: Usar FirstOrDefault ao invés de Any
        var exists = context.Municipios.AsNoTracking()
            .FirstOrDefault(m => m.Id == id) != null;
        if (!exists) return false;

        var zonaIds = context.ZonasRisco.AsNoTracking()
            .Where(z => EF.Property<long>(z, "ID_MUNICIPIO") == id)
            .Select(z => z.Id).ToList();

        foreach (var zonaId in zonaIds)
        {
            var alertaIds = context.Alertas.AsNoTracking()
                .Where(a => EF.Property<long>(a, "ID_ZONA") == zonaId)
                .Select(a => a.Id).ToList();

            foreach (var alertaId in alertaIds)
            {
                context.Database.ExecuteSqlRaw("DELETE FROM TB_ANALISE_IA WHERE ID_ALERTA = {0}", alertaId);
                context.Database.ExecuteSqlRaw("DELETE FROM TB_HISTORICO_ALERTA WHERE ID_ALERTA = {0}", alertaId);
                context.Database.ExecuteSqlRaw("DELETE FROM TB_NOTIFICACAO WHERE ID_ALERTA = {0}", alertaId);
            }

            context.Database.ExecuteSqlRaw("DELETE FROM TB_ALERTA WHERE ID_ZONA = {0}", zonaId);

            var estacaoIds = context.EstacoesIot.AsNoTracking()
                .Where(e => EF.Property<long>(e, "ID_ZONA") == zonaId)
                .Select(e => e.Id).ToList();

            foreach (var estacaoId in estacaoIds)
                context.Database.ExecuteSqlRaw("DELETE FROM TB_LEITURA_IOT WHERE ID_ESTACAO = {0}", estacaoId);

            context.Database.ExecuteSqlRaw("DELETE FROM TB_ESTACAO_IOT WHERE ID_ZONA = {0}", zonaId);
        }

        context.Database.ExecuteSqlRaw("DELETE FROM TB_ZONA_RISCO WHERE ID_MUNICIPIO = {0}", id);
        context.Database.ExecuteSqlRaw("DELETE FROM TB_USUARIO_MUNICIPIO WHERE ID_MUNICIPIO = {0}", id);
        context.Database.ExecuteSqlRaw("DELETE FROM TB_MUNICIPIO WHERE ID_MUNICIPIO = {0}", id);

        return true;
    }

    public void SaveChanges() => context.SaveChanges();
}