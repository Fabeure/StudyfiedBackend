using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace StudyfiedBackend.Models
{
    [CollectionName("Users")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
        public string Name { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public List<int> Favorites { get; set; } = new List<int>();

        public string ProfilePictureBase64 { get; set; } = string.Empty;
    }
}
