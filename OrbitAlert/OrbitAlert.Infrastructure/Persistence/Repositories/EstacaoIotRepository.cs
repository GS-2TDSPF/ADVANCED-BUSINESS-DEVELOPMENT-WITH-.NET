using Microsoft.EntityFrameworkCore;
using OrbitAlert.Application.Interfaces.Repositories;
using OrbitAlert.Domain.Entities;
using OrbitAlert.Infrastructure.Persistence;

namespace OrbitAlert.Infrastructure.Persistence.Repositories;

public class EstacaoIotRepository(OrbitAlertContext context) : IEstacaoIotRepository
{
    public IReadOnlyList<EstacaoIot> GetAll() =>
        context.EstacoesIot.AsNoTracking().Include(e => e.ZonaRisco).ToList();

    public EstacaoIot? GetById(long id) =>
        context.EstacoesIot.AsNoTracking().Include(e => e.ZonaRisco).FirstOrDefault(e => e.Id == id);

    public IReadOnlyList<EstacaoIot> GetByZona(long idZona) =>
        context.EstacoesIot.AsNoTracking()
            .Include(e => e.ZonaRisco)
            .Where(e => EF.Property<long>(e, "ID_ZONA") == idZona)
            .ToList();

    public void Add(EstacaoIot estacao)
    {
        context.Entry(estacao).State = EntityState.Added;
        context.Entry(estacao).Property("ID_ZONA").CurrentValue = estacao.ZonaRisco.Id;
        context.SaveChanges();
    }

    public void Update(EstacaoIot estacao)
    {
        context.Entry(estacao).State = EntityState.Modified;
        context.Entry(estacao).Property("ID_ZONA").CurrentValue = estacao.ZonaRisco.Id;
        context.SaveChanges();
    }

    public bool Delete(long id)
    {
        // ✅ FIX: Usar FirstOrDefault ao invés de Any
        var exists = context.EstacoesIot.AsNoTracking()
            .FirstOrDefault(e => e.Id == id) != null;
        if (!exists) return false;

        context.Database.ExecuteSqlRaw("DELETE FROM TB_LEITURA_IOT WHERE ID_ESTACAO = {0}", id);
        context.Database.ExecuteSqlRaw("DELETE FROM TB_ESTACAO_IOT WHERE ID_ESTACAO = {0}", id);

        return true;
    }

    public void SaveChanges() => context.SaveChanges();
}