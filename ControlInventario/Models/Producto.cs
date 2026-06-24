namespace ControlInventario.Models;

public class Producto
{
    public int Id { get; set; }
    public string CodigoSku { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public decimal PrecioCompra { get; set; }
    public decimal PrecioVenta { get; set; }
    public int StockActual { get; set; }
    public int StockMinimo { get; set; }
    public int CategoriaId { get; set; }
    public int? ProveedorId { get; set; }

    public Categoria Categoria { get; set; } = null!;
    public Proveedor? Proveedor { get; set; }
    public ICollection<MovimientoInventario> MovimientosInventario { get; set; } = new List<MovimientoInventario>();
}
