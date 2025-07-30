using Microsoft.AspNetCore.Mvc;

namespace api_bangun_kebun.Controllers
{
    [ApiController]
    [Route("bangunKebun/[controller]")]
    public class ProductController : ControllerBase
    {
        public readonly string __constr;
        private readonly IConfiguration __config;
        public ProductController(IConfiguration configuration)
        {
            __config = configuration;
            __constr = __config.GetConnectionString("koneksi");
        }
    }
}
