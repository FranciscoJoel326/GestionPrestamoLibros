using GestionPrestamoLibro.Context;
using GestionPrestamoLibro.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestionPrestamoLibro.Services;

public class PrestamosService : Aplicada1.Core.IService<Prestamos, int>
{
    private readonly IDbContextFactory<Contexto> _contextFactory;

    public PrestamosService(IDbContextFactory<Contexto> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    private async Task<bool> Existe(int prestamoId)
    {
        await using var contexto = await _contextFactory.CreateDbContextAsync();
        var prestamo = await contexto.Prestamos.FindAsync(prestamoId);

        if (prestamo == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private async Task<bool> Insertar(Prestamos prestamo)
    {
        await using var contexto = await _contextFactory.CreateDbContextAsync();
        contexto.Prestamos.Add(prestamo);
        var cantidad = await contexto.SaveChangesAsync();
        return cantidad > 0;
    }

    private async Task<bool> Modificar(Prestamos prestamo)
    {
        await using var contexto = await _contextFactory.CreateDbContextAsync();
        contexto.Update(prestamo);
        var cantidad = await contexto.SaveChangesAsync();
        return cantidad > 0;
    }

    public async Task<bool> Guardar(Prestamos prestamo)
    {
        prestamo.Balance = prestamo.Monto;

        bool existe = await Existe(prestamo.PrestamoId);

        if (existe == false)
        {
            return await Insertar(prestamo);
        }
        else
        {
            return await Modificar(prestamo);
        }
    }

    public async Task<Prestamos?> Buscar(int prestamoId)
    {
        await using var contexto = await _contextFactory.CreateDbContextAsync();
        var prestamo = await contexto.Prestamos
            .Include(p => p.Deudor)
            .FirstOrDefaultAsync(p => p.PrestamoId == prestamoId);
        return prestamo;
    }

    public async Task<bool> Eliminar(int prestamoId)
    {
        await using var contexto = await _contextFactory.CreateDbContextAsync();
        var prestamo = await contexto.Prestamos.FindAsync(prestamoId);

        if (prestamo == null)
        {
            return false;
        }

        contexto.Prestamos.Remove(prestamo);
        var cantidad = await contexto.SaveChangesAsync();
        return cantidad > 0;
    }

    public async Task<List<Prestamos>> GetList(Expression<Func<Prestamos, bool>> criterio)
    {
        await using var contexto = await _contextFactory.CreateDbContextAsync();
        var lista = await contexto.Prestamos
            .Include(p => p.Deudor)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
        return lista;
    }

    public async Task<List<Prestamos>> GetPrestamosPendientes(int deudorId)
    {
        await using var contexto = await _contextFactory.CreateDbContextAsync();
        var prestamosDelDeudor = await contexto.Prestamos
            .Where(p => p.DeudorId == deudorId)
            .OrderBy(p => p.PrestamoId)
            .AsNoTracking()
            .ToListAsync();

        List<Prestamos> pendientes = new List<Prestamos>();

        foreach (var prestamo in prestamosDelDeudor)
        {
            if (prestamo.Balance > 0)
            {
                pendientes.Add(prestamo);
            }
        }

        return pendientes;
    }

    public async Task<Prestamos?> BuscarPrestamo(int id)
    {
        await using var contexto = await _contextFactory.CreateDbContextAsync();
        var prestamo = await contexto.Prestamos
            .Include(p => p.Deudor)
            .FirstOrDefaultAsync(p => p.DeudorId == id);
        return prestamo;
    }
}
