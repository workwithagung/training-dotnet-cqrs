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

    public async Task<IEnumerable<Pegawai>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Pegawais.ToListAsync(cancellationToken);
    }

    public async Task<Pegawai?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Pegawais.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task AddAsync(Pegawai pegawai, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(pegawai, cancellationToken);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Pegawai pegawai, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Pegawai pegawai, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}