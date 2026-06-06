using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Application.DTO.Responses;

public record EstacaoIotResponse(
    long Id,
    string NmEstacao,
    string? DsLocalizacao,
    string StAtivo,
    DateTime DtInstalacao,
    ZonaRiscoResponse ZonaRisco)
{
    public static EstacaoIotResponse ToDTO(EstacaoIot e) => new(
        e.Id, e.NmEstacao, e.DsLocalizacao, e.StAtivo, e.DtInstalacao,
        ZonaRiscoResponse.ToDTO(e.ZonaRisco));
}
