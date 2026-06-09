using Microsoft.EntityFrameworkCore;
using OrbitAlert.Application.Interfaces.Repositories;
using OrbitAlert.Domain.Entities;
using OrbitAlert.Infrastructure.Persistence;

namespace OrbitAlert.Infrastructure.Persistence.Repositories;

public class AnaliseIaRepository(OrbitAlertContext context) : IAnaliseIaRepository
{
    public IReadOnlyList<AnaliseIa> GetAll() =>
        context.AnalisesIa.AsNoTracking().Include(a => a.Alerta).ToList();

    public AnaliseIa? GetById(long id) =>
        context.AnalisesIa.AsNoTracking()
            .Include(a => a.Alerta)
            .FirstOrDefault(a => a.Id == id);

    public AnaliseIa? GetByAlerta(long idAlerta) =>
        context.AnalisesIa.AsNoTracking()
            .Include(a => a.Alerta)
            .FirstOrDefault(a => EF.Property<long>(a, "ID_ALERTA") == idAlerta);

    public void Add(AnaliseIa analise)
    {
        context.Entry(analise).State = EntityState.Added;
        context.Entry(analise).Property("ID_ALERTA").CurrentValue = analise.Alerta.Id;
        context.SaveChanges();
    }

    public void Update(AnaliseIa analise)
    {
        context.Entry(analise).State = EntityState.Modified;
        context.Entry(analise).Property("ID_ALERTA").CurrentValue = analise.Alerta.Id;
        context.SaveChanges();
    }

    public bool Delete(long id)
    {
        // ✅ FIX: Usar FirstOrDefault ao invés de Any
        var exists = context.AnalisesIa.AsNoTracking()
            .FirstOrDefault(a => a.Id == id) != null;
        if (!exists) return false;

        context.Database.ExecuteSqlRaw("DELETE FROM TB_ANALISE_IA WHERE ID_ANALISE = {0}", id);
        return true;
    }

    public void SaveChanges() => context.SaveChanges();
}