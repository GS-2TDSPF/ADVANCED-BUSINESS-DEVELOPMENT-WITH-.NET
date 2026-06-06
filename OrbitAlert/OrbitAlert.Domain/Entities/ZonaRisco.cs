using System.Text.Json.Serialization;

namespace OrbitAlert.Domain.Entities;

public class ZonaRisco
{
    public long Id { get; private set; }
    public string NmZona { get; private set; } = null!;
    public string? DsDescricao { get; private set; }
    public double NrLatitude { get; private set; }
    public double NrLongitude { get; private set; }
    public int NrLimiarAlerta { get; private set; } = 3;
    public string StAtivo { get; private set; } = "S";
    public DateTime DtCadastro { get; private set; }
    public virtual Municipio Municipio { get; private set; } = null!;

    [JsonIgnore]
    public virtual List<EstacaoIot> Estacoes { get; private set; } = [];

    [JsonIgnore]
    public virtual List<Alerta> Alertas { get; private set; } = [];

    public ZonaRisco(string nmZona, string? dsDescricao, double nrLatitude, double nrLongitude, int nrLimiarAlerta, string stAtivo, Municipio municipio)
    {
        UpdateNmZona(nmZona);
        DsDescricao = dsDescricao;
        NrLatitude = nrLatitude;
        NrLongitude = nrLongitude;
        UpdateNrLimiarAlerta(nrLimiarAlerta);
        StAtivo = stAtivo;
        UpdateMunicipio(municipio);
    }

    public void Transferir(string nmZona, string? dsDescricao, double nrLatitude, double nrLongitude, int nrLimiarAlerta, string stAtivo, Municipio municipio)
    {
        UpdateNmZona(nmZona);
        DsDescricao = dsDescricao;
        NrLatitude = nrLatitude;
        NrLongitude = nrLongitude;
        UpdateNrLimiarAlerta(nrLimiarAlerta);
        StAtivo = stAtivo;
        UpdateMunicipio(municipio);
    }

    public void UpdateNmZona(string nmZona)
    {
        if (string.IsNullOrWhiteSpace(nmZona)) throw new ArgumentException("O nome da zona é obrigatório.");
        NmZona = nmZona.Trim();
    }

    public void UpdateNrLimiarAlerta(int nrLimiarAlerta)
    {
        if (nrLimiarAlerta < 1 || nrLimiarAlerta > 5) throw new ArgumentException("O limiar deve ser entre 1 e 5.");
        NrLimiarAlerta = nrLimiarAlerta;
    }

    public void UpdateMunicipio(Municipio municipio)
    {
        if (municipio is null) throw new ArgumentException("O município é obrigatório.");
        Municipio = municipio;
    }

    public ZonaRisco() { }
}
