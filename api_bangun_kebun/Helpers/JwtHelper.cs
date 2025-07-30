//using api_bangun_kebun.Models;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace api_bangun_kebun.Helpers
//{
//    public class JwtHelper
//    {
//        private readonly IConfiguration _configuration;
//        public JwtHelper(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        public string generateTokenPegawai(Pengguna pengguna)
//        {
//            var tokenHandler = new JwtSecurityTokenHandler();
//            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(new[]
//                {
//                    new Claim(ClaimTypes.NameIdentifier, pengguna.id.ToString()),
//                    new Claim("Id", pengguna.id.ToString()),
//                    new Claim(ClaimTypes.Email, pengguna.email),
//                }),
//                Expires = DateTime.UtcNow.AddHours(1),
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//            };
//            var token = tokenHandler.CreateToken(tokenDescriptor);
//            return tokenHandler.WriteToken(token);
//        }
//    }
//}
