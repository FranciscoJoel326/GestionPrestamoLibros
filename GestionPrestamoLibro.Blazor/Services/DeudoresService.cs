using GestionPrestamoLibro.Context;
using GestionPrestamoLibro.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestionPrestamoLibro.Services;

public class DeudoresService : Aplicada1.Core.IService<Deudores, int>
{
    private readonly IDbContextFactory<Contexto> _contextFactory;

    public DeudoresService(IDbContextFactory<Contexto> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public Task<bool> Guardar(Deudores entidad)
    {
        throw new NotImplementedException();
    }

    public async Task<Deudores?> Buscar(int deudorId)
    {
        await using var contexto = await _contextFactory.CreateDbContextAsync();
        var deudor = await contexto.Deudores
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.DeudorId == deudorId);
        return deudor;
    }

    public Task<bool> Eliminar(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Deudores>> GetList(Expression<Func<Deudores, bool>> criterio)
    {
        await using var contexto = await _contextFactory.CreateDbContextAsync();
        var lista = await contexto.Deudores
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
        return lista;
    }
}
