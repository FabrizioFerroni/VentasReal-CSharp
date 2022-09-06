using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace WSVentas.Models
{
        public partial class VentasRealContext : DbContext
        {
            public VentasRealContext()
            {
            }

            public VentasRealContext(DbContextOptions<VentasRealContext> options)
                : base(options)
            {
            }

            public virtual DbSet<Cliente> Cliente { get; set; }
            public virtual DbSet<Concepto> Concepto { get; set; }
            public virtual DbSet<Producto> Producto { get; set; }
            public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Venta> Venta { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    IConfigurationRoot configuration = new ConfigurationBuilder()
                                      .SetBasePath(Directory.GetCurrentDirectory())
                                      .AddJsonFile("appsettings.json")
                                      .Build();
                    var connectionString = configuration.GetConnectionString("Conexion");
                    optionsBuilder.UseSqlServer(connectionString);
                }
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Cliente>(entity =>
                {
                    entity.ToTable("cliente");

                    entity.Property(e => e.Id).HasColumnName("id");

                    entity.Property(e => e.Nombre)
                        .IsRequired()
                        .HasColumnName("nombre")
                        .HasMaxLength(50)
                        .IsUnicode(false);
                });

                modelBuilder.Entity<Concepto>(entity =>
                {
                    entity.ToTable("concepto");

                    entity.Property(e => e.Id).HasColumnName("id");

                    entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                    entity.Property(e => e.IdProducto).HasColumnName("id_producto");

                    entity.Property(e => e.IdVenta).HasColumnName("id_venta");

                    entity.Property(e => e.Importe)
                        .HasColumnName("importe")
                        .HasColumnType("decimal(16, 2)");

                    entity.Property(e => e.PrecioUnitario)
                        .HasColumnName("precioUnitario")
                        .HasColumnType("decimal(16, 2)");

                    entity.HasOne(d => d.IdProductoNavigation)
                        .WithMany(p => p.Concepto)
                        .HasForeignKey(d => d.IdProducto)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_concepto_producto");

                    entity.HasOne(d => d.IdVentaNavigation)
                        .WithMany(p => p.Concepto)
                        .HasForeignKey(d => d.IdVenta)
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_concepto_venta");
                });

                modelBuilder.Entity<Producto>(entity =>
                {
                    entity.ToTable("producto");

                    entity.Property(e => e.Id).HasColumnName("id");

                    entity.Property(e => e.Costo)
                        .HasColumnName("costo")
                        .HasColumnType("decimal(16, 2)");

                    entity.Property(e => e.Nombre)
                        .IsRequired()
                        .HasColumnName("nombre")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    entity.Property(e => e.PrecioUnitario)
                        .HasColumnName("precioUnitario")
                        .HasColumnType("decimal(16, 2)");
                });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Venta>(entity =>
                {
                    entity.ToTable("venta");

                    entity.Property(e => e.Id).HasColumnName("id");

                    entity.Property(e => e.Fecha)
                        .HasColumnName("fecha")
                        .HasColumnType("datetime");

                    entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                    entity.Property(e => e.Total)
                        .HasColumnName("total")
                        .HasColumnType("decimal(16, 2)");

                    entity.HasOne(d => d.IdClienteNavigation)
                        .WithMany(p => p.Venta)
                        .HasForeignKey(d => d.IdCliente)
                        .HasConstraintName("FK_venta_cliente");
                });

                OnModelCreatingPartial(modelBuilder);
            }

            partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        }
    }
