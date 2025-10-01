using Api.Data.AspNet;
using Api.Data.Models;
using Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<View> Views { get; set; }
        public DbSet<AspNetUser> Users { get; set; }
        public DbSet<RoleView> ViewRols { get; set; }
        public DbSet<Servicios> Servicios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<RegistroUsuarioDispositivo> RegistroUsuarioDispositivo { get; set; }

        public DbSet<AlumnoDTO> Alumnos { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<View>().ToTable("View");
            modelBuilder.Entity<RoleView>().ToTable("RoleView");
            modelBuilder.Entity<Servicios>().ToTable("Servicios");
            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<RegistroUsuarioDispositivo>().ToTable("RegistroUsuarioDispositivo");

            modelBuilder.Entity<AlumnoDTO>().HasNoKey();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<AspNetRoles>();
            modelBuilder.Ignore<AspNetRoleClaim>();
            modelBuilder.Entity<AspNetUser>();
            modelBuilder.Ignore<AspNetUserClaim>();
            modelBuilder.Ignore<AspNetUserLogin>();
            modelBuilder.Ignore<AspNetUserToken>();
            
            //modelBuilder.Entity<View>()
            //.HasMany(e => e.Roles)
            //.WithMany(e => e.Views)
            //.UsingEntity<RoleView>();


        }
    }
}
