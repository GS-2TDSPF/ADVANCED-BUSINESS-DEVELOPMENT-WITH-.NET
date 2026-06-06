using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Application.DTO.Responses;

public record UsuarioMunicipioResponse(
    UsuarioResponse Usuario,
    MunicipioResponse Municipio,
    DateTime DtVinculo)
{
    public static UsuarioMunicipioResponse ToDTO(UsuarioMunicipio um) => new(
        UsuarioResponse.ToDTO(um.Usuario),
        MunicipioResponse.ToDTO(um.Municipio),
        um.DtVinculo);
}
