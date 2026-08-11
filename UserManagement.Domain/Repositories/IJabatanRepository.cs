using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Repositories;

public interface IJabatanRepository
{
    Task<IEnumerable<Jabatan>> GetAllAsync(CancellationToken cancellationToken);
    Task<Jabatan?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(Jabatan jabatan, CancellationToken cancellationToken);
    Task UpdateAsync(Jabatan jabatan, CancellationToken cancellationToken);
    Task DeleteAsync(Jabatan jabatan, CancellationToken cancellationToken);
}