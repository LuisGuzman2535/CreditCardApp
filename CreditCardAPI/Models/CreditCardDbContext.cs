using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CreditCardAPI.Models;

public partial class CreditCardDbContext : DbContext
{
    // Campo para el logger
    private readonly ILogger<CreditCardDbContext> _logger;

    public CreditCardDbContext()
    {
    }

    // Constructor con parámetros para opciones de DbContext y logger
    public CreditCardDbContext(DbContextOptions<CreditCardDbContext> options,
        ILogger<CreditCardDbContext> logger)
        : base(options)
    {
        _logger = logger;
    }

    public virtual DbSet<TarjetasDeCredito> TarjetasDeCreditos { get; set; }

    public virtual DbSet<Transacciones> Transacciones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }
    public DbSet<EstadoCuenta> EstadoCuentas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EstadoCuenta>().HasNoKey();

        modelBuilder.Entity<TarjetasDeCredito>(entity =>
        {
            entity.HasKey(e => e.TarjetaId).HasName("PK__Tarjetas__C825069623385015");

            entity.ToTable("TarjetasDeCredito");

            entity.Property(e => e.TarjetaId).HasColumnName("TarjetaID");
            entity.Property(e => e.LimiteCredito).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NumeroTarjeta).HasMaxLength(16);
            entity.Property(e => e.SaldoActual).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SaldoDisponible)
                .HasComputedColumnSql("([LimiteCredito]-[SaldoActual])", false)
                .HasColumnType("decimal(19, 2)");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.TarjetasDeCreditos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__TarjetasD__Usuar__398D8EEE");
        });

        modelBuilder.Entity<Transacciones>(entity =>
        {
            entity.HasKey(e => e.TransaccionId).HasName("PK__Transacc__86A849DEBF99D5CC");

            entity.Property(e => e.TransaccionId).HasColumnName("TransaccionID");
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TarjetaId).HasColumnName("TarjetaID");

            entity.HasOne(d => d.Tarjeta).WithMany(p => p.Transacciones)
                .HasForeignKey(d => d.TarjetaId)
                .HasConstraintName("FK__Transacci__Tarje__3C69FB99");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE798359E44CA");

            entity.HasIndex(e => e.Correo, "UQ__Usuarios__60695A19AAE06393").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }
    // Método para obtener el estado de cuenta de una tarjeta de crédito
    public async Task<EstadoCuenta> ObtenerEstadoCuentaAsync(int tarjetaId)
    {
        try
        {
            var estadoCuenta = EstadoCuentas
                .FromSqlRaw("EXEC ObtenerEstadoDeCuenta @TarjetaID = {0}", tarjetaId)
                .AsEnumerable()
                .FirstOrDefault();
            return estadoCuenta;
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, $"Error al obtener el estado de cuenta para la tarjeta ID {tarjetaId}");
            throw new Exception("Error al obtener el estado de cuenta", ex);
        }
    }

    // Método para agregar una transacción a la base de datos puede ser del tipo COMPRA O PAGO
    public async Task AgregarTransaccionAsync(Transacciones transacciones)
    {
        try
        {
            await Database.ExecuteSqlRawAsync("EXEC AgregarTransaccion " +
                "@TarjetaID = {0}, " +
                "@Fecha = {1}, " +
                "@Descripcion = {2}, " +
                "@Monto = {3}, " +
                "@Tipo = {4}",
                transacciones.TarjetaId,
                transacciones.Fecha,
                transacciones.Descripcion,
                transacciones.Monto,
                transacciones.TipoTransaccion);
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "Error al ejecutar la transacción");
            throw new Exception("Error al ejecutar la transacción", ex);
        }
    }

    // Método para obtener el historial de transacciones de una tarjeta de crédito
    public async Task<List<Transacciones>> HistorialTransacciones(int id)
    {
        try
        {
            var historialTransacciones = await Transacciones
                .FromSqlRaw("EXEC ObtenerHistorialTransacciones @TarjetaID = {0}", id)
                .ToListAsync();
            return historialTransacciones;
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "Error al obtener las transacciones");
            throw new Exception("Error al obtener el historial de transacciones", ex);
        }
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
