using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class ConexionDB
    {
        private static string Conn = @"Data Source=.;Initial Catalog=PREGUNTAS_RESPUESTAS;Integrated Security=False;Persist Security Info=True;User ID=sa; Password=holamundo;";

        public static IDbConnection Conexion()
        {
            return new SqlConnection(Conn);
        }
    }
}
