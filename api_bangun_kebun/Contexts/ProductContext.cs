using api_bangun_kebun.Helpers;
using api_bangun_kebun.Models;
using Npgsql;

namespace api_bangun_kebun.Contexts
{
    public class ProductContext
    {
        private string _constr;

        public ProductContext(string conn)
        {
            _constr = conn;
        }

        public List<Product> getDataProduct()
        {
            List<Product> product = new List<Product>();

            string query = @"SELECT * FROM produks";

            SqlDbHelper db = new SqlDbHelper(this._constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    product.Add(new Product
                    {
                        id_user = int.Parse(reader["id_user"].ToString()),
                        id_product = int.Parse(reader["id_product"].ToString()),
                        nama_produk = reader["nama_produk"].ToString(),
                        gambar_produk = reader["gambar_produk"].ToString(),
                        harga = double.Parse(reader["harga"].ToString()),
                        timestamp = DateTime.Parse(reader["timestamp"].ToString()),
                        id_jenis_produk = int.Parse(reader["id_jenis_produk"].ToString()),
                        id_status_ketahanan = int.Parse(reader["id_status_ketahanan"].ToString())
                    });
                }

                cmd.Dispose();
                db.closeConnection();
            }
            catch (Exception ex)
            {
                throw new Exception("Get product gagal: " + ex.Message);
            }

            return product;
        }
    }
}
