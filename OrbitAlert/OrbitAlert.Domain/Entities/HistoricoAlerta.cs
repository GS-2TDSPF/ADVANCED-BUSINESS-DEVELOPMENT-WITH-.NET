namespace OrbitAlert.Domain.Entities;

public class HistoricoAlerta
{
    public long Id { get; private set; }
    public string? StStatusAnt { get; private set; }
    public string StStatusNovo { get; private set; } = null!;
    public int? NrIndiceRisco { get; private set; }
    public string? DsObservacao { get; private set; }
    public string? NmUsuarioMod { get; private set; }
    public DateTime DtAlteracao { get; private set; }
    public virtual Alerta Alerta { get; set; } = null!;

    public HistoricoAlerta(string? stStatusAnt, string stStatusNovo, int? nrIndiceRisco, string? dsObservacao, string? nmUsuarioMod, Alerta alerta)
    {
        StStatusAnt = stStatusAnt;
        UpdateStStatusNovo(stStatusNovo);
        NrIndiceRisco = nrIndiceRisco;
        DsObservacao = dsObservacao;
        NmUsuarioMod = nmUsuarioMod;
        UpdateAlerta(alerta);
    }

    public void UpdateStStatusNovo(string stStatusNovo)
    {
        if (string.IsNullOrWhiteSpace(stStatusNovo)) throw new ArgumentException("O novo status é obrigatório.");
        StStatusNovo = stStatusNovo;
    }

    public void UpdateAlerta(Alerta alerta)
    {
        if (alerta is null) throw new ArgumentException("O alerta é obrigatório.");
        Alerta = alerta;
    }

    public HistoricoAlerta() { }
}
