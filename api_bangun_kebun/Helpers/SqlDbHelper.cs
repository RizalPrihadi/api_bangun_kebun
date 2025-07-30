using Npgsql;
using System.Data;

namespace api_bangun_kebun.Helpers
{
    public class SqlDbHelper
    {
        private NpgsqlConnection connection;
        private string __constr;

        public SqlDbHelper(string pConstr)
        {
            __constr = pConstr;
            connection = new NpgsqlConnection();
            connection.ConnectionString = __constr;
        }

        public NpgsqlCommand GetNpgsqlCommand(string query)
        {
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            return cmd;
        }

        public void closeConnection()
        {
            connection.Close();
        }
    }
}
