using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Application.DTO.Requests;

public record MunicipioRequest(
    string NmMunicipio,
    string NmEstado,
    double NrLatitude,
    double NrLongitude,
    long? NrPopulacao,
    string StAtivo)
{
    public Municipio ToEntity() => new(NmMunicipio, NmEstado, NrLatitude, NrLongitude, NrPopulacao, StAtivo);
}
