namespace UserManagement.Application.Responses;

public record PegawaiResponse(
    Guid Id, 
    string Nip, 
    string Nama, 
    decimal Tunjangan, 
    JabatanResponse Jabatan);