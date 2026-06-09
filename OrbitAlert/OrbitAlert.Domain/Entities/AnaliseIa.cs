namespace OrbitAlert.Domain.Entities;

public class AnaliseIa
{
    public long Id { get; private set; }
    public string? DsPrompt { get; private set; }
    public string DsResposta { get; private set; } = null!;
    public string DsModeloIa { get; private set; } = "claude-sonnet-4-20250514";
    public long? NrTokensUsados { get; private set; }
    public DateTime DtGeracao { get; private set; }
    public virtual Alerta Alerta { get; set; } = null!;

    public AnaliseIa(string? dsPrompt, string dsResposta, string dsModeloIa, long? nrTokensUsados, Alerta alerta)
    {
        DsPrompt = dsPrompt;
        UpdateDsResposta(dsResposta);
        DsModeloIa = dsModeloIa;
        NrTokensUsados = nrTokensUsados;
        UpdateAlerta(alerta);
    }

    public void Transferir(string? dsPrompt, string dsResposta, string dsModeloIa, long? nrTokensUsados, Alerta alerta)
    {
        DsPrompt = dsPrompt;
        UpdateDsResposta(dsResposta);
        DsModeloIa = dsModeloIa;
        NrTokensUsados = nrTokensUsados;
        UpdateAlerta(alerta);
    }

    public void UpdateDsResposta(string dsResposta)
    {
        if (string.IsNullOrWhiteSpace(dsResposta)) throw new ArgumentException("A resposta da IA é obrigatória.");
        DsResposta = dsResposta;
    }

    public void UpdateAlerta(Alerta alerta)
    {
        if (alerta is null) throw new ArgumentException("O alerta é obrigatório.");
        Alerta = alerta;
    }

    public AnaliseIa() { }
}
