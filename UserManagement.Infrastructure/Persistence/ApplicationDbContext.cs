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
        });
        
        base.OnModelCreating(modelBuilder);
    }
}