using System.Data.SqlClient;
using System.Data;

namespace RussianLoto
{
    internal class Database
    {

        SqlConnection sql_connection = new SqlConnection(@"Data Source=KAHOOSO\SQLEXPRESS;Initial Catalog=PlayersRussianLoto;Integrated Security=true;");

        public void open_connection()
        {
            if (sql_connection.State == ConnectionState.Closed)
            {
                sql_connection.Open();
            }
        }

        public void close_connection()
        {
            if (sql_connection.State == ConnectionState.Open)
            {
                sql_connection.Close();
            }
        }

        public SqlConnection get_connection()
        {
            return sql_connection;
        }
    }
}
