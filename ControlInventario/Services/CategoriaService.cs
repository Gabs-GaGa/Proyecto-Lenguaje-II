using ControlInventario.Data;
using ControlInventario.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlInventario.Services;

public class CategoriaService : ICategoriaService
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

    public CategoriaService(IDbContextFactory<AppDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<List<Categoria>> GetAllAsync()
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Categorias
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Categoria?> GetByIdAsync(int id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Categorias
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Categoria> AddAsync(Categoria categoria)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        context.Categorias.Add(categoria);
        await context.SaveChangesAsync();
        return categoria;
    }

    public async Task UpdateAsync(Categoria categoria)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        context.Categorias.Update(categoria);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();
        var categoria = await context.Categorias.FindAsync(id);
        if (categoria != null)
        {
            context.Categorias.Remove(categoria);
            await context.SaveChangesAsync();
        }
    }
}
