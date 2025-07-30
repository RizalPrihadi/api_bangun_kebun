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

        [HttpGet("{nomorTelepon}")]
        public IActionResult cariNomorTelepon(string nomorTelepon)
        {
            PenggunaContext penggunaContext = new PenggunaContext(this.__constr);
            bool penggunaExist = penggunaContext.cariNomorTelepon(nomorTelepon);

            if (!penggunaExist)
            {
                return StatusCode(500, new { message = "Tidak menemukan pengguna dengan nomor telepon itu!" });
            }

            return Ok(new { message = "Pengguna dengan nomor telepon itu ditemukan!" });

        }

        [HttpPost("login")]
        public IActionResult login(Login login)
        {
            try
            {
                PenggunaContext penggunaContext = new PenggunaContext(this.__constr);
                bool penggunaExist = penggunaContext.checkLogin(login.email, login.password);

                if (!penggunaExist)
                {
                    return Unauthorized(new { message = "Email atau password salah!" });
                }

                List<Pengguna> user = penggunaContext.getDataLogin(login.email, login.password);
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
        public IActionResult registration(RegistrasiPengguna data)
        {
            try
            {
                PenggunaContext penggunaContext = new PenggunaContext(this.__constr);
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
                PenggunaContext penggunaContext = new PenggunaContext(this.__constr);
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
        public IActionResult updatePassword(string password, int id)
        {
            try
            {
                PenggunaContext penggunaContext = new PenggunaContext(this.__constr);
                bool update = penggunaContext.updatePassword(password, id);

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
