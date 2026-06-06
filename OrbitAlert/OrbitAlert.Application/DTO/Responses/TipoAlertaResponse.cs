using OrbitAlert.Domain.Entities;

namespace OrbitAlert.Application.DTO.Responses;

public record TipoAlertaResponse(long Id, string NmTipo, string? DsDescricao)
{
    public static TipoAlertaResponse ToDTO(TipoAlerta t) => new(t.Id, t.NmTipo, t.DsDescricao);
}
