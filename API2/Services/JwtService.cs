using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiAuth;
using Microsoft.IdentityModel.Tokens;

namespace UserClass.Security
{
    public static class TokenGen{
        
// ver de arrumar essa graca aq e como configurar os acessos as rotas, parece ser um braco na roda :3
        public static string GenerateToken(User user){
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.secret);
            var balanceClaim = new Claim("BALANCE", user.BALANCE.ToString());
            var IDClaim = new Claim("ID", user.ID.ToString());
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(new Claim[]{
                    new(ClaimTypes.Role, user.Email.ToString()),
                    balanceClaim,
                    IDClaim
                }),
                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}