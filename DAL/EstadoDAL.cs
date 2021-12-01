using EN;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class EstadoDAL
    {

        public EstadoEN ConsultarPorId(long Id)
        {
            IDbConnection _Conn = ConexionDB.Conexion();
            _Conn.Open();
            SqlCommand _Command = new SqlCommand("CONSULTAR_ESTADO_POR_ID", _Conn as SqlConnection);
            _Command.CommandType = CommandType.StoredProcedure;
            _Command.Parameters.Add(new SqlParameter("@ID",Id));
            IDataReader _reader = _Command.ExecuteReader();
            EstadoEN estado = new EstadoEN();

            if (_reader.Read())
            {
                estado.Id = _reader.GetInt64(0);
                estado.Nom_Estado = _reader.GetString(1);
            }
            _Conn.Close();
            return estado;
        }

        public EstadoEN ConsultarPorNombre(string Nombre)
        {
            IDbConnection _Conn = ConexionDB.Conexion();
            _Conn.Open();
            SqlCommand _Command = new SqlCommand("CONSULTAR_ESTADO_POR_NOMBRE", _Conn as SqlConnection);
            _Command.CommandType = CommandType.StoredProcedure;
            _Command.Parameters.Add(new SqlParameter("@NOMBRE", Nombre));
            IDataReader _reader = _Command.ExecuteReader();
            EstadoEN estado = new EstadoEN();
            if (_reader.Read())
            {
                estado.Id = _reader.GetInt64(0);
                estado.Nom_Estado = _reader.GetString(1);
            }
            _Conn.Close();
            return estado;
        }

    }
}
