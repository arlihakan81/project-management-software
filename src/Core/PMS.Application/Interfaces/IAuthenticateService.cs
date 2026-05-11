using PMS.Application.Requests;
using PMS.Domain.Entities;

namespace PMS.Application.Interfaces
{
    public interface IAuthenticateService
    {
        Task<string> AuthenticateAsync(string email, string password);
        Task RegisterAsync(RegisterRequest request);
        Task<User?> GetByEmailAsync(string email);
    }
}
