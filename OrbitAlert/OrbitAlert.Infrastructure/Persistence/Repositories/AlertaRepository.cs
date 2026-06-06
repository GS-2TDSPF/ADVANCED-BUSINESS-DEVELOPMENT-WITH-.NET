
using OrbitAlert.Application.Interfaces.Repositories;
using OrbitAlert.Domain.Entities;
using OrbitAlert.Domain.Enum;

namespace OrbitAlert.Infrastructure.Persistence.Repositories;

public class AlertaRepository(OrbitAlertContext context) : IAlertaRepository
{
    public IReadOnlyList<Alerta> GetAll() => context.Alertas.ToList();
    public Alerta? GetById(long id) => context.Alertas.FirstOrDefault(a => a.Id == id);
    public IReadOnlyList<Alerta> GetByStatus(StatusAlertaEnum status) => context.Alertas.Where(a => a.StStatus == status).ToList();
    public IReadOnlyList<Alerta> GetByMunicipio(long idMunicipio) => context.Alertas.Where(a => a.ZonaRisco.Municipio.Id == idMunicipio).ToList();

    public void Add(Alerta alerta) { context.Alertas.Add(alerta); context.SaveChanges(); }
    public void Update(Alerta alerta) { context.Alertas.Update(alerta); context.SaveChanges(); }

    public bool Delete(long id)
    {
        var alerta = context.Alertas.FirstOrDefault(a => a.Id == id);
        if (alerta is null) return false;
        context.Alertas.Remove(alerta);
        context.SaveChanges();
        return true;
    }

    public void SaveChanges() => context.SaveChanges();
}
