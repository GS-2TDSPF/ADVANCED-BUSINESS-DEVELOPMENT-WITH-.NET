using OrbitAlert.Application.Interfaces.Repositories;
using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Infrastructure.Persistence.Repositories;

public class EstacaoIotRepository(OrbitAlertContext context) : IEstacaoIotRepository
{
    public IReadOnlyList<EstacaoIot> GetAll() => context.EstacoesIot.ToList();
    public EstacaoIot? GetById(long id) => context.EstacoesIot.FirstOrDefault(e => e.Id == id);
    public IReadOnlyList<EstacaoIot> GetByZona(long idZona) => context.EstacoesIot.Where(e => e.ZonaRisco.Id == idZona).ToList();

    public void Add(EstacaoIot estacao) { context.EstacoesIot.Add(estacao); context.SaveChanges(); }
    public void Update(EstacaoIot estacao) { context.EstacoesIot.Update(estacao); context.SaveChanges(); }

    public bool Delete(long id)
    {
        var estacao = context.EstacoesIot.FirstOrDefault(e => e.Id == id);
        if (estacao is null) return false;
        context.EstacoesIot.Remove(estacao);
        context.SaveChanges();
        return true;
    }

    public void SaveChanges() => context.SaveChanges();
}
