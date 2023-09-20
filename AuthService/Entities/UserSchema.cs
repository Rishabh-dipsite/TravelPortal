using AuthService.Model;

namespace AuthService.Entities
{
    public class UserSchema: Users
    {
        public string Role { get; set; }
    }
}
