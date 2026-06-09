using System.Text.Json.Serialization;

namespace OrbitAlert.Domain.Entities;

public class EstacaoIot
{
    public long Id { get; private set; }
    public string NmEstacao { get; private set; } = null!;
    public string? DsLocalizacao { get; private set; }
    public string StAtivo { get; private set; } = "S";
    public DateTime DtInstalacao { get; private set; }
    public virtual ZonaRisco ZonaRisco { get; set; } = null!;

    [JsonIgnore]
    public virtual List<LeituraIot> Leituras { get; private set; } = [];

    public EstacaoIot(string nmEstacao, string? dsLocalizacao, string stAtivo, ZonaRisco zonaRisco)
    {
        UpdateNmEstacao(nmEstacao);
        DsLocalizacao = dsLocalizacao;
        StAtivo = stAtivo;
        UpdateZonaRisco(zonaRisco);
    }

    public void Transferir(string nmEstacao, string? dsLocalizacao, string stAtivo, ZonaRisco zonaRisco)
    {
        UpdateNmEstacao(nmEstacao);
        DsLocalizacao = dsLocalizacao;
        StAtivo = stAtivo;
        UpdateZonaRisco(zonaRisco);
    }

    public void UpdateNmEstacao(string nmEstacao)
    {
        if (string.IsNullOrWhiteSpace(nmEstacao)) throw new ArgumentException("O nome da estação é obrigatório.");
        NmEstacao = nmEstacao.Trim();
    }

    public void UpdateZonaRisco(ZonaRisco zonaRisco)
    {
        if (zonaRisco is null) throw new ArgumentException("A zona de risco é obrigatória.");
        ZonaRisco = zonaRisco;
    }

    public EstacaoIot() { }
}
