using OrbitAlert.Domain.Entities;
using OrbitAlert.Domain.Enum;

namespace OrbitAlert.Application.DTO.Requests;

public record NotificacaoRequest(
    TipoNotificacaoEnum TpNotificacao,
    string DsTitulo,
    string? DsMensagem,
    string StLida,
    Usuario Usuario,
    Alerta Alerta)
{
    public Notificacao ToEntity() => new(TpNotificacao, DsTitulo, DsMensagem, StLida, Usuario, Alerta);
}
