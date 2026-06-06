using System.Text.Json.Serialization;
using OrbitAlert.Domain.Enum;

namespace OrbitAlert.Domain.Entities;

public class Usuario
{
    public long Id { get; private set; }
    public string NmUsuario { get; private set; } = null!;
    public string DsEmail { get; private set; } = null!;
    public string DsSenhaHash { get; private set; } = null!;
    public TipoPerfilEnum TpPerfil { get; private set; }
    public string StAtivo { get; private set; } = "S";
    public DateTime DtCadastro { get; private set; }

    [JsonIgnore]
    public virtual List<UsuarioMunicipio> Municipios { get; private set; } = [];

    [JsonIgnore]
    public virtual List<Notificacao> Notificacoes { get; private set; } = [];

    public Usuario(string nmUsuario, string dsEmail, string dsSenhaHash, TipoPerfilEnum tpPerfil, string stAtivo)
    {
        UpdateNmUsuario(nmUsuario);
        UpdateDsEmail(dsEmail);
        UpdateDsSenhaHash(dsSenhaHash);
        TpPerfil = tpPerfil;
        StAtivo = stAtivo;
    }

    public void Transferir(string nmUsuario, string dsEmail, string dsSenhaHash, TipoPerfilEnum tpPerfil, string stAtivo)
    {
        UpdateNmUsuario(nmUsuario);
        UpdateDsEmail(dsEmail);
        UpdateDsSenhaHash(dsSenhaHash);
        TpPerfil = tpPerfil;
        StAtivo = stAtivo;
    }

    public void UpdateNmUsuario(string nmUsuario)
    {
        if (string.IsNullOrWhiteSpace(nmUsuario)) throw new ArgumentException("O nome é obrigatório.");
        NmUsuario = nmUsuario.Trim();
    }

    public void UpdateDsEmail(string dsEmail)
    {
        if (string.IsNullOrWhiteSpace(dsEmail)) throw new ArgumentException("O e-mail é obrigatório.");
        DsEmail = dsEmail.Trim();
    }

    public void UpdateDsSenhaHash(string dsSenhaHash)
    {
        if (string.IsNullOrWhiteSpace(dsSenhaHash)) throw new ArgumentException("A senha é obrigatória.");
        DsSenhaHash = dsSenhaHash;
    }

    public Usuario() { }
}
