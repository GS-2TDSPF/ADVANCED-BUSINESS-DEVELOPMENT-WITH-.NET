using OrbitAlert.Application.Interfaces.Repositories;
using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Infrastructure.Persistence.Repositories;

public class NotificacaoRepository(OrbitAlertContext context) : INotificacaoRepository
{
    public IReadOnlyList<Notificacao> GetAll() => context.Notificacoes.ToList();
    public Notificacao? GetById(long id) => context.Notificacoes.FirstOrDefault(n => n.Id == id);
    public IReadOnlyList<Notificacao> GetByUsuario(long idUsuario) => context.Notificacoes.Where(n => n.Usuario.Id == idUsuario).ToList();

    public void Add(Notificacao notificacao) { context.Notificacoes.Add(notificacao); context.SaveChanges(); }
    public void Update(Notificacao notificacao) { context.Notificacoes.Update(notificacao); context.SaveChanges(); }

    public bool Delete(long id)
    {
        var notificacao = context.Notificacoes.FirstOrDefault(n => n.Id == id);
        if (notificacao is null) return false;
        context.Notificacoes.Remove(notificacao);
        context.SaveChanges();
        return true;
    }

    public void SaveChanges() => context.SaveChanges();
}
