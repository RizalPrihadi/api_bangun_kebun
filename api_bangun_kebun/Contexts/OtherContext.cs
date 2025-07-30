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
                db.closeConnection();
            }catch(Exception ex)
            {
                _errMsg = ex.Message;
            }

            return data;
        }
    }
}
