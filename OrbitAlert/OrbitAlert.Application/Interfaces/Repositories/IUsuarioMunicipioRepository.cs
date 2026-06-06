using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Application.Interfaces.Repositories;

public interface IUsuarioMunicipioRepository
{
    IReadOnlyList<UsuarioMunicipio> GetAll();
    IReadOnlyList<UsuarioMunicipio> GetByUsuario(long idUsuario);
    IReadOnlyList<UsuarioMunicipio> GetByMunicipio(long idMunicipio);
    void Add(UsuarioMunicipio vinculo);
    bool Delete(long idUsuario, long idMunicipio);
    void SaveChanges();
}
