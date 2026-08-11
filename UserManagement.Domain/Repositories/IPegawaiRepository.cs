using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Repositories;

public interface IPegawaiRepository
{
    Task<(List<Pegawai>, int TotalCount)> GetAllAsync(string keyword, int page, int size, CancellationToken cancellationToken);
    Task<Pegawai?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(Pegawai pegawai, CancellationToken cancellationToken);
    Task UpdateAsync(Pegawai pegawai, CancellationToken cancellationToken);
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}
