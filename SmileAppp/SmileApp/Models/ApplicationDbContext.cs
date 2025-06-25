using Microsoft.EntityFrameworkCore;

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
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<ArchivoPaciente> ArchivosPacientes { get; set; }

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

            // Relación Usuario → Rol
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.RolId)
                .OnDelete(DeleteBehavior.Restrict); // Evita que se borre el rol si hay usuarios

            // Configuración de Rol
            modelBuilder.Entity<Rol>().Property(r => r.Nombre).IsRequired().HasMaxLength(50);

            // Configuración de Paciente
            modelBuilder.Entity<Paciente>()
                .Property(p => p.FechaRegistro)
                .HasDefaultValueSql("GETDATE()");

            // Configuración de ArchivoPaciente
            modelBuilder.Entity<ArchivoPaciente>()
                .HasOne(a => a.Paciente)
                .WithMany(p => p.Archivos)
                .HasForeignKey(a => a.PacienteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}