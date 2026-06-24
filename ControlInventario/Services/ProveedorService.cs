using ControlInventario.Data;
using ControlInventario.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlInventario.Services;

public class ProveedorService : IProveedorService
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

    public ProveedorService(IDbContextFactory<AppDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<List<Proveedor>> GetAllAsync()
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Proveedores
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Proveedor?> GetByIdAsync(int id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Proveedores
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Proveedor> AddAsync(Proveedor proveedor)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        context.Proveedores.Add(proveedor);
        await context.SaveChangesAsync();
        return proveedor;
    }

    public async Task UpdateAsync(Proveedor proveedor)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        context.Proveedores.Update(proveedor);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var proveedor = await context.Proveedores.FindAsync(id);
        if (proveedor != null)
        {
            context.Proveedores.Remove(proveedor);
            await context.SaveChangesAsync();
        }
    }
}
