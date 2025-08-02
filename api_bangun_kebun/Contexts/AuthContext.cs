using api_bangun_kebun.Helpers;
using api_bangun_kebun.Models;
using Npgsql;
using BCrypt.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace api_bangun_kebun.Contexts
{
    public class AuthContext
    {
        private string _constr;

        public AuthContext(string conn)
        {
            _constr = conn;
        }
        public bool registrasiAkun(Pengguna dataRegis)
        {
            List<Pengguna> dataRegistrasi = new List<Pengguna>();
            SqlDbHelper db = new SqlDbHelper(this._constr);

            string query = @"INSERT INTO pengguna(nama_lengkap, no_telepon, username, email, password, kecamatan_id_kecamatan) 
                            VALUES(@nama_lengkap, @no_telepon, @username, @email, @password, @id_kecamatan)";

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@nama_lengkap", dataRegis.nama_lengkap);
                cmd.Parameters.AddWithValue("@no_telepon", dataRegis.no_telepon);
                cmd.Parameters.AddWithValue("@username", dataRegis.username);
                cmd.Parameters.AddWithValue("@email", dataRegis.email);
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dataRegis.password);
                cmd.Parameters.AddWithValue("@password", hashedPassword);
                cmd.Parameters.AddWithValue("@id_kecamatan", dataRegis.id_kecamatan);

                int rowsAffected = cmd.ExecuteNonQuery();

                cmd.Dispose();
                db.CloseConnection();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Registrasi gagal: " + ex.Message);
            }
        }
        public bool checkLogin(string email, string password)
        {
            string query = "SELECT password FROM pengguna WHERE email = @email";
            SqlDbHelper db = new SqlDbHelper(this._constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@email", email);

                string hashedPassword = cmd.ExecuteScalar()?.ToString();

                cmd.Dispose();
                db.CloseConnection();

                if (string.IsNullOrEmpty(hashedPassword))
                {
                    return false;
                }

                return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            }
            catch (Exception ex)
            {
                throw new Exception("Login gagal: " + ex.Message);
            }
        }


        public List<Pengguna> getDataLogin(string email)
        {
            List<Pengguna> pengguna = new List<Pengguna>();

            string query = @"SELECT * FROM pengguna p 
                            JOIN kecamatan ke ON p.kecamatan_id_kecamatan = ke.id_kecamatan
                            WHERE email = @email";

            SqlDbHelper db = new SqlDbHelper(this._constr);

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@email", email);

                NpgsqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pengguna.Add(new Pengguna()
                    {
                        id_user = int.Parse(reader["id_user"].ToString()),
                        nama_lengkap = reader["nama_lengkap"].ToString(),
                        no_telepon = reader["no_telepon"].ToString(),
                        username = reader["username"].ToString(),
                        email = reader["email"].ToString(),
                        password = reader["password"].ToString(),
                        id_kecamatan = int.Parse(reader["kecamatan_id_kecamatan"].ToString()),
                    });
                }

                cmd.Dispose();
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                throw new Exception("login gagal: " + ex.Message); 
            }

            return pengguna;
        }

        public bool updateProfile(Pengguna data)
        {
            SqlDbHelper db = new SqlDbHelper(this._constr);

            string query = @"UPDATE pengguna SET 
                                    nama_lengkap = @nama_lengkap, 
                                    username = @username, 
                                    no_telepon = @no_telepon, 
                                    email = @email, 
                                    kecamatan_id_kecamatan = @id_kecamatan
                            WHERE id_user = @id_user";

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id_user", data.id_user);
                cmd.Parameters.AddWithValue("@nama_lengkap", data.nama_lengkap);
                cmd.Parameters.AddWithValue("@username", data.username);
                cmd.Parameters.AddWithValue("@no_telepon", data.no_telepon);
                cmd.Parameters.AddWithValue("@email", data.email);
                cmd.Parameters.AddWithValue("@id_kecamatan", data.id_kecamatan);

                int rowsAffected = cmd.ExecuteNonQuery();

                cmd.Dispose();
                db.CloseConnection();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Update gagal: " + ex.Message);
            }
        }

        public bool checkPassword(string password, int id_user)
        {
            bool isTrue = false;
            string query = @"SELECT password FROM pengguna WHERE id_user = @id_user";
            SqlDbHelper db = new SqlDbHelper(this._constr);
            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id_user", id_user);

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    string hashedPasswordFromDB = result.ToString();
                    isTrue = BCrypt.Net.BCrypt.Verify(password, hashedPasswordFromDB);
                }

                cmd.Dispose();
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                throw new Exception("Pengecekan password gagal: " + ex.Message);
            }
            return isTrue;
        }

        public bool updatePassword(string password, int id_user)
        {
            SqlDbHelper db = new SqlDbHelper(this._constr);

            string query = @"UPDATE pengguna SET 
                                    password = @password
                            where id_user = @id_user";

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id_user", id_user);
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                cmd.Parameters.AddWithValue("@password", hashedPassword);

                int rowsAffected = cmd.ExecuteNonQuery();

                cmd.Dispose();
                db.CloseConnection();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Update gagal: " + ex.Message);
            }
        }
    }
}
