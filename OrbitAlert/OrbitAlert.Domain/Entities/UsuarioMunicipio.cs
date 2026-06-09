namespace OrbitAlert.Domain.Entities;

public class UsuarioMunicipio
{
    public long IdUsuario { get; private set; }
    public long IdMunicipio { get; private set; }
    public DateTime DtVinculo { get; private set; }
    public virtual Usuario Usuario { get; set; } = null!;
    public virtual Municipio Municipio { get;  set; } = null!;

    public UsuarioMunicipio(Usuario usuario, Municipio municipio)
    {
        Usuario = usuario ?? throw new ArgumentException("O usuário é obrigatório.");
        Municipio = municipio ?? throw new ArgumentException("O município é obrigatório.");
    }

    public UsuarioMunicipio() { }
}
