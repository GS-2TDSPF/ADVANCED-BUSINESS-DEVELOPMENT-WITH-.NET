namespace OrbitAlert.Application.DTO.Requests;

public record HistoricoAlertaRequest(
    string? StStatusAnt,
    string StStatusNovo,
    int? NrIndiceRisco,
    string? DsObservacao,
    string? NmUsuarioMod,
    long IdAlerta);
