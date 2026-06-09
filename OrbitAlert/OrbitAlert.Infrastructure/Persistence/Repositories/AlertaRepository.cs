using Microsoft.EntityFrameworkCore;
using OrbitAlert.Application.Interfaces.Repositories;
using OrbitAlert.Domain.Entities;
using OrbitAlert.Domain.Enum;
using OrbitAlert.Infrastructure.Persistence;

namespace OrbitAlert.Infrastructure.Persistence.Repositories;

public class AlertaRepository(OrbitAlertContext context) : IAlertaRepository
{
    public IReadOnlyList<Alerta> GetAll() =>
        context.Alertas.AsNoTracking()
            .Include(a => a.ZonaRisco).ThenInclude(z => z.Municipio)
            .Include(a => a.TipoAlerta).ToList();

    public Alerta? GetById(long id) =>
        context.Alertas.AsNoTracking()
            .Include(a => a.ZonaRisco).ThenInclude(z => z.Municipio)
            .Include(a => a.TipoAlerta)
            .FirstOrDefault(a => a.Id == id);

    public IReadOnlyList<Alerta> GetByStatus(StatusAlertaEnum status) =>
        context.Alertas.AsNoTracking()
            .Include(a => a.ZonaRisco).ThenInclude(z => z.Municipio)
            .Include(a => a.TipoAlerta)
            .Where(a => a.StStatus == status).ToList();

    public IReadOnlyList<Alerta> GetByMunicipio(long idMunicipio) =>
        context.Alertas.AsNoTracking()
            .Include(a => a.ZonaRisco).ThenInclude(z => z.Municipio)
            .Include(a => a.TipoAlerta)
            .Where(a => a.ZonaRisco.Municipio.Id == idMunicipio).ToList();

    public void Add(Alerta alerta)
    {
        // ✅ FIX: Usar context.Add() ao invés de EntityState.Added
        // Isso permite que EF Core chame a sequência corretamente
        context.Alertas.Add(alerta);
        context.SaveChanges();
    }

    public void Update(Alerta alerta)
    {
        // ✅ FIX: Usar context.Update() ao invés de EntityState.Modified
        context.Alertas.Update(alerta);
        context.SaveChanges();
    }

    public bool Delete(long id)
    {
        // ✅ FIX: Usar FirstOrDefault ao invés de Any
        var exists = context.Alertas.AsNoTracking()
            .FirstOrDefault(a => a.Id == id) != null;
        if (!exists) return false;

        context.Database.ExecuteSqlRaw("DELETE FROM TB_ANALISE_IA WHERE ID_ALERTA = {0}", id);
        context.Database.ExecuteSqlRaw("DELETE FROM TB_HISTORICO_ALERTA WHERE ID_ALERTA = {0}", id);
        context.Database.ExecuteSqlRaw("DELETE FROM TB_NOTIFICACAO WHERE ID_ALERTA = {0}", id);
        context.Database.ExecuteSqlRaw("DELETE FROM TB_ALERTA WHERE ID_ALERTA = {0}", id);

        return true;
    }

    public void SaveChanges() => context.SaveChanges();
}