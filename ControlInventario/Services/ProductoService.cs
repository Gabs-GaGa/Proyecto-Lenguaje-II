using ControlInventario.Data;
using ControlInventario.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlInventario.Services;

public class ProductoService : IProductoService
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

    public ProductoService(IDbContextFactory<AppDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<List<Producto>> GetAllAsync()
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Productos
            .Include(p => p.Categoria)
            .Include(p => p.Proveedor)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Producto?> GetByIdAsync(int id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Productos
            .Include(p => p.Categoria)
            .Include(p => p.Proveedor)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Producto> AddAsync(Producto producto)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        if (string.IsNullOrWhiteSpace(producto.Nombre))
            throw new InvalidOperationException("El nombre del producto es obligatorio.");
        if (string.IsNullOrWhiteSpace(producto.CodigoSku))
            throw new InvalidOperationException("El código SKU es obligatorio.");
        if (producto.PrecioCompra < 0)
            throw new InvalidOperationException("El precio de compra no puede ser negativo.");
        if (producto.PrecioVenta < 0)
            throw new InvalidOperationException("El precio de venta no puede ser negativo.");
        if (producto.StockActual < 0)
            throw new InvalidOperationException("El stock actual no puede ser negativo.");
        if (producto.StockMinimo < 0)
            throw new InvalidOperationException("El stock mínimo no puede ser negativo.");

        var categoriaExists = await context.Categorias.AnyAsync(c => c.Id == producto.CategoriaId);
        if (!categoriaExists)
            throw new InvalidOperationException("La categoría especificada no existe.");

        if (producto.ProveedorId.HasValue)
        {
            var proveedorExists = await context.Proveedores.AnyAsync(p => p.Id == producto.ProveedorId.Value);
            if (!proveedorExists)
                throw new InvalidOperationException("El proveedor especificado no existe.");
        }

        context.Productos.Add(producto);
        await context.SaveChangesAsync();
        return producto;
    }

    public async Task UpdateAsync(Producto producto)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        if (string.IsNullOrWhiteSpace(producto.Nombre))
            throw new InvalidOperationException("El nombre del producto es obligatorio.");
        if (string.IsNullOrWhiteSpace(producto.CodigoSku))
            throw new InvalidOperationException("El código SKU es obligatorio.");
        if (producto.PrecioCompra < 0)
            throw new InvalidOperationException("El precio de compra no puede ser negativo.");
        if (producto.PrecioVenta < 0)
            throw new InvalidOperationException("El precio de venta no puede ser negativo.");
        if (producto.StockActual < 0)
            throw new InvalidOperationException("El stock actual no puede ser negativo.");
        if (producto.StockMinimo < 0)
            throw new InvalidOperationException("El stock mínimo no puede ser negativo.");

        var categoriaExists = await context.Categorias.AnyAsync(c => c.Id == producto.CategoriaId);
        if (!categoriaExists)
            throw new InvalidOperationException("La categoría especificada no existe.");

        if (producto.ProveedorId.HasValue)
        {
            var proveedorExists = await context.Proveedores.AnyAsync(p => p.Id == producto.ProveedorId.Value);
            if (!proveedorExists)
                throw new InvalidOperationException("El proveedor especificado no existe.");
        }

        context.Productos.Update(producto);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var producto = await context.Productos.FindAsync(id);
        if (producto != null)
        {
            context.Productos.Remove(producto);
            await context.SaveChangesAsync();
        }
    }
}
