using OrbitAlert.Domain.Entities;
using OrbitAlert.Domain.Enum;

namespace OrbitAlert.Application.DTO.Requests;

public record AlertaRequest(
    int NrNivelRisco,
    StatusAlertaEnum StStatus,
    string? DsObservacao,
    DateTime? DtFechamento,
    ZonaRisco ZonaRisco,
    TipoAlerta TipoAlerta)
{
    public Alerta ToEntity() => new(NrNivelRisco, StStatus, DsObservacao, DtFechamento, ZonaRisco, TipoAlerta);
}
