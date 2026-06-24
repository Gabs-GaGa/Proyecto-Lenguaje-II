using ControlInventario.Models.Enums;

namespace ControlInventario.Models;

public class MovimientoInventario
{
    public int Id { get; set; }
    public int ProductoId { get; set; }
    public TipoMovimiento Tipo { get; set; }
    public int Cantidad { get; set; }
    public DateTime Fecha { get; set; }
    public string? Motivo { get; set; }

    public Producto Producto { get; set; } = null!;
}
