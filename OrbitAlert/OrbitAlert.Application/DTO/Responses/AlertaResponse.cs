using OrbitAlert.Domain.Entities;
using OrbitAlert.Domain.Enum;

namespace OrbitAlert.Application.DTO.Responses;

public record AlertaResponse(
    long Id,
    int NrNivelRisco,
    StatusAlertaEnum StStatus,
    string? DsObservacao,
    DateTime DtCriacao,
    DateTime? DtFechamento,
    ZonaRiscoResponse ZonaRisco,
    TipoAlertaResponse TipoAlerta)
{
    public static AlertaResponse ToDTO(Alerta a) => new(
        a.Id, a.NrNivelRisco, a.StStatus, a.DsObservacao, a.DtCriacao, a.DtFechamento,
        ZonaRiscoResponse.ToDTO(a.ZonaRisco),
        TipoAlertaResponse.ToDTO(a.TipoAlerta));
}
