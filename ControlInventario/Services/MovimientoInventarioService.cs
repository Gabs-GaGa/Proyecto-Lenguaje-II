using ControlInventario.Data;
using ControlInventario.Models;
using ControlInventario.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace ControlInventario.Services;

public class MovimientoInventarioService : IMovimientoInventarioService
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

    public MovimientoInventarioService(IDbContextFactory<AppDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<List<MovimientoInventario>> GetAllAsync()
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.MovimientosInventario
            .Include(m => m.Producto)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<MovimientoInventario?> GetByIdAsync(int id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.MovimientosInventario
            .Include(m => m.Producto)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<MovimientoInventario> AddAsync(MovimientoInventario movimiento)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        var producto = await context.Productos.FindAsync(movimiento.ProductoId);
        if (producto == null)
            throw new InvalidOperationException("El producto especificado no existe.");

        if (movimiento.Cantidad <= 0)
            throw new InvalidOperationException("La cantidad debe ser mayor a cero.");

        if (movimiento.Tipo == TipoMovimiento.Salida && producto.StockActual < movimiento.Cantidad)
            throw new InvalidOperationException($"Stock insuficiente. Disponible: {producto.StockActual}, solicitado: {movimiento.Cantidad}.");

        if (movimiento.Tipo == TipoMovimiento.Ajuste && movimiento.Cantidad < 0)
            throw new InvalidOperationException("La cantidad de ajuste no puede ser negativa.");

        switch (movimiento.Tipo)
        {
            case TipoMovimiento.Entrada:
                producto.StockActual += movimiento.Cantidad;
                break;
            case TipoMovimiento.Salida:
                producto.StockActual -= movimiento.Cantidad;
                break;
            case TipoMovimiento.Ajuste:
                producto.StockActual = movimiento.Cantidad;
                break;
        }

        context.MovimientosInventario.Add(movimiento);
        await context.SaveChangesAsync();
        return movimiento;
    }

    public async Task UpdateAsync(MovimientoInventario movimiento)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        context.MovimientosInventario.Update(movimiento);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var movimiento = await context.MovimientosInventario.FindAsync(id);
        if (movimiento != null)
        {
            context.MovimientosInventario.Remove(movimiento);
            await context.SaveChangesAsync();
        }
    }
}
