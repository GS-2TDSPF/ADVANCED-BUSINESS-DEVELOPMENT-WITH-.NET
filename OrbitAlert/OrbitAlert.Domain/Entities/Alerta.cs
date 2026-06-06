using System.Text.Json.Serialization;
using OrbitAlert.Domain.Enum;

namespace OrbitAlert.Domain.Entities;

public class Alerta
{
    public long Id { get; private set; }
    public int NrNivelRisco { get; private set; }
    public StatusAlertaEnum StStatus { get; private set; }
    public string? DsObservacao { get; private set; }
    public DateTime DtCriacao { get; private set; }
    public DateTime? DtFechamento { get; private set; }
    public virtual ZonaRisco ZonaRisco { get; private set; } = null!;
    public virtual TipoAlerta TipoAlerta { get; private set; } = null!;

    [JsonIgnore]
    public virtual AnaliseIa? AnaliseIa { get; private set; }

    [JsonIgnore]
    public virtual List<HistoricoAlerta> Historicos { get; private set; } = [];

    [JsonIgnore]
    public virtual List<Notificacao> Notificacoes { get; private set; } = [];

    public Alerta(int nrNivelRisco, StatusAlertaEnum stStatus, string? dsObservacao, DateTime? dtFechamento, ZonaRisco zonaRisco, TipoAlerta tipoAlerta)
    {
        UpdateNrNivelRisco(nrNivelRisco);
        StStatus = stStatus;
        DsObservacao = dsObservacao;
        DtFechamento = dtFechamento;
        UpdateZonaRisco(zonaRisco);
        UpdateTipoAlerta(tipoAlerta);
    }

    public void Transferir(int nrNivelRisco, StatusAlertaEnum stStatus, string? dsObservacao, DateTime? dtFechamento, ZonaRisco zonaRisco, TipoAlerta tipoAlerta)
    {
        UpdateNrNivelRisco(nrNivelRisco);
        StStatus = stStatus;
        DsObservacao = dsObservacao;
        DtFechamento = dtFechamento;
        UpdateZonaRisco(zonaRisco);
        UpdateTipoAlerta(tipoAlerta);
    }

    public void UpdateNrNivelRisco(int nrNivelRisco)
    {
        if (nrNivelRisco < 1 || nrNivelRisco > 5) throw new ArgumentException("O nível de risco deve ser entre 1 e 5.");
        NrNivelRisco = nrNivelRisco;
    }

    public void UpdateZonaRisco(ZonaRisco zonaRisco)
    {
        if (zonaRisco is null) throw new ArgumentException("A zona de risco é obrigatória.");
        ZonaRisco = zonaRisco;
    }

    public void UpdateTipoAlerta(TipoAlerta tipoAlerta)
    {
        if (tipoAlerta is null) throw new ArgumentException("O tipo de alerta é obrigatório.");
        TipoAlerta = tipoAlerta;
    }

    public Alerta() { }
}
