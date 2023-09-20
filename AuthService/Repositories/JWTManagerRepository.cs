using AuthService.Entities;
using AuthService.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService
{
    internal class JWTManagerRepository : IJWTManagerRepository
    {
        Dictionary<string, UserSchema> UsersRecords = new Dictionary<string, UserSchema>
        {
            { "user1",new UserSchema{Name= "user1", Password="password1", Role= "Consumer"} },
            { "user2",new UserSchema{Name= "user2", Password="password2", Role= "Provider"}},
            { "user3",new UserSchema{Name= "user3", Password="password3", Role= "Admin"}},
        };

        private readonly IConfiguration iconfiguration;
        public JWTManagerRepository(IConfiguration iconfiguration)
        {
            this.iconfiguration = iconfiguration;
        }
        public Tokens? Authenticate(Users users)
        {
            UserSchema user;
            if (UsersRecords.ContainsKey(users.Name) && UsersRecords[users.Name].Password == users.Password)
            {
                user = UsersRecords[users.Name];
            } else
            {
                return null;
            }

            // Else we generate JSON Web Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(this.iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                 new Claim("UserID", users.Name),
                 new Claim("Role", user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tokens { Token = tokenHandler.WriteToken(token) };

        }
    }
}