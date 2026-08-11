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

    public Task<IEnumerable<Jabatan>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Jabatan?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Jabatan jabatan, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
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