using EN;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class RolDAL
    {

        public List<RolEN> Consultar()
        {
            IDbConnection _conn = ConexionDB.Conexion();
            _conn.Open();

            SqlCommand _comand = new SqlCommand("CONSULTAR_ROLES", _conn as SqlConnection);
            _comand.CommandType = CommandType.StoredProcedure;

            IDataReader _reader = _comand.ExecuteReader();
            List<RolEN> Lista = new List<RolEN>();

            while (_reader.Read())
            {
                RolEN rol = new RolEN();
                rol.Id = _reader.GetInt64(0);
                rol.Nom_Rol = _reader.GetString(1);                
                Lista.Add(rol);
            }

            _conn.Close();
            return Lista;
        }

        public RolEN ConsultarPorId(long Id)
        {
            IDbConnection _Conn = ConexionDB.Conexion();
            _Conn.Open();
            SqlCommand _Command = new SqlCommand("CONSULTAR_ROL_POR_ID", _Conn as SqlConnection);
            _Command.CommandType = CommandType.StoredProcedure;
            _Command.Parameters.Add(new SqlParameter("@ID", Id));
            IDataReader _reader = _Command.ExecuteReader();
            RolEN rol = new RolEN();
            if (_reader.Read())
            {               
                rol.Id = _reader.GetInt64(0);
                rol.Nom_Rol = _reader.GetString(1);               
            }
            _Conn.Close();
            return rol;
        }

        public RolEN ConsultarPorNombre(string Nombre)
        {
            IDbConnection _Conn = ConexionDB.Conexion();
            _Conn.Open();
            SqlCommand _Command = new SqlCommand("CONSULTAR_ROL_POR_NOMBRE", _Conn as SqlConnection);
            _Command.CommandType = CommandType.StoredProcedure;
            _Command.Parameters.Add(new SqlParameter("@NOMBRE", Nombre));
            IDataReader _reader = _Command.ExecuteReader();
            RolEN rol = new RolEN();
            if (_reader.Read())
            {
                rol.Id = _reader.GetInt64(0);
                rol.Nom_Rol = _reader.GetString(1);
            }
            _Conn.Close();
            return rol;
        }

    }
}
