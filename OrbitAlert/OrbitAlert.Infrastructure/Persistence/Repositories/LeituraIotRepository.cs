using Microsoft.EntityFrameworkCore;
using OrbitAlert.Application.Interfaces.Repositories;
using OrbitAlert.Domain.Entities;
using OrbitAlert.Infrastructure.Persistence;

namespace OrbitAlert.Infrastructure.Persistence.Repositories;

public class LeituraIotRepository(OrbitAlertContext context) : ILeituraIotRepository
{
    public IReadOnlyList<LeituraIot> GetAll() =>
        context.LeiturasIot.AsNoTracking().Include(l => l.EstacaoIot).ToList();

    public LeituraIot? GetById(long id) =>
        context.LeiturasIot.AsNoTracking()
            .Include(l => l.EstacaoIot)
            .FirstOrDefault(l => l.Id == id);

    public IReadOnlyList<LeituraIot> GetByEstacao(long idEstacao) =>
        context.LeiturasIot.AsNoTracking()
            .Include(l => l.EstacaoIot)
            .Where(l => EF.Property<long>(l, "ID_ESTACAO") == idEstacao)
            .ToList();

    public void Add(LeituraIot leitura)
    {
        context.Entry(leitura).State = EntityState.Added;
        context.Entry(leitura).Property("ID_ESTACAO").CurrentValue = leitura.EstacaoIot.Id;
        context.SaveChanges();
    }

    public bool Delete(long id)
    {
        // ✅ FIX: Usar FirstOrDefault ao invés de Any
        var exists = context.LeiturasIot.AsNoTracking()
            .FirstOrDefault(l => l.Id == id) != null;
        if (!exists) return false;

        context.Database.ExecuteSqlRaw("DELETE FROM TB_LEITURA_IOT WHERE ID_LEITURA = {0}", id);
        return true;
    }

    public void SaveChanges() => context.SaveChanges();
}