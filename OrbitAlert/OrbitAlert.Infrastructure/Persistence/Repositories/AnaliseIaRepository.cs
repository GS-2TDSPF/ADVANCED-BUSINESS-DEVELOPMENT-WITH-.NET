using OrbitAlert.Application.Interfaces.Repositories;
using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Infrastructure.Persistence.Repositories;

public class AnaliseIaRepository(OrbitAlertContext context) : IAnaliseIaRepository
{
    public IReadOnlyList<AnaliseIa> GetAll() => context.AnalisesIa.ToList();
    public AnaliseIa? GetById(long id) => context.AnalisesIa.FirstOrDefault(a => a.Id == id);
    public AnaliseIa? GetByAlerta(long idAlerta) => context.AnalisesIa.FirstOrDefault(a => a.Alerta.Id == idAlerta);

    public void Add(AnaliseIa analise) { context.AnalisesIa.Add(analise); context.SaveChanges(); }
    public void Update(AnaliseIa analise) { context.AnalisesIa.Update(analise); context.SaveChanges(); }

    public bool Delete(long id)
    {
        var analise = context.AnalisesIa.FirstOrDefault(a => a.Id == id);
        if (analise is null) return false;
        context.AnalisesIa.Remove(analise);
        context.SaveChanges();
        return true;
    }

    public void SaveChanges() => context.SaveChanges();
}
