using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Repositories;

namespace UserManagement.Infrastructure.Persistence;

public class JabatanRepository: IJabatanRepository
{
    private readonly ApplicationDbContext _dbContext;

    public JabatanRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<(List<Jabatan>, int TotalCount)> GetAllAsync(string keyword, int page, int size, CancellationToken cancellationToken)
    {
        var query = _dbContext.Jabatans.AsNoTracking();

        if (!string.IsNullOrEmpty(keyword))
        {
            query = query.Where(j => j.Nama.ToLower().Contains(keyword.ToLower()));
        }
        
        var totalCount = await query.CountAsync(cancellationToken);
        
        var data = query
            .OrderByDescending(j => j.DateCreated)
            .Skip((page - 1) * size)
            .Take(size)
            .ToList();
        
        return (data, totalCount);
    }

    public async Task<Jabatan?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Jabatans.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(Jabatan jabatan, CancellationToken cancellationToken)
    {
        await _dbContext.Jabatans.AddAsync(jabatan);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateAsync(Jabatan jabatan, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Jabatan jabatan, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}