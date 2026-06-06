using OrbitAlert.Domain.Entities;
using OrbitAlert.Domain.Enum;

namespace OrbitAlert.Application.DTO.Responses;

public record UsuarioResponse(
    long Id,
    string NmUsuario,
    string DsEmail,
    TipoPerfilEnum TpPerfil,
    string StAtivo,
    DateTime DtCadastro)
{
    public static UsuarioResponse ToDTO(Usuario u) => new(u.Id, u.NmUsuario, u.DsEmail, u.TpPerfil, u.StAtivo, u.DtCadastro);
}
