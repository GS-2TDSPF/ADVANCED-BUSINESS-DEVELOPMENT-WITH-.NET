using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Application.DTO.Requests;

public record HistoricoAlertaRequest(
    string? StStatusAnt,
    string StStatusNovo,
    int? NrIndiceRisco,
    string? DsObservacao,
    string? NmUsuarioMod,
    Alerta Alerta)
{
    public HistoricoAlerta ToEntity() => new(StStatusAnt, StStatusNovo, NrIndiceRisco, DsObservacao, NmUsuarioMod, Alerta);
}
