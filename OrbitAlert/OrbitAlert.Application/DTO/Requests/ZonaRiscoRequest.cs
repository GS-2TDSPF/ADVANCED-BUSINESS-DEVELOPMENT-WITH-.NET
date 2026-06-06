using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Application.DTO.Requests;

public record ZonaRiscoRequest(
    string NmZona,
    string? DsDescricao,
    double NrLatitude,
    double NrLongitude,
    int NrLimiarAlerta,
    string StAtivo,
    Municipio Municipio)
{
    public ZonaRisco ToEntity() => new(NmZona, DsDescricao, NrLatitude, NrLongitude, NrLimiarAlerta, StAtivo, Municipio);
}
