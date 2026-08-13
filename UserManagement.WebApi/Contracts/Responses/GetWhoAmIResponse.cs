using UserManagement.Application.Common.Interfaces;

namespace UserManagement.WebApi.Contracts.Responses;

public record GetWhoAmIResponse(
    string? Id,
    string? UserName,
    List<string>? Roles,
    IamClaims? Profile
    );