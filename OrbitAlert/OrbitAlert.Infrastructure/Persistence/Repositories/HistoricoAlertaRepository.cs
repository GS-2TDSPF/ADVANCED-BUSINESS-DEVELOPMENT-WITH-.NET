using OrbitAlert.Application.Interfaces.Repositories;
using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Infrastructure.Persistence.Repositories;

public class HistoricoAlertaRepository(OrbitAlertContext context) : IHistoricoAlertaRepository
{
    public IReadOnlyList<HistoricoAlerta> GetAll() => context.HistoricosAlerta.ToList();
    public HistoricoAlerta? GetById(long id) => context.HistoricosAlerta.FirstOrDefault(h => h.Id == id);
    public IReadOnlyList<HistoricoAlerta> GetByAlerta(long idAlerta) => context.HistoricosAlerta.Where(h => h.Alerta.Id == idAlerta).ToList();

    public void Add(HistoricoAlerta historico) { context.HistoricosAlerta.Add(historico); context.SaveChanges(); }

    public bool Delete(long id)
    {
        var historico = context.HistoricosAlerta.FirstOrDefault(h => h.Id == id);
        if (historico is null) return false;
        context.HistoricosAlerta.Remove(historico);
        context.SaveChanges();
        return true;
    }

    public void SaveChanges() => context.SaveChanges();
}
