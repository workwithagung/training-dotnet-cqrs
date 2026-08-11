using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Repositories;

public interface IJabatanRepository
{
    Task<(List<Jabatan>, int TotalCount)> GetAllAsync(string keyword, int page, int size, CancellationToken cancellationToken);
    Task<Jabatan?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(Jabatan jabatan, CancellationToken cancellationToken);
    Task UpdateAsync(Jabatan jabatan, CancellationToken cancellationToken);
    Task DeleteAsync(Jabatan jabatan, CancellationToken cancellationToken);
}