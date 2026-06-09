using Microsoft.EntityFrameworkCore;
using OrbitAlert.Application.Interfaces.Repositories;
using OrbitAlert.Domain.Entities;
using OrbitAlert.Infrastructure.Persistence;

namespace OrbitAlert.Infrastructure.Persistence.Repositories;

public class UsuarioRepository(OrbitAlertContext context) : IUsuarioRepository
{
    public IReadOnlyList<Usuario> GetAll() =>
        context.Usuarios.AsNoTracking().ToList();

    public Usuario? GetById(long id) =>
        context.Usuarios.AsNoTracking().FirstOrDefault(u => u.Id == id);

    public Usuario? GetByEmail(string email) =>
        context.Usuarios.AsNoTracking()
            .FirstOrDefault(u => u.DsEmail == email);

    public void Add(Usuario usuario)
    {
        context.Entry(usuario).State = EntityState.Added;
        context.SaveChanges();
    }

    public void Update(Usuario usuario)
    {
        context.Entry(usuario).State = EntityState.Modified;
        context.SaveChanges();
    }

    public bool Delete(long id)
    {
        // ✅ FIX: Usar FirstOrDefault ao invés de Any
        var exists = context.Usuarios.AsNoTracking()
            .FirstOrDefault(u => u.Id == id) != null;
        if (!exists) return false;

        context.Database.ExecuteSqlRaw("DELETE FROM TB_USUARIO_MUNICIPIO WHERE ID_USUARIO = {0}", id);
        context.Database.ExecuteSqlRaw("DELETE FROM TB_NOTIFICACAO WHERE ID_USUARIO = {0}", id);
        context.Database.ExecuteSqlRaw("DELETE FROM TB_USUARIO WHERE ID_USUARIO = {0}", id);

        return true;
    }

    public void SaveChanges() => context.SaveChanges();
}