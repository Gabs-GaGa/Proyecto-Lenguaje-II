using ControlInventario.Models;

namespace ControlInventario.Services;

public interface IMovimientoInventarioService
{
    Task<List<MovimientoInventario>> GetAllAsync();
    Task<MovimientoInventario?> GetByIdAsync(int id);
    Task<MovimientoInventario> AddAsync(MovimientoInventario movimiento);
    Task UpdateAsync(MovimientoInventario movimiento);
    Task DeleteAsync(int id);
}
