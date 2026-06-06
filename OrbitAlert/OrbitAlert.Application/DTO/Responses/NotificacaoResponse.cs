using OrbitAlert.Domain.Entities;
using OrbitAlert.Domain.Enum;

namespace OrbitAlert.Application.DTO.Responses;

public record NotificacaoResponse(
    long Id,
    TipoNotificacaoEnum TpNotificacao,
    string DsTitulo,
    string? DsMensagem,
    string StLida,
    DateTime DtEnvio,
    long IdUsuario,
    long IdAlerta)
{
    public static NotificacaoResponse ToDTO(Notificacao n) => new(
        n.Id, n.TpNotificacao, n.DsTitulo, n.DsMensagem, n.StLida, n.DtEnvio,
        n.Usuario.Id, n.Alerta.Id);
}
