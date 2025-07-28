//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace api_bangun_kebun.Helpers
//{
//    public class JwtHelper
//    {
//        private readonly IConfiguration _config;
//        public JwtHelper(IConfiguration config)
//        {
//            _config = config;
//        }

//        public string GenerateToken(Pengguna pengguna)
//        {
//            var tokenHandler = new JwtSecurityTokenHandler();
//            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(new[]
//                {
//                    new Claim("id_pengguna", pengguna.id_pengguna.ToString()),
//                    new Claim(ClaimTypes.Email, pengguna.email),
//                    new Claim(ClaimTypes.Role, pengguna.nama_peran)
//                }),
//                Expires = DateTime.UtcNow.AddHours(1),
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//            };
//            var token = tokenHandler.CreateToken(tokenDescriptor);
//            return tokenHandler.WriteToken(token);
//        }
//    }
//}
