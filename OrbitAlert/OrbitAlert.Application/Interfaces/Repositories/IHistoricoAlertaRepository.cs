using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Application.Interfaces.Repositories;

public interface IHistoricoAlertaRepository
{
    IReadOnlyList<HistoricoAlerta> GetAll();
    HistoricoAlerta? GetById(long id);
    IReadOnlyList<HistoricoAlerta> GetByAlerta(long idAlerta);
    void Add(HistoricoAlerta historico);
    bool Delete(long id);
    void SaveChanges();
}
