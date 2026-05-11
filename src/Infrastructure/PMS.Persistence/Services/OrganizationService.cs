using Microsoft.AspNetCore.Http;
using PMS.Application.Interfaces;
using System.Security.Claims;

namespace PMS.Persistence.Services
{
    public class OrganizationService(IHttpContextAccessor httpContextAccessor) : IOrganizationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public Guid GetAuthenticatedUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return userId;
            }
            throw new Exception("User ID claim not found or invalid.");
        }

        public Guid GetCurrentOrganizationId()
        {
            var organizationIdClaim = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "OrganizationId");
            if (organizationIdClaim != null && Guid.TryParse(organizationIdClaim.Value, out var organizationId))
            {
                return organizationId;
            }
            throw new Exception("Organization ID claim not found or invalid.");
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
        }
    }
}
