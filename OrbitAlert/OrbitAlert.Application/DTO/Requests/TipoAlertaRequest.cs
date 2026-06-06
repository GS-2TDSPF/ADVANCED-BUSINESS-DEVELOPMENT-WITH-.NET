using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Application.DTO.Requests;

public record TipoAlertaRequest(
    string NmTipo,
    string? DsDescricao)
{
    public TipoAlerta ToEntity() => new(NmTipo, DsDescricao);
}
