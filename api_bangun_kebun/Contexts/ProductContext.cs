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
                        id_user = int.Parse(reader["pengguna_id_user"].ToString()),
                        id_produk = int.Parse(reader["id_produk"].ToString()),
                        nama_produk = reader["nama_produk"].ToString(),
                        gambar_produk = reader["gambar_produk"].ToString(),
                        harga = double.Parse(reader["harga"].ToString()),
                        stok = int.Parse(reader["stok"].ToString()),
                        deskripsi = reader["deskripsi"].ToString(),
                        lokasi = reader["lokasi"].ToString(),
                        timestamp = DateTime.Parse(reader["timestamp"].ToString()),
                        id_jenis_produk = int.Parse(reader["jenis_produk_id_jenis_produk"].ToString()),
                        id_status_ketahanan = int.Parse(reader["status_ketahanan_id_status_ketahanan"].ToString())
                    });
                }

                cmd.Dispose();
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                throw new Exception("Get product gagal: " + ex.Message);
            }

            return product;
        }

        public List<Product> GetDataProductById(int id_produk)
        {
            List<Product> dataProduct = new List<Product>();

            string query = @"SELECT * FROM produks WHERE id_produk = @id_produk";

            SqlDbHelper db = new SqlDbHelper(this._constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id_produk", id_produk);

                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dataProduct.Add(new Product
                    {
                        id_user = int.Parse(reader["pengguna_id_user"].ToString()),
                        id_produk = int.Parse(reader["id_produk"].ToString()),
                        nama_produk = reader["nama_produk"].ToString(),
                        gambar_produk = reader["gambar_produk"].ToString(),
                        harga = double.Parse(reader["harga"].ToString()),
                        stok = int.Parse(reader["stok"].ToString()),
                        deskripsi = reader["deskripsi"].ToString(),
                        lokasi = reader["lokasi"].ToString(),
                        timestamp = DateTime.Parse(reader["timestamp"].ToString()),
                        id_jenis_produk = int.Parse(reader["jenis_produk_id_jenis_produk"].ToString()),
                        id_status_ketahanan = int.Parse(reader["status_ketahanan_id_status_ketahanan"].ToString())
                    });
                }

                cmd.Dispose();
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                throw new Exception("Get product gagal: " + ex.Message);
            }

            return dataProduct;
        }

        public List<Product> GetDataProductByUser(string nama_lengkap)
        {
            List<Product> dataProduct = new List<Product>();

            string query = @"SELECT * FROM produks pr
                            JOIN pengguna pe ON pr.pengguna_id_user = pe.id_user
                            WHERE nama_lengkap = @nama_lengkap";

            SqlDbHelper db = new SqlDbHelper(this._constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@nama_lengkap", nama_lengkap);

                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dataProduct.Add(new Product
                    {
                        id_user = int.Parse(reader["pengguna_id_user"].ToString()),
                        id_produk = int.Parse(reader["id_produk"].ToString()),
                        nama_produk = reader["nama_produk"].ToString(),
                        gambar_produk = reader["gambar_produk"].ToString(),
                        harga = double.Parse(reader["harga"].ToString()),
                        stok = int.Parse(reader["stok"].ToString()),
                        deskripsi = reader["deskripsi"].ToString(),
                        lokasi = reader["lokasi"].ToString(),
                        timestamp = DateTime.Parse(reader["timestamp"].ToString()),
                        id_jenis_produk = int.Parse(reader["jenis_produk_id_jenis_produk"].ToString()),
                        id_status_ketahanan = int.Parse(reader["status_ketahanan_id_status_ketahanan"].ToString())
                    });
                }

                cmd.Dispose();
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                throw new Exception("Get product gagal: " + ex.Message);
            }

            return dataProduct;
        }

        public List<Product> GetDataProductByJenis(string nama_jenis_product)
        {
            List<Product> dataProduct = new List<Product>();

            string query = @"SELECT * FROM produks pr
                            JOIN jenis_produk jp ON pr.jenis_produk_id_jenis_produk = jp.id_jenis_produk
                            WHERE nama_jenis_product = @nama_jenis_product";

            SqlDbHelper db = new SqlDbHelper(this._constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@nama_jenis_product", nama_jenis_product);

                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dataProduct.Add(new Product
                    {
                        id_user = int.Parse(reader["pengguna_id_user"].ToString()),
                        id_produk = int.Parse(reader["id_produk"].ToString()),
                        nama_produk = reader["nama_produk"].ToString(),
                        gambar_produk = reader["gambar_produk"].ToString(),
                        harga = double.Parse(reader["harga"].ToString()),
                        stok = int.Parse(reader["stok"].ToString()),
                        deskripsi = reader["deskripsi"].ToString(),
                        lokasi = reader["lokasi"].ToString(),
                        timestamp = DateTime.Parse(reader["timestamp"].ToString()),
                        id_jenis_produk = int.Parse(reader["jenis_produk_id_jenis_produk"].ToString()),
                        id_status_ketahanan = int.Parse(reader["status_ketahanan_id_status_ketahanan"].ToString())
                    });
                }

                cmd.Dispose();
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                throw new Exception("Get product gagal: " + ex.Message);
            }

            return dataProduct;
        }

        public bool CreateProduct(Product product)
        {
            SqlDbHelper db = new SqlDbHelper(this._constr);

            string query = @"INSERT INTO produks (pengguna_id_user, nama_produk, gambar_produk, harga, stok, deskripsi, lokasi, timestamp, jenis_produk_id_jenis_produk, status_ketahanan_id_status_ketahanan)
                            VALUES(@id_user, @nama_produk, @gambar_produk, @harga, @stok, @deskripsi, @lokasi, @timestamp, @id_jenis_produk, @id_status_ketahanan)";
            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id_user", product.id_user);
                cmd.Parameters.AddWithValue("@nama_produk", product.nama_produk);
                cmd.Parameters.AddWithValue("@gambar_produk", product.gambar_produk);
                cmd.Parameters.AddWithValue("@harga", product.harga);
                cmd.Parameters.AddWithValue("@stok", product.stok);
                cmd.Parameters.AddWithValue("@deskripsi", product.deskripsi);
                cmd.Parameters.AddWithValue("@lokasi", product.lokasi);
                cmd.Parameters.AddWithValue("@timestamp", product.timestamp);
                cmd.Parameters.AddWithValue("@id_jenis_produk", product.id_jenis_produk);
                cmd.Parameters.AddWithValue("@id_status_ketahanan", product.id_status_ketahanan);

                int rowsAffected = cmd.ExecuteNonQuery();

                cmd.Dispose();
                db.CloseConnection();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Create product gagal: " + ex.Message);
            }
        }

        public bool UpdateProduct(int id_produk, Product product)
        {
            SqlDbHelper db = new SqlDbHelper(this._constr);

            string query = @"UPDATE produks 
                            SET nama_produk = @nama_produk, 
                                gambar_produk = @gambar_produk, 
                                harga = @harga,
                                stok = @stok,
                                deskripsi = @deskripsi,
                                lokasi = @lokasi,
                                timestamp = @timestamp, 
                                jenis_produk_id_jenis_produk = @id_jenis_produk, 
                                status_ketahanan_id_status_ketahanan = @id_status_ketahanan
                            WHERE id_produk = @id_produk";
            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id_produk", product.id_produk);
                cmd.Parameters.AddWithValue("@nama_produk", product.nama_produk);
                cmd.Parameters.AddWithValue("@gambar_produk", product.gambar_produk);
                cmd.Parameters.AddWithValue("@harga", product.harga);
                cmd.Parameters.AddWithValue("@stok", product.stok);
                cmd.Parameters.AddWithValue("@deskripsi", product.deskripsi);
                cmd.Parameters.AddWithValue("@lokasi", product.lokasi);
                cmd.Parameters.AddWithValue("@timestamp", product.timestamp);
                cmd.Parameters.AddWithValue("@id_jenis_produk", product.id_jenis_produk);
                cmd.Parameters.AddWithValue("@id_status_ketahanan", product.id_status_ketahanan);

                int rowsAffected = cmd.ExecuteNonQuery();

                cmd.Dispose();
                db.CloseConnection();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Update product gagal: " + ex.Message);
            }
        }

        public bool DeleteProduct(int id_produk)
        {
            SqlDbHelper db = new SqlDbHelper(this._constr);

            string query = @"DELETE FROM produks WHERE id_produk = @id_produk";
            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id_produk", id_produk);

                int rowsAffected = cmd.ExecuteNonQuery();

                cmd.Dispose();
                db.CloseConnection();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Delete product gagal: " + ex.Message);
            }
        }
    }
}
