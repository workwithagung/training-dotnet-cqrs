using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Domain.Entities;

public class Pegawai
{
    public Guid Id { get; set; }
    public string Nip { get; set; }
    public string Nama { get; set; }
    public decimal Tunjangan { get; set; }
    public Jabatan Jabatan { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
}