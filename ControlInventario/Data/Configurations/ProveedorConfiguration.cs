using ControlInventario.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlInventario.Data.Configurations;

public class ProveedorConfiguration : IEntityTypeConfiguration<Proveedor>
{
    public void Configure(EntityTypeBuilder<Proveedor> builder)
    {
        builder.ToTable("proveedores");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Nombre)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(e => e.Telefono)
            .HasMaxLength(20);

        builder.Property(e => e.Email)
            .HasMaxLength(100);
    }
}
