using System.Security.Claims;
using System.Text.Json;
using UserManagement.Application.Common.Interfaces;

namespace UserManagement.WebApi.Services;

public class CurrentUserServices: ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public CurrentUserServices(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value;
    public string? UserName => _httpContextAccessor.HttpContext?.User?.FindFirst("username")?.Value;

    public IamClaims? Claims
    {
        get
        {
            var claimsJson = _httpContextAccessor.HttpContext?.User?.FindFirstValue("pegawai");

            if (string.IsNullOrEmpty(claimsJson)) return null;

            try
            {
                return JsonSerializer.Deserialize<IamClaims>(claimsJson, _jsonSerializerOptions);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }

    public List<string>? Roles
    {
        get
        {
            var roles = _httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();
            
            return roles is {Count: > 0} ? roles : null;
        }
    }

    public bool IsInRole(string role)
    {
        return _httpContextAccessor.HttpContext?.User?.IsInRole(role) ?? false;
    }
}