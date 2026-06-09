namespace OrbitAlert.Application.DTO.Requests;

public record ZonaRiscoRequest(
    string NmZona,
    string? DsDescricao,
    double NrLatitude,
    double NrLongitude,
    int NrLimiarAlerta,
    string StAtivo,
    long IdMunicipio);
