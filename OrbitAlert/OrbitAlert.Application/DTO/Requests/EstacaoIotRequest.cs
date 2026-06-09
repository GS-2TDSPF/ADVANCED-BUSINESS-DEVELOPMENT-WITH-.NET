namespace OrbitAlert.Application.DTO.Requests;

public record EstacaoIotRequest(
    string NmEstacao,
    string? DsLocalizacao,
    string StAtivo,
    long IdZona);
