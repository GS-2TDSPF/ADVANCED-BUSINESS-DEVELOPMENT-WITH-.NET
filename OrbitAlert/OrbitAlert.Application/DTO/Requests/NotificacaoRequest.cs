using OrbitAlert.Domain.Enum;

namespace OrbitAlert.Application.DTO.Requests;

public record NotificacaoRequest(
    TipoNotificacaoEnum TpNotificacao,
    string DsTitulo,
    string? DsMensagem,
    string StLida,
    long IdUsuario,
    long IdAlerta);
