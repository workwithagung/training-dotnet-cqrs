using UserManagement.Domain.Entities;
using UserManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace UserManagement.Infrastructure.Persistence;

public class PegawaiRepository: IPegawaiRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PegawaiRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<(List<Pegawai>, int TotalCount)> GetAllAsync(string keyword, int page, int size, CancellationToken cancellationToken)
    {
        var query = _dbContext.Pegawais.Include(p => p.Jabatan).AsNoTracking();

        if (!string.IsNullOrEmpty(keyword))
        {
            query = query.Where(p => p.Nama.ToLower().Contains(keyword.ToLower()));
        }
        
        var totalCount = await query.CountAsync(cancellationToken);
        
        var data = query
            .OrderByDescending(p => p.DateCreated)
            .Skip((page - 1) * size)
            .Take(size)
            .ToList();
        
        return (data, totalCount);
    }

    public async Task<Pegawai?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Pegawais.Include(p => p.Jabatan).FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task AddAsync(Pegawai pegawai, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(pegawai, cancellationToken);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Pegawai pegawai, CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _dbContext
            .Pegawais
            .Where(p => p.Id == id)
            .ExecuteUpdateAsync(
                s => s.SetProperty(p => p.DateDeleted, DateTime.Now), 
                cancellationToken);
    }
}