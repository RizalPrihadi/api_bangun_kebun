using api_bangun_kebun.Contexts;
using api_bangun_kebun.Models;
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

        [HttpGet("dataProduct")]
        public IActionResult getDataProduct()
        {
            ProductContext productContext = new ProductContext(this.__constr);

            List<Product> dataProduct = productContext.getDataProduct();

            if (dataProduct.Count > 0)
            {
                return Ok(dataProduct);
            }

            return BadRequest();
        }
    }
}
