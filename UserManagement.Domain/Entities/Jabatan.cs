namespace UserManagement.Domain.Entities;

public class Jabatan: BaseModel
{
    public required string Nama { get; set; }
    public required int Level { get; set; }
}