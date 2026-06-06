using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Application.Interfaces.Repositories;

public interface IAnaliseIaRepository
{
    IReadOnlyList<AnaliseIa> GetAll();
    AnaliseIa? GetById(long id);
    AnaliseIa? GetByAlerta(long idAlerta);
    void Add(AnaliseIa analise);
    void Update(AnaliseIa analise);
    bool Delete(long id);
    void SaveChanges();
}
