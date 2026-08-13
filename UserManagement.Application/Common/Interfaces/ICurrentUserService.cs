namespace UserManagement.Application.Common.Interfaces;

public record IamClaims(
    string PegawaiId,
    string Nama,
    string Nip9,
    string Nip18,
    string Pangkat,
    List<JabatanPegawai> JabatanPegawais
    );

public record JabatanPegawai(
    string Jabatan_name,
    string Kantor_name,
    string Unit_name,
    string KantorId,
    string UnitId,
    List<string> Roles);

public interface ICurrentUserService
{
    string? UserId { get; }
    string? UserName { get; }
    IamClaims? Claims { get; }
    List<string>? Roles { get; }
    bool IsInRole(string role);
}