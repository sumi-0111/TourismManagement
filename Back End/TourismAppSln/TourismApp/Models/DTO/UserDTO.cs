namespace TourismApp.Models.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string? Token { get; set; }

        public string? Role { get; set; }
        public string? PasswordClear { get; set; }

    }
}
