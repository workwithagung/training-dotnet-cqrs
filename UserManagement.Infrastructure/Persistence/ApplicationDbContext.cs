using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;

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
        modelBuilder.Entity<Pegawai>(e =>
        {
            e.ToTable("hris_pegawai");
            e.HasKey(x => x.Id);
            e.Property(x => x.DateCreated).HasColumnType("datetime");
            e.Property(x => x.DateUpdated).HasColumnType("datetime");
            e.HasOne(x => x.Jabatan);
        });

        modelBuilder.Entity<Jabatan>(e =>
        {
            e.ToTable("hris_jabatan");
            e.HasKey(x => x.Id);
            e.Property(x => x.DateCreated).HasColumnType("datetime");
            e.Property(x => x.DateUpdated).HasColumnType("datetime");
        });
        
        base.OnModelCreating(modelBuilder);
    }
}