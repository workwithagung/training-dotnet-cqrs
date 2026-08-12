using UserManagement.Application.Responses;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Extensions;

public static class JabatanExtensions
{
    public static JabatanResponse ToResponse(this Jabatan jabatan)
    {
        return new JabatanResponse(jabatan.Id, jabatan.Nama, jabatan.Level);
    }
}