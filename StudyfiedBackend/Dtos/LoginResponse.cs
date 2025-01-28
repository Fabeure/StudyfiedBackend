namespace StudyfiedBackend.Dtos
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string AccessToken { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Name {  get; set; } = string.Empty;
        public string Surname {  get; set; } = string.Empty;
        public List<int> Favorites { get; set; } = new List<int>();
        public string ProfilePictureBase64 { get; set; } = string.Empty;
    }

}
