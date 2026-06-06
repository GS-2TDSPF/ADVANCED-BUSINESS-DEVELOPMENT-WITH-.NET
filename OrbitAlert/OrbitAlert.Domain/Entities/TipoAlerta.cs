using System.Text.Json.Serialization;

namespace OrbitAlert.Domain.Entities;

public class TipoAlerta
{
    public long Id { get; private set; }
    public string NmTipo { get; private set; } = null!;
    public string? DsDescricao { get; private set; }

    [JsonIgnore]
    public virtual List<Alerta> Alertas { get; private set; } = [];

    public TipoAlerta(string nmTipo, string? dsDescricao)
    {
        UpdateNmTipo(nmTipo);
        DsDescricao = dsDescricao;
    }

    public void Transferir(string nmTipo, string? dsDescricao)
    {
        UpdateNmTipo(nmTipo);
        DsDescricao = dsDescricao;
    }

    public void UpdateNmTipo(string nmTipo)
    {
        if (string.IsNullOrWhiteSpace(nmTipo)) throw new ArgumentException("O nome do tipo é obrigatório.");
        NmTipo = nmTipo.Trim().ToUpper();
    }

    public TipoAlerta() { }
}
