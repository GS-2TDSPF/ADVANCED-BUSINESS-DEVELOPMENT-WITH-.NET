using OrbitAlert.Application.Interfaces.Repositories;
using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Infrastructure.Persistence.Repositories;

public class UsuarioRepository(OrbitAlertContext context) : IUsuarioRepository
{
    public IReadOnlyList<Usuario> GetAll() => context.Usuarios.ToList();
    public Usuario? GetById(long id) => context.Usuarios.FirstOrDefault(u => u.Id == id);
    public Usuario? GetByEmail(string email) => context.Usuarios.FirstOrDefault(u => u.DsEmail == email);

    public void Add(Usuario usuario) { context.Usuarios.Add(usuario); context.SaveChanges(); }
    public void Update(Usuario usuario) { context.Usuarios.Update(usuario); context.SaveChanges(); }

    public bool Delete(long id)
    {
        var usuario = context.Usuarios.FirstOrDefault(u => u.Id == id);
        if (usuario is null) return false;
        context.Usuarios.Remove(usuario);
        context.SaveChanges();
        return true;
    }

    public void SaveChanges() => context.SaveChanges();
}
