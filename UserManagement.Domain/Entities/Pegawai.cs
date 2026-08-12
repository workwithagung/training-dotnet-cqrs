using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Domain.Entities;

public class Pegawai: BaseModel
{
    public required string Nip { get; set; }
    public required string Nama { get; set; }
    public decimal Tunjangan { get; set; }
    public required Jabatan Jabatan { get; set; }

    public bool UpdateDetails(Jabatan jabatan, decimal tunjangan)
    {
        Jabatan = jabatan;
        Tunjangan = tunjangan;
        return true;
    }
}