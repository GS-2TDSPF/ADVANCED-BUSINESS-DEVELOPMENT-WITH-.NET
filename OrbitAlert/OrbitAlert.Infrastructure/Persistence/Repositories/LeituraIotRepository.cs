using OrbitAlert.Application.Interfaces.Repositories;
using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Infrastructure.Persistence.Repositories;

public class LeituraIotRepository(OrbitAlertContext context) : ILeituraIotRepository
{
    public IReadOnlyList<LeituraIot> GetAll() => context.LeiturasIot.ToList();
    public LeituraIot? GetById(long id) => context.LeiturasIot.FirstOrDefault(l => l.Id == id);
    public IReadOnlyList<LeituraIot> GetByEstacao(long idEstacao) => context.LeiturasIot.Where(l => l.EstacaoIot.Id == idEstacao).ToList();

    public void Add(LeituraIot leitura) { context.LeiturasIot.Add(leitura); context.SaveChanges(); }

    public bool Delete(long id)
    {
        var leitura = context.LeiturasIot.FirstOrDefault(l => l.Id == id);
        if (leitura is null) return false;
        context.LeiturasIot.Remove(leitura);
        context.SaveChanges();
        return true;
    }

    public void SaveChanges() => context.SaveChanges();
}
