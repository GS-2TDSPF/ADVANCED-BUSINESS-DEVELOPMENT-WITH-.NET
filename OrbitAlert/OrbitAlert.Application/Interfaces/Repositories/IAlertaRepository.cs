using OrbitAlert.Domain.Entities;
using OrbitAlert.Domain.Enum;

namespace OrbitAlert.Application.Interfaces.Repositories;

public interface IAlertaRepository
{
    IReadOnlyList<Alerta> GetAll();
    Alerta? GetById(long id);
    IReadOnlyList<Alerta> GetByStatus(StatusAlertaEnum status);
    IReadOnlyList<Alerta> GetByMunicipio(long idMunicipio);
    void Add(Alerta alerta);
    void Update(Alerta alerta);
    bool Delete(long id);
    void SaveChanges();
}