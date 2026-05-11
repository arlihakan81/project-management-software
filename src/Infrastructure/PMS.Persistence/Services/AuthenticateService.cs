using PMS.Application.Interfaces;
using PMS.Application.Requests;
using PMS.Domain.Entities;
using PMS.Persistence.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace PMS.Persistence.Services
{
    public class AuthenticateService(AppDbContext context, ITokenService tokenService) : IAuthenticateService
    {
        private readonly AppDbContext context = context;
        private readonly ITokenService tokenService = tokenService;

        public async Task<string> AuthenticateAsync(string email, string password)
        {
            var user = await GetByEmailAsync(email);
            if (user is null)
            {
                return null!;
            }
            if (!user.IsEmailConfirmed && !user.IsDeleted)
            {
                throw new UnauthorizedAccessException("Email not confirmed");
            }
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, password)
                is PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }
            return tokenService.GenerateToken(user);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await context.Users.Include(u => u.Role).IgnoreQueryFilters().FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task RegisterAsync(RegisterRequest request)
        {
            var organization = new Organization
            {
                Name = request.Email.Split('@')[1],
                Domain = request.Email.Split('@')[1]
            };
            context.Organizations.Add(organization);
            context.SaveChanges();

            if(await GetByEmailAsync(request.Email) != null)
            {
                throw new Exception("Email already exists");
            }

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = new PasswordHasher<User>().HashPassword(null!, request.Password),
                RoleId = context.Roles.FirstOrDefault(_ => _.Name == "Admin")!.Id,
                OrganizationId = organization.Id
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

    }
}
