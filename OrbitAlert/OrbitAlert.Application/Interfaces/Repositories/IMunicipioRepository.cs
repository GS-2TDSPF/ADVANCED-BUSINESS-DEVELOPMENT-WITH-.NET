using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Application.Interfaces.Repositories;

public interface IMunicipioRepository
{
    IReadOnlyList<Municipio> GetAll();
    Municipio? GetById(long id);
    IReadOnlyList<Municipio> GetByEstado(string nmEstado);
    void Add(Municipio municipio);
    void Update(Municipio municipio);
    bool Delete(long id);
    void SaveChanges();
}
