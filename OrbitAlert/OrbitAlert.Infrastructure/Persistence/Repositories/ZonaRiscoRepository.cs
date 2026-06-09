using Microsoft.EntityFrameworkCore;
using OrbitAlert.Application.Interfaces.Repositories;
using OrbitAlert.Domain.Entities;
using OrbitAlert.Infrastructure.Persistence;

namespace OrbitAlert.Infrastructure.Persistence.Repositories;

public class ZonaRiscoRepository(OrbitAlertContext context) : IZonaRiscoRepository
{
    public IReadOnlyList<ZonaRisco> GetAll() =>
        context.ZonasRisco.AsNoTracking().Include(z => z.Municipio).ToList();

    public ZonaRisco? GetById(long id) =>
        context.ZonasRisco.AsNoTracking().Include(z => z.Municipio).FirstOrDefault(z => z.Id == id);

    public IReadOnlyList<ZonaRisco> GetByMunicipio(long idMunicipio) =>
        context.ZonasRisco.AsNoTracking().Include(z => z.Municipio).Where(z => z.Municipio.Id == idMunicipio).ToList();

    public void Add(ZonaRisco zona)
    {
        context.Entry(zona).State = EntityState.Added;
        context.Entry(zona).Property("ID_MUNICIPIO").CurrentValue = zona.Municipio.Id;
        context.SaveChanges();
    }

    public void Update(ZonaRisco zona)
    {
        context.Entry(zona).State = EntityState.Modified;
        context.Entry(zona).Property("ID_MUNICIPIO").CurrentValue = zona.Municipio.Id;
        context.SaveChanges();
    }

    public bool Delete(long id)
    {
        // ✅ FIX: Usar FirstOrDefault ao invés de Any
        var exists = context.ZonasRisco.AsNoTracking()
            .FirstOrDefault(z => z.Id == id) != null;
        if (!exists) return false;

        var alertaIds = context.Alertas.AsNoTracking()
            .Where(a => EF.Property<long>(a, "ID_ZONA") == id)
            .Select(a => a.Id).ToList();

        foreach (var alertaId in alertaIds)
        {
            context.Database.ExecuteSqlRaw("DELETE FROM TB_ANALISE_IA WHERE ID_ALERTA = {0}", alertaId);
            context.Database.ExecuteSqlRaw("DELETE FROM TB_HISTORICO_ALERTA WHERE ID_ALERTA = {0}", alertaId);
            context.Database.ExecuteSqlRaw("DELETE FROM TB_NOTIFICACAO WHERE ID_ALERTA = {0}", alertaId);
        }

        context.Database.ExecuteSqlRaw("DELETE FROM TB_ALERTA WHERE ID_ZONA = {0}", id);

        var estacaoIds = context.EstacoesIot.AsNoTracking()
            .Where(e => EF.Property<long>(e, "ID_ZONA") == id)
            .Select(e => e.Id).ToList();

        foreach (var estacaoId in estacaoIds)
            context.Database.ExecuteSqlRaw("DELETE FROM TB_LEITURA_IOT WHERE ID_ESTACAO = {0}", estacaoId);

        context.Database.ExecuteSqlRaw("DELETE FROM TB_ESTACAO_IOT WHERE ID_ZONA = {0}", id);
        context.Database.ExecuteSqlRaw("DELETE FROM TB_ZONA_RISCO WHERE ID_ZONA = {0}", id);

        return true;
    }

    public void SaveChanges() => context.SaveChanges();
}