using ControlAsistencia.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlAsistencia.Data.Configurations;

public class MovimientoInventarioConfiguration : IEntityTypeConfiguration<MovimientoInventario>
{
    public void Configure(EntityTypeBuilder<MovimientoInventario> builder)
    {
        builder.ToTable("movimientos_inventario");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.ProductoId)
            .IsRequired();

        builder.Property(e => e.Tipo)
            .HasMaxLength(20)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(e => e.Cantidad)
            .IsRequired();

        builder.Property(e => e.Fecha)
            .IsRequired()
            .HasDefaultValueSql("NOW()");

        builder.Property(e => e.Motivo)
            .HasMaxLength(255);

        builder.HasOne(e => e.Producto)
            .WithMany(e => e.MovimientosInventario)
            .HasForeignKey(e => e.ProductoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
