using api_bangun_kebun.Contexts;
using api_bangun_kebun.Models;
using Microsoft.AspNetCore.Mvc;

namespace api_bangun_kebun.Controllers
{
    [ApiController]
    [Route("bangunKebun/[controller]")]
    public class AuthController : ControllerBase
    {
        public readonly string __constr;
        private readonly IConfiguration __config;
        public AuthController(IConfiguration configuration)
        {
            __config = configuration;
            __constr = __config.GetConnectionString("koneksi");
        }

        [HttpPost("login")]
        public IActionResult login(Login data)
        {
            try
            {
                AuthContext penggunaContext = new AuthContext(this.__constr);
                bool penggunaExist = penggunaContext.checkLogin(data.email, data.password);

                if (!penggunaExist)
                {
                    return Unauthorized(new { message = "Email atau password salah!" });
                }

                List<Pengguna> user = penggunaContext.getDataLogin(data.email);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Terjadi kesalahan saat proses login.",
                    error = ex.Message
                });
            }
        }


        [HttpPost("registration")]
        public IActionResult registration(Pengguna data)
        {
            try
            {
                AuthContext penggunaContext = new AuthContext(this.__constr);
                bool result = penggunaContext.registrasiAkun(data);

                if (result)
                {
                    return Ok(new { message = "Berhasil registrasi akun" });
                }

                return StatusCode(500, new { message = "Gagal registrasi akun! (Tidak bisa insert data)" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Gagal registrasi akun!",
                    error = ex.Message
                });
            }
        }



        [HttpPatch("updateProfile")]
        public IActionResult updateProfile(Pengguna data)
        {
            try
            {
                AuthContext penggunaContext = new AuthContext(this.__constr);
                bool update = penggunaContext.updateProfile(data);

                if (update)
                {
                    return Ok(new { message = "Berhasil update profile" });
                }

                return StatusCode(500, new { message = "Gagal update profile!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Terjadi kesalahan saat update profile.",
                    error = ex.Message
                });
            }
        }


        [HttpPatch("updatePassword")]
        public IActionResult updatePassword(string newPassword, string oldPassword, int id_user)
        {
            try
            {
                AuthContext penggunaContext = new AuthContext(this.__constr);
                bool checkPassword = penggunaContext.checkPassword(oldPassword, id_user);

                if (checkPassword == false)
                {
                    return StatusCode(500, new { message = "Password lama salah!"});
                }

                bool update = penggunaContext.updatePassword(newPassword, id_user);

                if (update)
                {
                    return Ok(new { message = "Berhasil update password" });
                }

                return StatusCode(500, new { message = "Gagal update password!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Terjadi kesalahan saat update password.",
                    error = ex.Message
                });
            }
        }

    }
}
