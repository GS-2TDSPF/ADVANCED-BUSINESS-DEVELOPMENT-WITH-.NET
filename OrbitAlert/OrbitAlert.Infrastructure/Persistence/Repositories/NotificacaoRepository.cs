using Microsoft.EntityFrameworkCore;
using OrbitAlert.Application.Interfaces.Repositories;
using OrbitAlert.Domain.Entities;
using OrbitAlert.Infrastructure.Persistence;

namespace OrbitAlert.Infrastructure.Persistence.Repositories;

public class NotificacaoRepository(OrbitAlertContext context) : INotificacaoRepository
{
    public IReadOnlyList<Notificacao> GetAll() =>
        context.Notificacoes.AsNoTracking()
            .Include(n => n.Alerta)
            .Include(n => n.Usuario)
            .ToList();

    public Notificacao? GetById(long id) =>
        context.Notificacoes.AsNoTracking()
            .Include(n => n.Alerta)
            .Include(n => n.Usuario)
            .FirstOrDefault(n => n.Id == id);

    public IReadOnlyList<Notificacao> GetByUsuario(long idUsuario) =>
        context.Notificacoes.AsNoTracking()
            .Include(n => n.Alerta)
            .Include(n => n.Usuario)
            .Where(n => EF.Property<long>(n, "ID_USUARIO") == idUsuario)
            .ToList();

    public void Add(Notificacao notificacao)
    {
        context.Entry(notificacao).State = EntityState.Added;
        context.Entry(notificacao).Property("ID_ALERTA").CurrentValue = notificacao.Alerta.Id;
        context.Entry(notificacao).Property("ID_USUARIO").CurrentValue = notificacao.Usuario.Id;
        context.SaveChanges();
    }

    public void Update(Notificacao notificacao)
    {
        context.Entry(notificacao).State = EntityState.Modified;
        context.Entry(notificacao).Property("ID_ALERTA").CurrentValue = notificacao.Alerta.Id;
        context.Entry(notificacao).Property("ID_USUARIO").CurrentValue = notificacao.Usuario.Id;
        context.SaveChanges();
    }

    public bool Delete(long id)
    {
        // ✅ FIX: Usar FirstOrDefault ao invés de Any
        var exists = context.Notificacoes.AsNoTracking()
            .FirstOrDefault(n => n.Id == id) != null;
        if (!exists) return false;

        context.Database.ExecuteSqlRaw("DELETE FROM TB_NOTIFICACAO WHERE ID_NOTIFICACAO = {0}", id);
        return true;
    }

    public void SaveChanges() => context.SaveChanges();
}