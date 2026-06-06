using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Application.DTO.Responses;

public record ZonaRiscoResponse(
    long Id,
    string NmZona,
    string? DsDescricao,
    double NrLatitude,
    double NrLongitude,
    int NrLimiarAlerta,
    string StAtivo,
    DateTime DtCadastro,
    MunicipioResponse Municipio)
{
    public static ZonaRiscoResponse ToDTO(ZonaRisco z) => new(
        z.Id, z.NmZona, z.DsDescricao, z.NrLatitude, z.NrLongitude,
        z.NrLimiarAlerta, z.StAtivo, z.DtCadastro,
        MunicipioResponse.ToDTO(z.Municipio));
}
