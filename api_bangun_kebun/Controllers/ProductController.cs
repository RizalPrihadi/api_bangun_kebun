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
            try
            {
                ProductContext productContext = new ProductContext(this.__constr);

                List<Product> dataProduct = productContext.getDataProduct();

                if (dataProduct.Count > 0)
                {
                    return Ok(dataProduct);
                }

                return NotFound(new { message = "Tidak ada data produk yang ditemukan." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Terjadi kesalahan saat mengambil data produk.",
                    error = ex.Message
                });
            }
        }

        [HttpGet("dataProductById/{id_product}")]
        public IActionResult getDataProductById(int id_produk)
        {
            try
            {
                ProductContext productContext = new ProductContext(this.__constr);
                List<Product> dataProduct = productContext.GetDataProductById(id_produk);

                if (dataProduct.Count > 0)
                {
                    return Ok(dataProduct);
                }

                return BadRequest(new { message = "Data produk tidak ditemukan." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Terjadi kesalahan saat mengambil data produk.",
                    error = ex.Message
                });
            }
        }

        [HttpGet("dataProductByUser/{nama_lengkap}")]
        public IActionResult getDataProductByUser(string nama_lengkap)
        {
            try
            {
                ProductContext productContext = new ProductContext(this.__constr);

                List<Product> dataProduct = productContext.GetDataProductByUser(nama_lengkap);

                if (dataProduct.Count > 0)
                {
                    return Ok(dataProduct);
                }

                return BadRequest(new { message = "Data produk tidak ditemukan untuk pengguna tersebut." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Terjadi kesalahan saat mengambil data produk berdasarkan pengguna.",
                    error = ex.Message
                });
            }
        }

        [HttpGet("dataProductByJenis/{nama_jenis_product}")]
        public IActionResult getDataProductByJenis(string nama_jenis_product)
        {
            try
            {
                ProductContext productContext = new ProductContext(this.__constr);

                List<Product> dataProduct = productContext.GetDataProductByJenis(nama_jenis_product);

                if (dataProduct.Count > 0)
                {
                    return Ok(dataProduct);
                }

                return BadRequest(new { message = "Data produk tidak ditemukan untuk jenis tersebut." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Terjadi kesalahan saat mengambil data produk berdasarkan jenis.",
                    error = ex.Message
                });
            }
        }

        [HttpPost("createProduct")]
        public IActionResult createProduct(Product product)
        {
            try
            {
                ProductContext productContext = new ProductContext(this.__constr);

                bool result = productContext.CreateProduct(product);

                if (result)
                {
                    return Ok(new { message = "Berhasil menambahkan produk" });
                }

                return StatusCode(500, new { message = "Gagal menambahkan produk!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Terjadi kesalahan saat menambahkan produk.",
                    error = ex.Message
                });
            }
        }

        [HttpPut("updateProduct/{id_produk}")]
        public IActionResult updateProduct(int id_produk, Product product)
        {
            try
            {
                ProductContext productContext = new ProductContext(this.__constr);

                bool result = productContext.UpdateProduct(id_produk, product);

                if (result)
                {
                    return Ok(new { message = "Berhasil memperbarui produk" });
                }

                return StatusCode(500, new { message = "Gagal memperbarui produk!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Terjadi kesalahan saat memperbarui produk.",
                    error = ex.Message
                });
            }
        }

        [HttpDelete("deleteProduct/{id_produk}")]
        public IActionResult deleteProduct(int id_produk)
        {
            try
            {
                ProductContext productContext = new ProductContext(this.__constr);

                bool result = productContext.DeleteProduct(id_produk);

                if (result)
                {
                    return Ok(new { message = "Berhasil menghapus produk" });
                }

                return StatusCode(500, new { message = "Gagal menghapus produk!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Terjadi kesalahan saat menghapus produk.",
                    error = ex.Message
                });
            }
        }
    }
}
