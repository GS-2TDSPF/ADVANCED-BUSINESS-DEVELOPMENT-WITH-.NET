using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Application.Interfaces.Repositories;

public interface IEstacaoIotRepository
{
    IReadOnlyList<EstacaoIot> GetAll();
    EstacaoIot? GetById(long id);
    IReadOnlyList<EstacaoIot> GetByZona(long idZona);
    void Add(EstacaoIot estacao);
    void Update(EstacaoIot estacao);
    bool Delete(long id);
    void SaveChanges();
}
