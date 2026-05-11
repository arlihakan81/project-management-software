using PMS.Domain.Entities;

namespace PMS.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
