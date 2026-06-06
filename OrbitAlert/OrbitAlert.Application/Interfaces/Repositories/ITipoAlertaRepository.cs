using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Application.Interfaces.Repositories;

public interface ITipoAlertaRepository
{
    IReadOnlyList<TipoAlerta> GetAll();
    TipoAlerta? GetById(long id);
    void Add(TipoAlerta tipo);
    void Update(TipoAlerta tipo);
    bool Delete(long id);
    void SaveChanges();
}
