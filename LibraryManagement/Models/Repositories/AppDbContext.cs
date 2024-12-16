using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models.Repositories
{
    public class AppDbContext : IdentityDbContext<AppUser,AppRole,Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Ek model yapılandırmaları
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id); // Kitapların Primary Key'i
                entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Author).IsRequired().HasMaxLength(255);
                entity.Property(e => e.PublicationYear).IsRequired();
                entity.Property(e => e.ISBN).IsRequired().HasMaxLength(13);
            });

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", PublicationYear = 1925, ISBN = "9780743273565" },
                new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", PublicationYear = 1960, ISBN = "9780061120084" },
                new Book { Id = 3, Title = "1984", Author = "George Orwell", PublicationYear = 1949, ISBN = "9780451524935" }
            );

            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);

            modelBuilder.Entity<Permission>().HasData(
                new Permission
                {
                    Id = 1,
                    PermissionName = "Viewer",
                    Description = "Allows access to list books"
                }
            );
        }
    }
}
