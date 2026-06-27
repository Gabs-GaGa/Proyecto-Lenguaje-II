using ControlInventario.Data;
using ControlInventario.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlInventario.Data.Seeders;

public static class ProductoSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Productos.AnyAsync())
            return;

        var categoria = await context.Categorias
            .FirstOrDefaultAsync(c => c.Nombre == "Computación");
        var proveedor = await context.Proveedores
            .FirstOrDefaultAsync(p => p.Nombre == "TechDistrib C.A.");

        if (categoria == null || proveedor == null)
            return;

        var productos = new List<Producto>
        {
            new() {
                CodigoSku    = "COMP-001",
                Nombre       = "Laptop Dell Inspiron 15",
                Descripcion  = "Laptop 15 pulgadas, 16GB RAM, 512GB SSD",
                PrecioCompra = 800.00m,
                PrecioVenta  = 1050.00m,
                StockActual  = 10,
                StockMinimo  = 3,
                CategoriaId  = categoria.Id,
                ProveedorId  = proveedor.Id
            },
            new() {
                CodigoSku    = "COMP-002",
                Nombre       = "Mouse Inalámbrico Logitech",
                Descripcion  = "Mouse inalámbrico, 2.4GHz, batería AA",
                PrecioCompra = 15.00m,
                PrecioVenta  = 25.00m,
                StockActual  = 50,
                StockMinimo  = 10,
                CategoriaId  = categoria.Id,
                ProveedorId  = proveedor.Id
            },
            new() {
                CodigoSku    = "COMP-003",
                Nombre       = "Teclado Mecánico Redragon",
                Descripcion  = "Teclado mecánico RGB, switches red",
                PrecioCompra = 40.00m,
                PrecioVenta  = 65.00m,
                StockActual  = 25,
                StockMinimo  = 5,
                CategoriaId  = categoria.Id,
                ProveedorId  = proveedor.Id
            },
        };

        await context.Productos.AddRangeAsync(productos);
        await context.SaveChangesAsync();
    }
}