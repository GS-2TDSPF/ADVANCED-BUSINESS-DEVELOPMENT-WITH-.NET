using Microsoft.EntityFrameworkCore;
using OrbitAlert.Application.Interfaces.Repositories;
using OrbitAlert.Domain.Entities;
using OrbitAlert.Infrastructure.Persistence;

namespace OrbitAlert.Infrastructure.Persistence.Repositories;

public class UsuarioMunicipioRepository(OrbitAlertContext context) : IUsuarioMunicipioRepository
{
    public IReadOnlyList<UsuarioMunicipio> GetAll() =>
        context.UsuariosMunicipios.AsNoTracking()
            .Include(um => um.Usuario)
            .Include(um => um.Municipio)
            .ToList();

    public IReadOnlyList<UsuarioMunicipio> GetByUsuario(long idUsuario) =>
        context.UsuariosMunicipios.AsNoTracking()
            .Include(um => um.Usuario)
            .Include(um => um.Municipio)
            .Where(um => EF.Property<long>(um, "ID_USUARIO") == idUsuario)
            .ToList();

    public IReadOnlyList<UsuarioMunicipio> GetByMunicipio(long idMunicipio) =>
        context.UsuariosMunicipios.AsNoTracking()
            .Include(um => um.Usuario)
            .Include(um => um.Municipio)
            .Where(um => EF.Property<long>(um, "ID_MUNICIPIO") == idMunicipio)
            .ToList();

    public void Add(UsuarioMunicipio vinculo)
    {
        context.Entry(vinculo).State = EntityState.Added;
        context.Entry(vinculo).Property("ID_USUARIO").CurrentValue = vinculo.Usuario.Id;
        context.Entry(vinculo).Property("ID_MUNICIPIO").CurrentValue = vinculo.Municipio.Id;
        context.SaveChanges();
    }

    public bool Delete(long idUsuario, long idMunicipio)
    {
        // ✅ FIX: Usar FirstOrDefault ao invés de Any
        var exists = context.UsuariosMunicipios.AsNoTracking()
            .FirstOrDefault(um => 
                EF.Property<long>(um, "ID_USUARIO") == idUsuario &&
                EF.Property<long>(um, "ID_MUNICIPIO") == idMunicipio) != null;
        
        if (!exists) return false;

        context.Database.ExecuteSqlRaw(
            "DELETE FROM TB_USUARIO_MUNICIPIO WHERE ID_USUARIO = {0} AND ID_MUNICIPIO = {1}", 
            idUsuario, idMunicipio);
        
        return true;
    }

    public void SaveChanges() => context.SaveChanges();
}