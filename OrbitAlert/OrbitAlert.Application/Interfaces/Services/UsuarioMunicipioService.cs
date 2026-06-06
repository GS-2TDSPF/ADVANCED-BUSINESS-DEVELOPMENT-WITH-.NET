using OrbitAlert.Application.DTO.Requests;
using OrbitAlert.Application.DTO.Responses;
using OrbitAlert.Application.Interfaces.Repositories;

namespace OrbitAlert.Application.Interfaces.Services;

public interface IUsuarioMunicipioService
{
    UsuarioMunicipioResponse Create(UsuarioMunicipioRequest request);
    IReadOnlyList<UsuarioMunicipioResponse> GetAll();
    IReadOnlyList<UsuarioMunicipioResponse> GetByUsuario(long idUsuario);
    IReadOnlyList<UsuarioMunicipioResponse> GetByMunicipio(long idMunicipio);
    bool Delete(long idUsuario, long idMunicipio);
}

public class UsuarioMunicipioService(IUsuarioMunicipioRepository repository) : IUsuarioMunicipioService
{
    public UsuarioMunicipioResponse Create(UsuarioMunicipioRequest request)
    {
        var vinculo = request.ToEntity();
        repository.Add(vinculo);
        return UsuarioMunicipioResponse.ToDTO(vinculo);
    }

    public IReadOnlyList<UsuarioMunicipioResponse> GetAll() =>
        repository.GetAll().Select(UsuarioMunicipioResponse.ToDTO).ToList();

    public IReadOnlyList<UsuarioMunicipioResponse> GetByUsuario(long idUsuario) =>
        repository.GetByUsuario(idUsuario).Select(UsuarioMunicipioResponse.ToDTO).ToList();

    public IReadOnlyList<UsuarioMunicipioResponse> GetByMunicipio(long idMunicipio) =>
        repository.GetByMunicipio(idMunicipio).Select(UsuarioMunicipioResponse.ToDTO).ToList();

    public bool Delete(long idUsuario, long idMunicipio) => repository.Delete(idUsuario, idMunicipio);
}
