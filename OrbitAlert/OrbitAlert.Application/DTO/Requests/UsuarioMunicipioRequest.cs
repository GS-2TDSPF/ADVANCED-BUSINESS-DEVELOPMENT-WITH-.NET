using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Application.DTO.Requests;

public record UsuarioMunicipioRequest(
    Usuario Usuario,
    Municipio Municipio)
{
    public UsuarioMunicipio ToEntity() => new(Usuario, Municipio);
}
