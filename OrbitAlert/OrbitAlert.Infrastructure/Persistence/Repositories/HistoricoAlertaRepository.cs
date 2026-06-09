using Microsoft.EntityFrameworkCore;
using OrbitAlert.Application.Interfaces.Repositories;
using OrbitAlert.Domain.Entities;
using OrbitAlert.Infrastructure.Persistence;

namespace OrbitAlert.Infrastructure.Persistence.Repositories;

public class HistoricoAlertaRepository(OrbitAlertContext context) : IHistoricoAlertaRepository
{
    public IReadOnlyList<HistoricoAlerta> GetAll() =>
        context.HistoricosAlerta.AsNoTracking().Include(h => h.Alerta).ToList();

    public HistoricoAlerta? GetById(long id) =>
        context.HistoricosAlerta.AsNoTracking()
            .Include(h => h.Alerta)
            .FirstOrDefault(h => h.Id == id);

    public IReadOnlyList<HistoricoAlerta> GetByAlerta(long idAlerta) =>
        context.HistoricosAlerta.AsNoTracking()
            .Include(h => h.Alerta)
            .Where(h => EF.Property<long>(h, "ID_ALERTA") == idAlerta)
            .ToList();

    public void Add(HistoricoAlerta historico)
    {
        context.Entry(historico).State = EntityState.Added;
        context.Entry(historico).Property("ID_ALERTA").CurrentValue = historico.Alerta.Id;
        context.SaveChanges();
    }

    public bool Delete(long id)
    {
        // ✅ FIX: Usar FirstOrDefault ao invés de Any
        var exists = context.HistoricosAlerta.AsNoTracking()
            .FirstOrDefault(h => h.Id == id) != null;
        if (!exists) return false;

        context.Database.ExecuteSqlRaw("DELETE FROM TB_HISTORICO_ALERTA WHERE ID_HISTORICO = {0}", id);
        return true;
    }

    public void SaveChanges() => context.SaveChanges();
}