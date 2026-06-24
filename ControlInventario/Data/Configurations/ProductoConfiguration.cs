using ControlInventario.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlInventario.Data.Configurations;

public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
{
    public void Configure(EntityTypeBuilder<Producto> builder)
    {
        builder.ToTable("productos");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.CodigoSku)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(e => e.CodigoSku)
            .IsUnique();

        builder.Property(e => e.Nombre)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(e => e.Descripcion)
            .HasColumnType("text");

        builder.Property(e => e.PrecioCompra)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(e => e.PrecioVenta)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(e => e.StockActual)
            .IsRequired();

        builder.Property(e => e.StockMinimo)
            .IsRequired();

        builder.Property(e => e.CategoriaId)
            .IsRequired();

        builder.Property(e => e.ProveedorId)
            .IsRequired(false);

        builder.HasOne(e => e.Categoria)
            .WithMany(e => e.Productos)
            .HasForeignKey(e => e.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Proveedor)
            .WithMany(e => e.Productos)
            .HasForeignKey(e => e.ProveedorId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
