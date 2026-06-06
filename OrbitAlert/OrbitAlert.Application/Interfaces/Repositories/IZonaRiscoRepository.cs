using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Application.Interfaces.Repositories;

public interface IZonaRiscoRepository
{
    IReadOnlyList<ZonaRisco> GetAll();
    ZonaRisco? GetById(long id);
    IReadOnlyList<ZonaRisco> GetByMunicipio(long idMunicipio);
    void Add(ZonaRisco zona);
    void Update(ZonaRisco zona);
    bool Delete(long id);
    void SaveChanges();
}
