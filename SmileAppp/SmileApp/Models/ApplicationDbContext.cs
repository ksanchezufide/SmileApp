﻿using Microsoft.EntityFrameworkCore;

namespace SmileApp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Tablas (entidades)
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Producto
            modelBuilder.Entity<Producto>().Property(p => p.Nombre).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Producto>().Property(p => p.Cantidad).HasDefaultValue(0);
            modelBuilder.Entity<Producto>().Property(p => p.Activo).HasDefaultValue(true);

            // Configuración de Usuario
            modelBuilder.Entity<Usuario>().Property(u => u.Nombre).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Usuario>().Property(u => u.Correo).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Usuario>().HasIndex(u => u.Correo).IsUnique();
            modelBuilder.Entity<Usuario>().Property(u => u.ContraseñaHash).IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.Estado).HasDefaultValue(true);
            modelBuilder.Entity<Usuario>().Property(u => u.FechaRegistro).HasDefaultValueSql("GETDATE()");

            // Configuración de Rol
            modelBuilder.Entity<Rol>().Property(r => r.Nombre).IsRequired().HasMaxLength(50);
        }
    }
}
