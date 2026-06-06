namespace OrbitAlert.Domain.Entities;

public class LeituraIot
{
    public long Id { get; private set; }
    public double NrTemperatura { get; private set; }
    public double NrUmidade { get; private set; }
    public double NrChuvaMm { get; private set; }
    public int NrIndiceRisco { get; private set; }
    public DateTime DtLeitura { get; private set; }
    public virtual EstacaoIot EstacaoIot { get; private set; } = null!;

    public LeituraIot(double nrTemperatura, double nrUmidade, double nrChuvaMm, int nrIndiceRisco, EstacaoIot estacaoIot)
    {
        NrTemperatura = nrTemperatura;
        UpdateNrUmidade(nrUmidade);
        NrChuvaMm = nrChuvaMm;
        UpdateNrIndiceRisco(nrIndiceRisco);
        UpdateEstacaoIot(estacaoIot);
    }

    public void Transferir(double nrTemperatura, double nrUmidade, double nrChuvaMm, int nrIndiceRisco, EstacaoIot estacaoIot)
    {
        NrTemperatura = nrTemperatura;
        UpdateNrUmidade(nrUmidade);
        NrChuvaMm = nrChuvaMm;
        UpdateNrIndiceRisco(nrIndiceRisco);
        UpdateEstacaoIot(estacaoIot);
    }

    public void UpdateNrUmidade(double nrUmidade)
    {
        if (nrUmidade < 0 || nrUmidade > 100) throw new ArgumentException("A umidade deve estar entre 0 e 100.");
        NrUmidade = nrUmidade;
    }

    public void UpdateNrIndiceRisco(int nrIndiceRisco)
    {
        if (nrIndiceRisco < 1 || nrIndiceRisco > 5) throw new ArgumentException("O índice de risco deve ser entre 1 e 5.");
        NrIndiceRisco = nrIndiceRisco;
    }

    public void UpdateEstacaoIot(EstacaoIot estacaoIot)
    {
        if (estacaoIot is null) throw new ArgumentException("A estação é obrigatória.");
        EstacaoIot = estacaoIot;
    }

    public LeituraIot() { }
}
