using api_bangun_kebun.Models;
using api_bangun_kebun.Helpers;
using Npgsql;

namespace api_bangun_kebun.Contexts
{
    public class ContentContext
    {
        private string _constr;
        private string _errMsg;

        public ContentContext(string conn) {
            _constr = conn;
        }

        public bool createContent(Content content)
        {
            SqlDbHelper db = new SqlDbHelper(this._constr);

            string query = @"insert into konten(pengguna_id_user, gambar_video, isi_deskripsi, timestamp, jenis_konten_id_jenis_konten, like) 
                            values(@id_pengguna, @gambar_video, @isi_deskripsi, @timestamp, @id_jenis_konten, 0)";

            try
            {
                NpgsqlCommand cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id_pengguna", content.id_user!);
                cmd.Parameters.AddWithValue("@gambar_video", content.gambar_video!);
                cmd.Parameters.AddWithValue("@isi_deskripsi", content.isi_deskripsi!);
                cmd.Parameters.AddWithValue("@timestamp", content.timestamp!);
                cmd.Parameters.AddWithValue("@id_jenis_konten", content.id_jenis_konten!);

                int rowsAffected = cmd.ExecuteNonQuery();

                cmd.Dispose();
                db.closeConnection();

                return rowsAffected > 0;
            }
            catch(Exception ex)
            {
                _errMsg = ex.Message;
                return false;
            }
        }


    }
}
