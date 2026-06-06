using System.Text.Json.Serialization;

namespace OrbitAlert.Domain.Entities;

public class Municipio
{
    public long Id { get; private set; }
    public string NmMunicipio { get; private set; } = null!;
    public string NmEstado { get; private set; } = null!;
    public double NrLatitude { get; private set; }
    public double NrLongitude { get; private set; }
    public long? NrPopulacao { get; private set; }
    public string StAtivo { get; private set; } = "S";
    public DateTime DtCadastro { get; private set; }

    [JsonIgnore]
    public virtual List<UsuarioMunicipio> Usuarios { get; private set; } = [];

    [JsonIgnore]
    public virtual List<ZonaRisco> Zonas { get; private set; } = [];

    public Municipio(string nmMunicipio, string nmEstado, double nrLatitude, double nrLongitude, long? nrPopulacao, string stAtivo)
    {
        UpdateNmMunicipio(nmMunicipio);
        UpdateNmEstado(nmEstado);
        NrLatitude = nrLatitude;
        NrLongitude = nrLongitude;
        NrPopulacao = nrPopulacao;
        StAtivo = stAtivo;
    }

    public void Transferir(string nmMunicipio, string nmEstado, double nrLatitude, double nrLongitude, long? nrPopulacao, string stAtivo)
    {
        UpdateNmMunicipio(nmMunicipio);
        UpdateNmEstado(nmEstado);
        NrLatitude = nrLatitude;
        NrLongitude = nrLongitude;
        NrPopulacao = nrPopulacao;
        StAtivo = stAtivo;
    }

    public void UpdateNmMunicipio(string nmMunicipio)
    {
        if (string.IsNullOrWhiteSpace(nmMunicipio)) throw new ArgumentException("O nome do município é obrigatório.");
        NmMunicipio = nmMunicipio.Trim();
    }

    public void UpdateNmEstado(string nmEstado)
    {
        if (string.IsNullOrWhiteSpace(nmEstado)) throw new ArgumentException("O estado é obrigatório.");
        NmEstado = nmEstado.Trim();
    }

    public Municipio() { }
}
