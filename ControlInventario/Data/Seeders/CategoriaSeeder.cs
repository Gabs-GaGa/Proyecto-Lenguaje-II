using ControlInventario.Data;
using ControlInventario.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlInventario.Data.Seeders;

public static class CategoriaSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Categorias.AnyAsync())
            return;

        var categorias = new List<Categoria>
        {
            new() { Nombre = "Electrónica",      Descripcion = "Equipos y dispositivos electrónicos" },
            new() { Nombre = "Computación",      Descripcion = "Equipos de cómputo y accesorios" },
            new() { Nombre = "Redes",            Descripcion = "Equipos de conectividad y redes" },
            new() { Nombre = "Almacenamiento",   Descripcion = "Dispositivos de almacenamiento de datos" },
            new() { Nombre = "Periféricos",      Descripcion = "Dispositivos periféricos de entrada y salida" },
        };

        await context.Categorias.AddRangeAsync(categorias);
        await context.SaveChangesAsync();
    }
}