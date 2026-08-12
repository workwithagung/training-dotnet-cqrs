using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Shared;

namespace UserManagement.Infrastructure.Persistence;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Pegawai> Pegawais { get; set; }
    public DbSet<Jabatan> Jabatans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseModel).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .HasKey(nameof(BaseModel.Id));

                modelBuilder.Entity(entityType.ClrType)
                    .Property(nameof(BaseModel.DateCreated))
                    .HasColumnType("datetime");

                modelBuilder.Entity(entityType.ClrType)
                    .Property(nameof(BaseModel.DateUpdated))
                    .HasColumnType("datetime");

                modelBuilder.Entity(entityType.ClrType)
                    .Property(nameof(BaseModel.DateDeleted))
                    .HasColumnType("datetime");

                modelBuilder.Entity(entityType.ClrType)
                    .Property(nameof(BaseModel.DateDeleted));
                
                // apply soft delete filter
                var method = typeof(ApplicationDbContext)
                    .GetMethod(nameof(ApplySoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Instance)!
                    .MakeGenericMethod(entityType.ClrType);
                
                method.Invoke(this, new object[] { modelBuilder });
            }
        }
        modelBuilder.Entity<Pegawai>(e =>
        {
            e.ToTable("hris_pegawai");
            e.HasOne(x => x.Jabatan);
        });

        modelBuilder.Entity<Jabatan>(e =>
        {
            e.ToTable("hris_jabatan");
            e.HasIndex(x => x.Nama).IsUnique();
        });
        
        base.OnModelCreating(modelBuilder);
    }

    private void ApplySoftDeleteFilter<T>(ModelBuilder modelBuilder) where T : BaseModel
    {
        modelBuilder.Entity<T>().HasQueryFilter(e => e.DateDeleted == null);
    }
}