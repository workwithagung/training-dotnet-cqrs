using UserManagement.Application.Responses;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Extensions;

public static class PegawaiExtension
{
    public static PegawaiResponse ToResponse(this Pegawai pegawai)
    {
        return new PegawaiResponse(
            pegawai.Id,
            pegawai.Nip,
            pegawai.Nama,
            pegawai.Tunjangan,
            new JabatanResponse(pegawai.Jabatan.Id, pegawai.Jabatan.Nama, pegawai.Jabatan.Level)
        );
    }
}