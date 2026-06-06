using OrbitAlert.Domain.Entities;
using OrbitAlert.Domain.Enum;

namespace OrbitAlert.Application.DTO.Requests;

public record UsuarioRequest(
    string NmUsuario,
    string DsEmail,
    string DsSenhaHash,
    TipoPerfilEnum TpPerfil,
    string StAtivo)
{
    public Usuario ToEntity() => new(NmUsuario, DsEmail, DsSenhaHash, TpPerfil, StAtivo);
}
