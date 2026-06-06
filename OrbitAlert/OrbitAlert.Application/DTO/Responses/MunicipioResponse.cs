using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Application.DTO.Responses;

public record MunicipioResponse(
    long Id,
    string NmMunicipio,
    string NmEstado,
    double NrLatitude,
    double NrLongitude,
    long? NrPopulacao,
    string StAtivo,
    DateTime DtCadastro)
{
    public static MunicipioResponse ToDTO(Municipio m) => new(m.Id, m.NmMunicipio, m.NmEstado, m.NrLatitude, m.NrLongitude, m.NrPopulacao, m.StAtivo, m.DtCadastro);
}
