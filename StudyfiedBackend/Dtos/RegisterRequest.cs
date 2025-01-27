using System.ComponentModel.DataAnnotations;

namespace StudyfiedBackend.Dtos
{
    public class RegisterRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
