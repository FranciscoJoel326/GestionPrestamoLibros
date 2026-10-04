using GestionPrestamoLibro.Context;
using GestionPrestamoLibro.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Aplicada1.Core;

namespace GestionPrestamoLibro.Services;

public class LibroService : Aplicada1.Core.IService<Cobros, int>
{
    private readonly IDbContextFactory<Contexto> _contextFactory;

    public LibroService(IDbContextFactory<Contexto> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    private async Task<bool> Existe(int? LibroId)
    {
        await using var contexto = await _contextFactory.CreateDbContextAsync();
        return await contexto.Cobros.AnyAsync(c=> c.Il == LibroId);

    private async Task<bool> Insertar(Cobros cobro)
    {
        await using var contexto = await _contextFactory.CreateDbContextAsync();
        contexto.Cobros.Add(cobro);
        await AfectarPrestamos(cobro.CobrosDetalle, TipoOperacion.Resta);
        var cantidad = await contexto.SaveChangesAsync();
        return cantidad > 0;
    }

    private async Task AfectarPrestamos(ICollection<PrestamoDetalle> detalle, TipoOperacion tipoOperacion)
    {
        await using var contexto = await _contextFactory.CreateDbContextAsync();

        foreach (var item in detalle)
        {
            var prestamo = await contexto.Prestamos.FirstAsync(p => p.PrestamoId == item.PrestamoId);

            if (tipoOperacion == TipoOperacion.Resta)
            {
                prestamo.Balance = prestamo.Balance - item.ValorCobrado;
            }
            else
            {
                prestamo.Balance = prestamo.Balance + item.ValorCobrado;
            }
        }
    }

    private async Task<bool> Modificar(Cobros cobro)
    {
        await using var contexto = await _contextFactory.CreateDbContextAsync();
        contexto.Update(cobro);
        var cantidad = await contexto.SaveChangesAsync();
        return cantidad > 0;
    }

    public async Task<bool> Guardar(Cobros cobro)
    {
        bool existe = await Existe(cobro.CobroId);

        if (existe == false)
        {
            return await Insertar(cobro);
        }
        else
        {
            return await Modificar(cobro);
        }
    }

    public async Task<Cobros?> Buscar(int cobroId)
    {
        await using var contexto = await _contextFactory.CreateDbContextAsync();
        var cobro = await contexto.Cobros
            .Include(c => c.Deudor)
            .Include(c => c.CobrosDetalle)
            .FirstOrDefaultAsync(c => c.CobroId == cobroId);
        return cobro;
    }

    public async Task<bool> Eliminar(int cobroId)
    {
        await using var contexto = await _contextFactory.CreateDbContextAsync();
        var cobro = await contexto.Cobros
            .Include(c => c.CobrosDetalle)
            .FirstOrDefaultAsync(c => c.CobroId == cobroId);

        if (cobro == null)
        {
            return false;
        }

        await AfectarPrestamos(cobro.CobrosDetalle, TipoOperacion.Suma);

        contexto.CobrosDetalle.RemoveRange(cobro.CobrosDetalle);
        contexto.Cobros.Remove(cobro);
        var cantidad = await contexto.SaveChangesAsync();
        return cantidad > 0;
    }

    public Task<List<Cobros>> GetList(Expression<Func<Cobros, bool>> criterio)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Cobros>> Listar(Expression<Func<Cobros, bool>> criterio)
    {
        await using var contexto = await _contextFactory.CreateDbContextAsync();
        var lista = await contexto.Cobros
            .Include(c => c.Deudor)
            .Include(c => c.CobrosDetalle)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
        return lista;
    }
}

public enum TipoOperacion
{
    Suma = 1,
    Resta = 2
}

