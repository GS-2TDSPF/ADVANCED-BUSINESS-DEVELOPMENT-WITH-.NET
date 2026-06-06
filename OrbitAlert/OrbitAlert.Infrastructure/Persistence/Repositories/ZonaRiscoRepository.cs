using OrbitAlert.Application.Interfaces.Repositories;
using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Infrastructure.Persistence.Repositories;

public class ZonaRiscoRepository(OrbitAlertContext context) : IZonaRiscoRepository
{
    public IReadOnlyList<ZonaRisco> GetAll() => context.ZonasRisco.ToList();
    public ZonaRisco? GetById(long id) => context.ZonasRisco.FirstOrDefault(z => z.Id == id);
    public IReadOnlyList<ZonaRisco> GetByMunicipio(long idMunicipio) => context.ZonasRisco.Where(z => z.Municipio.Id == idMunicipio).ToList();

    public void Add(ZonaRisco zona) { context.ZonasRisco.Add(zona); context.SaveChanges(); }
    public void Update(ZonaRisco zona) { context.ZonasRisco.Update(zona); context.SaveChanges(); }

    public bool Delete(long id)
    {
        var zona = context.ZonasRisco.FirstOrDefault(z => z.Id == id);
        if (zona is null) return false;
        context.ZonasRisco.Remove(zona);
        context.SaveChanges();
        return true;
    }

    public void SaveChanges() => context.SaveChanges();
}
