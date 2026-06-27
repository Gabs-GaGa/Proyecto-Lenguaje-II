using ControlInventario.Data;
using ControlInventario.Models;
using ControlInventario.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace ControlInventario.Data.Seeders;

public static class MovimientoInventarioSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.MovimientosInventario.AnyAsync())
            return;

        var producto = await context.Productos
            .FirstOrDefaultAsync(p => p.CodigoSku == "COMP-001");

        if (producto == null)
            return;

        var movimientos = new List<MovimientoInventario>
        {
            new() {
                ProductoId = producto.Id,
                Tipo       = TipoMovimiento.Entrada,
                Cantidad   = 10,
                Fecha      = DateTime.UtcNow.AddDays(-10),
                Motivo     = "Compra inicial de stock"
            },
            new() {
                ProductoId = producto.Id,
                Tipo       = TipoMovimiento.Salida,
                Cantidad   = 2,
                Fecha      = DateTime.UtcNow.AddDays(-5),
                Motivo     = "Venta a cliente"
            },
        };

        await context.MovimientosInventario.AddRangeAsync(movimientos);
        await context.SaveChangesAsync();
    }
}