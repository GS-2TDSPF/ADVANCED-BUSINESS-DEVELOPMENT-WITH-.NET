using OrbitAlert.Application.DTO.Requests;
using OrbitAlert.Application.DTO.Responses;
using OrbitAlert.Application.Interfaces.Repositories;

namespace OrbitAlert.Application.Interfaces.Services;

public interface IUsuarioService
{
    UsuarioResponse Create(UsuarioRequest request);
    UsuarioResponse? GetById(long id);
    IReadOnlyList<UsuarioResponse> GetAll();
    UsuarioResponse Update(long id, UsuarioRequest request);
    bool Delete(long id);
}

public class UsuarioService(IUsuarioRepository usuarioRepository) : IUsuarioService
{
    public UsuarioResponse Create(UsuarioRequest request)
    {
        var usuario = request.ToEntity();
        usuarioRepository.Add(usuario);
        return UsuarioResponse.ToDTO(usuario);
    }

    public UsuarioResponse? GetById(long id)
    {
        var usuario = usuarioRepository.GetById(id);
        return usuario is null ? null : UsuarioResponse.ToDTO(usuario);
    }

    public IReadOnlyList<UsuarioResponse> GetAll() =>
        usuarioRepository.GetAll().Select(UsuarioResponse.ToDTO).ToList();

    public UsuarioResponse Update(long id, UsuarioRequest request)
    {
        var usuario = usuarioRepository.GetById(id)
                      ?? throw new KeyNotFoundException("Usuário não encontrado.");
        usuario.Transferir(request.NmUsuario, request.DsEmail, request.DsSenhaHash, request.TpPerfil, request.StAtivo);
        usuarioRepository.Update(usuario);
        return UsuarioResponse.ToDTO(usuario);
    }

    public bool Delete(long id) => usuarioRepository.Delete(id);
}