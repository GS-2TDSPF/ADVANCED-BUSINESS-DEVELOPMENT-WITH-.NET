using OrbitAlert.Domain.Enum;

namespace OrbitAlert.Application.DTO.Requests;

public record AlertaRequest(
    int NrNivelRisco,
    StatusAlertaEnum StStatus,
    string? DsObservacao,
    DateTime? DtFechamento,
    long IdZona,
    long IdTipoAlerta);
