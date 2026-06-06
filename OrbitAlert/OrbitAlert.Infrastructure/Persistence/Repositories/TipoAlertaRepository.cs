using OrbitAlert.Application.Interfaces.Repositories;
using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Infrastructure.Persistence.Repositories;

public class TipoAlertaRepository(OrbitAlertContext context) : ITipoAlertaRepository
{
    public IReadOnlyList<TipoAlerta> GetAll() => context.TiposAlerta.ToList();
    public TipoAlerta? GetById(long id) => context.TiposAlerta.FirstOrDefault(t => t.Id == id);

    public void Add(TipoAlerta tipo) { context.TiposAlerta.Add(tipo); context.SaveChanges(); }
    public void Update(TipoAlerta tipo) { context.TiposAlerta.Update(tipo); context.SaveChanges(); }

    public bool Delete(long id)
    {
        var tipo = context.TiposAlerta.FirstOrDefault(t => t.Id == id);
        if (tipo is null) return false;
        context.TiposAlerta.Remove(tipo);
        context.SaveChanges();
        return true;
    }

    public void SaveChanges() => context.SaveChanges();
}
