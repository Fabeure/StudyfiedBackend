namespace StudyfiedBackend.Models
{
    public class UserToken
    {
        public string id { get; set; }
        public string email { get; set; }
        public DateTime expiration { get; set; }
        public bool IsExpired => this.expiration < DateTime.UtcNow;
        public UserToken(string id, string email, DateTime expiration)
        {
            this.id = id;
            this.email = email;
            this.expiration = expiration;
        }
        public void UpdateLastAccessed()
        {
            if ((this.expiration.AddMinutes(-10) < DateTime.UtcNow) )
            this.expiration = DateTime.UtcNow.AddMinutes(30);
        }

    }
}
