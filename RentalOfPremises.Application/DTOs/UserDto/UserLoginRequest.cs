using System.ComponentModel.DataAnnotations;

namespace RentalOfPremises.Application.DTOs.UserDto
{
    public class UserLoginRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
