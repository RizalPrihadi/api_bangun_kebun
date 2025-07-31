using api_bangun_kebun.Helpers;
using Npgsql;

namespace api_bangun_kebun.Contexts
{
    public class OtherContext
    {
        private string _constr;
        private string _errMsg;

        public OtherContext(string conn)
        {
            _constr = conn;
        }

        public List<dynamic> getDataJenisKonten()
        {
            List<dynamic> data = new List<dynamic>();

            string query = "select * from jenis_konten";

            SqlDbHelper db = new SqlDbHelper(this._constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    data.Add(new
                    {
                        id_jenis_konten = int.Parse(reader["id_jenis_konten"].ToString()),
                        jenis_konten = reader["jenis_konten"].ToString()
                    });
                }

                cmd.Dispose();
                db.CloseConnection();
            }catch(Exception ex)
            {
                _errMsg = ex.Message;
            }

            return data;
        }

        public List<dynamic> getDataProvinsi()
        {
            List<dynamic> data = new List<dynamic>();

            string query = "select * from provinsi";

            SqlDbHelper db = new SqlDbHelper(this._constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    data.Add(new
                    {
                        id_provinsi = int.Parse(reader["id_provinsi"].ToString()),
                        nama_provinsi = reader["nama_provinsi"].ToString()
                    });
                }

                cmd.Dispose();
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                _errMsg = ex.Message;
            }

            return data;
        }

        public List<dynamic> getDataKabupatenByProvinsi(int id_provinsi)
        {
            List<dynamic> data = new List<dynamic>();

            string query = "select * from kabupaten where provinsi_id_provinsi = @id_provinsi";

            SqlDbHelper db = new SqlDbHelper(this._constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id_provinsi", id_provinsi);

                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    data.Add(new
                    {
                        id_kabupaten = int.Parse(reader["id_kabupaten"].ToString()),
                        id_provinsi = int.Parse(reader["provinsi_id_provinsi"].ToString()),
                        nama_kabupaten = reader["nama_kabupaten"].ToString()
                    });
                }

                cmd.Dispose();
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                _errMsg = ex.Message;
            }

            return data;
        }

        public List<dynamic> getDataKecamatanByKabupaten(int id_kabupaten)
        {
            List<dynamic> data = new List<dynamic>();

            string query = "select * from kecamatan where kabupaten_id_kabupaten = @id_kabupaten";

            SqlDbHelper db = new SqlDbHelper(this._constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id_kabupaten", id_kabupaten);
                NpgsqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    data.Add(new
                    {
                        id_kecamatan = int.Parse(reader["id_kecamatan"].ToString()),
                        id_kabupaten = int.Parse(reader["kabupaten_id_kabupaten"].ToString()),
                        nama_kecamatan = reader["nama_kecamatan"].ToString()
                    });
                }

                cmd.Dispose();
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                _errMsg = ex.Message;
            }

            return data;
        }
    }
}
