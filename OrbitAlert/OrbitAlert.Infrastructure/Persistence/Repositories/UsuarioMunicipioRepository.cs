using OrbitAlert.Application.Interfaces.Repositories;
using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Infrastructure.Persistence.Repositories;

public class UsuarioMunicipioRepository(OrbitAlertContext context) : IUsuarioMunicipioRepository
{
    public IReadOnlyList<UsuarioMunicipio> GetAll() => context.UsuariosMunicipios.ToList();

    public IReadOnlyList<UsuarioMunicipio> GetByUsuario(long idUsuario) =>
        context.UsuariosMunicipios.Where(um => um.IdUsuario == idUsuario).ToList();

    public IReadOnlyList<UsuarioMunicipio> GetByMunicipio(long idMunicipio) =>
        context.UsuariosMunicipios.Where(um => um.IdMunicipio == idMunicipio).ToList();

    public void Add(UsuarioMunicipio vinculo) { context.UsuariosMunicipios.Add(vinculo); context.SaveChanges(); }

    public bool Delete(long idUsuario, long idMunicipio)
    {
        var vinculo = context.UsuariosMunicipios.FirstOrDefault(um => um.IdUsuario == idUsuario && um.IdMunicipio == idMunicipio);
        if (vinculo is null) return false;
        context.UsuariosMunicipios.Remove(vinculo);
        context.SaveChanges();
        return true;
    }

    public void SaveChanges() => context.SaveChanges();
}
