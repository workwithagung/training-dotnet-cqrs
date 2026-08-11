using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Repositories;

public interface IPegawaiRepository
{
    Task<IEnumerable<Pegawai>> GetAllAsync(CancellationToken cancellationToken);
    Task<Pegawai?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(Pegawai pegawai, CancellationToken cancellationToken);
    Task UpdateAsync(Pegawai pegawai, CancellationToken cancellationToken);
    Task DeleteAsync(Pegawai pegawai, CancellationToken cancellationToken);
}
