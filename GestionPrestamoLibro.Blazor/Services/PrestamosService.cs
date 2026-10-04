using Aplicada1.Core;
using GestionPrestamoLibro.Context;
using GestionPrestamoLibro.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestionPrestamoLibro.Services;

public class PrestamosService : Aplicada1.Core.IService<Prestamo, int>
{

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

    private async Task<bool> Insertar(Prestamo prestamo)
    {
        await using var contexto = await _contextFactory.CreateDbContextAsync();
        contexto.Prestamos.Add(prestamo);
        var cantidad = await contexto.SaveChangesAsync();
        return cantidad > 0;
    }

    private async Task<bool> Modificar(Prestamo prestamo)
    {
        await using var contexto = await _contextFactory.CreateDbContextAsync();
        contexto.Update(prestamo);
        var cantidad = await contexto.SaveChangesAsync();
        return cantidad > 0;
    }

    public async Task<bool> Guardar(Prestamo prestamo)
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

    public async Task<Prestamo?> Buscar(int prestamoId)
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

    public async Task<List<Prestamo>> GetList(Expression<Func<Prestamo, bool>> criterio)
    {
        await using var contexto = await _contextFactory.CreateDbContextAsync();
        var lista = await contexto.Prestamos
            .Include(p => p.Deudor)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
        return lista;
    }

    public async Task<List<Prestamo>> GetPrestamosPendientes(int deudorId)
    {
        await using var contexto = await _contextFactory.CreateDbContextAsync();
        var prestamosDelDeudor = await contexto.Prestamos
            .Where(p => p.DeudorId == deudorId)
            .OrderBy(p => p.PrestamoId)
            .AsNoTracking()
            .ToListAsync();

        List<Prestamo> pendientes = new List<Prestamo>();

        foreach (var prestamo in prestamosDelDeudor)
        {
            if (prestamo.Balance > 0)
            {
                pendientes.Add(prestamo);
            }
        }

        return pendientes;
    }

    public async Task<Prestamo?> BuscarPrestamo(int id)
    {
        await using var contexto = await _contextFactory.CreateDbContextAsync();
        var prestamo = await contexto.Prestamos
            .Include(p => p.Deudor)
            .FirstOrDefaultAsync(p => p.DeudorId == id);
        return prestamo;
    }
}
