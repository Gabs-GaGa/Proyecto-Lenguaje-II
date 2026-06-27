using ControlInventario.Data;
using ControlInventario.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlInventario.Data.Seeders;

public static class ProveedorSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Proveedores.AnyAsync())
            return;

        var proveedores = new List<Proveedor>
        {
            new() { Nombre = "TechDistrib C.A.",    Telefono = "0212-5550101", Email = "ventas@techdistrib.com" },
            new() { Nombre = "InfoSuministros S.A.", Telefono = "0212-5550202", Email = "info@infosuministros.com" },
            new() { Nombre = "MegaComp Caracas",     Telefono = "0212-5550303", Email = "pedidos@megacomp.com" },
            new() { Nombre = "NetProv Venezuela",    Telefono = "0212-5550404", Email = "contacto@netprov.com" },
        };

        await context.Proveedores.AddRangeAsync(proveedores);
        await context.SaveChangesAsync();
    }
}