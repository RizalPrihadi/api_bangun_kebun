using api_bangun_kebun.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace api_bangun_kebun.Controllers
{
    [ApiController]
    [Route("bangunKebun/[controller]")]
    public class OtherController : ControllerBase
    {
        public readonly string _constr;
        public OtherController(IConfiguration configuration)
        {
            _constr = configuration.GetConnectionString("koneksi");
        }

        [HttpGet("DataJenisKonten")]
        public IActionResult getDataJenisKonten()
        {
            OtherContext otherContext = new OtherContext(this._constr);

            List<dynamic> data = otherContext.getDataJenisKonten();

            if(data.Count > 0)
            {
                return Ok(data);
            }

            return BadRequest();
        }
    }
}
