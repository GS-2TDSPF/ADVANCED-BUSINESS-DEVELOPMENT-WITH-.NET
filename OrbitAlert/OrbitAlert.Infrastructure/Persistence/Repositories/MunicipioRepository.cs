using OrbitAlert.Application.Interfaces.Repositories;
using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Infrastructure.Persistence.Repositories;

public class MunicipioRepository(OrbitAlertContext context) : IMunicipioRepository
{
    public IReadOnlyList<Municipio> GetAll() => context.Municipios.ToList();
    public Municipio? GetById(long id) => context.Municipios.FirstOrDefault(m => m.Id == id);
    public IReadOnlyList<Municipio> GetByEstado(string nmEstado) => context.Municipios.Where(m => m.NmEstado == nmEstado).ToList();

    public void Add(Municipio municipio) { context.Municipios.Add(municipio); context.SaveChanges(); }
    public void Update(Municipio municipio) { context.Municipios.Update(municipio); context.SaveChanges(); }

    public bool Delete(long id)
    {
        var municipio = context.Municipios.FirstOrDefault(m => m.Id == id);
        if (municipio is null) return false;
        context.Municipios.Remove(municipio);
        context.SaveChanges();
        return true;
    }

    public void SaveChanges() => context.SaveChanges();
}
