using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Application.DTO.Responses;

public record HistoricoAlertaResponse(
    long Id,
    string? StStatusAnt,
    string StStatusNovo,
    int? NrIndiceRisco,
    string? DsObservacao,
    string? NmUsuarioMod,
    DateTime DtAlteracao,
    long IdAlerta)
{
    public static HistoricoAlertaResponse ToDTO(HistoricoAlerta h) => new(
        h.Id, h.StStatusAnt, h.StStatusNovo, h.NrIndiceRisco,
        h.DsObservacao, h.NmUsuarioMod, h.DtAlteracao, h.Alerta.Id);
}
