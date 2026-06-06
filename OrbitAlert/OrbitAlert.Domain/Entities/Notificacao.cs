using OrbitAlert.Domain.Enum;

namespace OrbitAlert.Domain.Entities;

public class Notificacao
{
    public long Id { get; private set; }
    public TipoNotificacaoEnum TpNotificacao { get; private set; }
    public string DsTitulo { get; private set; } = null!;
    public string? DsMensagem { get; private set; }
    public string StLida { get; private set; } = "N";
    public DateTime DtEnvio { get; private set; }
    public virtual Usuario Usuario { get; private set; } = null!;
    public virtual Alerta Alerta { get; private set; } = null!;

    public Notificacao(TipoNotificacaoEnum tpNotificacao, string dsTitulo, string? dsMensagem, string stLida, Usuario usuario, Alerta alerta)
    {
        TpNotificacao = tpNotificacao;
        UpdateDsTitulo(dsTitulo);
        DsMensagem = dsMensagem;
        StLida = stLida;
        UpdateUsuario(usuario);
        UpdateAlerta(alerta);
    }

    public void Transferir(TipoNotificacaoEnum tpNotificacao, string dsTitulo, string? dsMensagem, string stLida, Usuario usuario, Alerta alerta)
    {
        TpNotificacao = tpNotificacao;
        UpdateDsTitulo(dsTitulo);
        DsMensagem = dsMensagem;
        StLida = stLida;
        UpdateUsuario(usuario);
        UpdateAlerta(alerta);
    }

    public void UpdateDsTitulo(string dsTitulo)
    {
        if (string.IsNullOrWhiteSpace(dsTitulo)) throw new ArgumentException("O título é obrigatório.");
        DsTitulo = dsTitulo.Trim();
    }

    public void UpdateUsuario(Usuario usuario)
    {
        if (usuario is null) throw new ArgumentException("O usuário é obrigatório.");
        Usuario = usuario;
    }

    public void UpdateAlerta(Alerta alerta)
    {
        if (alerta is null) throw new ArgumentException("O alerta é obrigatório.");
        Alerta = alerta;
    }

    public Notificacao() { }
}
