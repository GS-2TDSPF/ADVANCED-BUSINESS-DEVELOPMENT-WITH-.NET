using System.Text.Json.Serialization;
using OrbitAlert.Domain.Enum;

namespace OrbitAlert.Domain.Entities;

public class Alerta
{
    // ✅ EF Core precisa de setters públicos ou internos
    public long Id { get; set; }
    public int NrNivelRisco { get; set; }
    public StatusAlertaEnum StStatus { get; set; }
    public string? DsObservacao { get; set; }
    public DateTime DtCriacao { get; set; }
    public DateTime? DtFechamento { get; set; }
    
    // ✅ Principais relacionamentos
    public virtual ZonaRisco ZonaRisco { get; set; } = null!;
    public virtual TipoAlerta TipoAlerta { get; set; } = null!;

    // ✅ Relacionamentos opcionais (ignorados no JSON)
    [JsonIgnore]
    public virtual AnaliseIa? AnaliseIa { get; set; }

    [JsonIgnore]
    public virtual List<HistoricoAlerta> Historicos { get; set; } = [];

    [JsonIgnore]
    public virtual List<Notificacao> Notificacoes { get; set; } = [];

    // ✅ Construtor com validação (opcional)
    public Alerta(int nrNivelRisco, StatusAlertaEnum stStatus, string? dsObservacao, DateTime? dtFechamento, ZonaRisco zonaRisco, TipoAlerta tipoAlerta)
    {
        UpdateNrNivelRisco(nrNivelRisco);
        StStatus = stStatus;
        DsObservacao = dsObservacao;
        DtFechamento = dtFechamento;
        UpdateZonaRisco(zonaRisco);
        UpdateTipoAlerta(tipoAlerta);
        DtCriacao = DateTime.Now;
    }

    // ✅ Construtor vazio para EF Core
    public Alerta() { }

    // ✅ Método de atualização com validação
    public void Transferir(int nrNivelRisco, StatusAlertaEnum stStatus, string? dsObservacao, DateTime? dtFechamento, ZonaRisco zonaRisco, TipoAlerta tipoAlerta)
    {
        UpdateNrNivelRisco(nrNivelRisco);
        StStatus = stStatus;
        DsObservacao = dsObservacao;
        DtFechamento = dtFechamento;
        UpdateZonaRisco(zonaRisco);
        UpdateTipoAlerta(tipoAlerta);
    }

    // ✅ Validações de negócio
    public void UpdateNrNivelRisco(int nrNivelRisco)
    {
        if (nrNivelRisco < 1 || nrNivelRisco > 5) 
            throw new ArgumentException("O nível de risco deve ser entre 1 e 5.");
        NrNivelRisco = nrNivelRisco;
    }

    public void UpdateZonaRisco(ZonaRisco zonaRisco)
    {
        if (zonaRisco is null) 
            throw new ArgumentException("A zona de risco é obrigatória.");
        ZonaRisco = zonaRisco;
    }

    public void UpdateTipoAlerta(TipoAlerta tipoAlerta)
    {
        if (tipoAlerta is null) 
            throw new ArgumentException("O tipo de alerta é obrigatório.");
        TipoAlerta = tipoAlerta;
    }
}