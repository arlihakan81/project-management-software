using System.ComponentModel.DataAnnotations;

namespace PMS.Application.Requests
{
    public class RegisterRequest : LoginRequest
    {
        public string Name { get; set; }

        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
