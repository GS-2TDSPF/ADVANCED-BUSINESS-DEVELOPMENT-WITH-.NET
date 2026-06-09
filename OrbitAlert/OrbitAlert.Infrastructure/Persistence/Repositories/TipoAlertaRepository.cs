using Microsoft.EntityFrameworkCore;
using OrbitAlert.Application.Interfaces.Repositories;
using OrbitAlert.Domain.Entities;
using OrbitAlert.Infrastructure.Persistence;

namespace OrbitAlert.Infrastructure.Persistence.Repositories;

public class TipoAlertaRepository(OrbitAlertContext context) : ITipoAlertaRepository
{
    public IReadOnlyList<TipoAlerta> GetAll() => context.TiposAlerta.AsNoTracking().ToList();
    public TipoAlerta? GetById(long id) => context.TiposAlerta.AsNoTracking().FirstOrDefault(t => t.Id == id);

    public void Add(TipoAlerta tipo) { context.TiposAlerta.Add(tipo); context.SaveChanges(); }

    public void Update(TipoAlerta tipo)
    {
        context.Entry(tipo).State = EntityState.Modified;
        context.SaveChanges();
    }

    public bool Delete(long id)
    {
        var exists = context.TiposAlerta.AsNoTracking().Any(t => t.Id == id);
        if (!exists) return false;

        var alertaIds = context.Alertas.AsNoTracking()
            .Where(a => EF.Property<long>(a, "ID_TIPO_ALERTA") == id)
            .Select(a => a.Id).ToList();

        foreach (var alertaId in alertaIds)
        {
            context.Database.ExecuteSqlRaw("DELETE FROM TB_ANALISE_IA WHERE ID_ALERTA = {0}", alertaId);
            context.Database.ExecuteSqlRaw("DELETE FROM TB_HISTORICO_ALERTA WHERE ID_ALERTA = {0}", alertaId);
            context.Database.ExecuteSqlRaw("DELETE FROM TB_NOTIFICACAO WHERE ID_ALERTA = {0}", alertaId);
        }

        context.Database.ExecuteSqlRaw("DELETE FROM TB_ALERTA WHERE ID_TIPO_ALERTA = {0}", id);
        context.Database.ExecuteSqlRaw("DELETE FROM TB_TIPO_ALERTA WHERE ID_TIPO_ALERTA = {0}", id);

        return true;
    }

    public void SaveChanges() => context.SaveChanges();
}