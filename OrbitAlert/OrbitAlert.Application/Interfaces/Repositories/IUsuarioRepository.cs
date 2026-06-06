using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Application.Interfaces.Repositories;

public interface IUsuarioRepository
{
    IReadOnlyList<Usuario> GetAll();
    Usuario? GetById(long id);
    Usuario? GetByEmail(string email);
    void Add(Usuario usuario);
    void Update(Usuario usuario);
    bool Delete(long id);
    void SaveChanges();
}
