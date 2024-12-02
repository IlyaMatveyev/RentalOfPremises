using System.ComponentModel.DataAnnotations;

namespace RentalOfPremises.Application.DTOs
{
    public class UserRegisterRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
