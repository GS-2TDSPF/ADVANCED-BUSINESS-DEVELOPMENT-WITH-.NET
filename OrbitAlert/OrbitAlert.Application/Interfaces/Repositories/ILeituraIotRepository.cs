using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Application.Interfaces.Repositories;

public interface ILeituraIotRepository
{
    IReadOnlyList<LeituraIot> GetAll();
    LeituraIot? GetById(long id);
    IReadOnlyList<LeituraIot> GetByEstacao(long idEstacao);
    void Add(LeituraIot leitura);
    bool Delete(long id);
    void SaveChanges();
}
