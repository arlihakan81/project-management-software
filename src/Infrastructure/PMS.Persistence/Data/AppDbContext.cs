using Microsoft.EntityFrameworkCore;
using PMS.Application.Interfaces;
using PMS.Domain.Entities;
using PMS.Domain.Entities.Commons;

namespace PMS.Persistence.Data
{
    public class AppDbContext(IOrganizationService organizationService = null!) : DbContext
    {
        private readonly IOrganizationService _organizationService = organizationService;

        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=ProjectDb; Trusted_Connection=True; TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(u => u.OrganizationId == _organizationService.GetCurrentOrganizationId() && !u.IsDeleted);

            modelBuilder.Entity<Project>().HasQueryFilter(p => p.OrganizationId == _organizationService.GetCurrentOrganizationId() && !p.IsDeleted && !p.IsArchived);

            modelBuilder.Entity<Project>().HasOne(p => p.Manager).WithMany(_ => _.ManagedProjects).HasForeignKey(p => p.ManagerId);
            modelBuilder.Entity<Project>().HasOne(p => p.CreatedBy).WithMany().HasForeignKey(p => p.CreatedById).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Project>().HasOne(p => p.UpdatedBy).WithMany().HasForeignKey(p => p.UpdatedById).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>().HasData([
                new Role()
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin"
                },
                new Role()
                {
                    Id = Guid.NewGuid(),
                    Name = "User"
                }
            ]);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Eklenen veya güncellenen tüm entity'leri bul
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                // Eğer entity ITenantEntity ise
                if (entry.Entity is BaseEntity baseEntity)
                {
                    // Yeni eklenen entity'ler için
                    if (entry.State == EntityState.Added)
                    {
                        // Tenant ID'yi otomatik ata
                        if (!organizationService.IsAuthenticated())
                        {
                            return await base.SaveChangesAsync(cancellationToken);
                        }
                        baseEntity.OrganizationId = organizationService.GetCurrentOrganizationId();
                        baseEntity.CreatedById = organizationService.GetAuthenticatedUserId();
                    }
                    // Güncellenen entity'lerde TenantId değişmesin
                    else if (entry.State == EntityState.Modified)
                    {
                        entry.Property("OrganizationId").IsModified = false;
                        entry.Property("CreatedById").IsModified = false;
                        entry.Property("CreatedAt").IsModified = false;
                        baseEntity.UpdatedById = organizationService.GetAuthenticatedUserId();
                        baseEntity.UpdatedAt = DateTime.Now;
                    }
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }



    }
}
