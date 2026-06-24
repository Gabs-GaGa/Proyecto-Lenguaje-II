using ControlInventario.Models;

namespace ControlInventario.Services;

public interface IProveedorService
{
    Task<List<Proveedor>> GetAllAsync();
    Task<Proveedor?> GetByIdAsync(int id);
    Task<Proveedor> AddAsync(Proveedor proveedor);
    Task UpdateAsync(Proveedor proveedor);
    Task DeleteAsync(int id);
}
