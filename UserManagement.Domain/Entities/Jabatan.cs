namespace UserManagement.Domain.Entities;

public class Jabatan
{
    public Guid Id { get; set; }
    public string Nama { get; set; }
    public int Level { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
}