using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Application.Interfaces.Repositories;

public interface INotificacaoRepository
{
    IReadOnlyList<Notificacao> GetAll();
    Notificacao? GetById(long id);
    IReadOnlyList<Notificacao> GetByUsuario(long idUsuario);
    void Add(Notificacao notificacao);
    void Update(Notificacao notificacao);
    bool Delete(long id);
    void SaveChanges();
}
