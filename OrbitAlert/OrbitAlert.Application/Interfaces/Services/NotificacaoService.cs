using OrbitAlert.Application.DTO.Requests;
using OrbitAlert.Application.DTO.Responses;
using OrbitAlert.Application.Interfaces.Repositories;

namespace OrbitAlert.Application.Interfaces.Services;

public interface INotificacaoService
{
    NotificacaoResponse Create(NotificacaoRequest request);
    NotificacaoResponse? GetById(long id);
    IReadOnlyList<NotificacaoResponse> GetAll();
    IReadOnlyList<NotificacaoResponse> GetByUsuario(long idUsuario);
    NotificacaoResponse Update(long id, NotificacaoRequest request);
    bool Delete(long id);
}

public class NotificacaoService(INotificacaoRepository repository) : INotificacaoService
{
    public NotificacaoResponse Create(NotificacaoRequest request)
    {
        var notificacao = request.ToEntity();
        repository.Add(notificacao);
        return NotificacaoResponse.ToDTO(notificacao);
    }

    public NotificacaoResponse? GetById(long id)
    {
        var notificacao = repository.GetById(id);
        return notificacao is null ? null : NotificacaoResponse.ToDTO(notificacao);
    }

    public IReadOnlyList<NotificacaoResponse> GetAll() =>
        repository.GetAll().Select(NotificacaoResponse.ToDTO).ToList();

    public IReadOnlyList<NotificacaoResponse> GetByUsuario(long idUsuario) =>
        repository.GetByUsuario(idUsuario).Select(NotificacaoResponse.ToDTO).ToList();

    public NotificacaoResponse Update(long id, NotificacaoRequest request)
    {
        var notificacao = repository.GetById(id)
            ?? throw new KeyNotFoundException("Notificação não encontrada.");
        notificacao.Transferir(request.TpNotificacao, request.DsTitulo, request.DsMensagem, request.StLida, request.Usuario, request.Alerta);
        repository.SaveChanges();
        return NotificacaoResponse.ToDTO(notificacao);
    }

    public bool Delete(long id) => repository.Delete(id);
}
