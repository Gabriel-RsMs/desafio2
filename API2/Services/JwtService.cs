using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace UserClass.Security{
    public static class TokenGen{
        
        public static string secret = "cf3a64db9aa59f8d603d76d346a05aefefc802f14e098b7a1f86a971c64bd826";
// ver de arrumar essa graca aq e como configurar os acessos as rotas, parece ser um braco na roda :3
        public static string GenerateToken(User user){
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var balanceClaim = new Claim("BALANCE", user.BALANCE.ToString());
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Role, user.Email.ToString()),
                    balanceClaim,
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}