using EN;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    public class UsuarioDAL
    {
        RolDAL rolDAL =  new RolDAL();

        public int Insertar(UsuarioEN usuario)
        {
            IDbConnection _conn = ConexionDB.Conexion();
            _conn.Open();

            SqlCommand _comand = new SqlCommand("AGREGAR_USUARIOS", _conn as SqlConnection);
            _comand.CommandType = CommandType.StoredProcedure;

            _comand.Parameters.Add(new SqlParameter("@USUARIO", usuario.Usuario));
            _comand.Parameters.Add(new SqlParameter("@CLAVE", usuario.Clave));
            _comand.Parameters.Add(new SqlParameter("@ID_ROL", usuario.Rol.Id));
            _comand.Parameters.Add(new SqlParameter("@FECHA_CREACION", usuario.Fecha_Creacion));

            int r = _comand.ExecuteNonQuery();
            _conn.Close();
            return r;
        }

        public List<UsuarioEN> Consultar()
        {
            IDbConnection _conn = ConexionDB.Conexion();
            _conn.Open();

            SqlCommand _comand = new SqlCommand("CONSULTAR_USUARIOS", _conn as SqlConnection);
            _comand.CommandType = CommandType.StoredProcedure;

            IDataReader _reader = _comand.ExecuteReader();
            List<UsuarioEN> Lista = new List<UsuarioEN>();

            while (_reader.Read())
            {
                UsuarioEN en = new UsuarioEN();
                en.Id = _reader.GetInt64(0);
                en.Usuario = _reader.GetString(1);
                en.Clave = _reader.GetString(2);
                en.Rol = rolDAL.ConsultarPorId(_reader.GetInt64(3));
                en.Fecha_Creacion = _reader.GetDateTime(4);
                Lista.Add(en);
            }

            _conn.Close();
            return Lista;
        }

        public bool UsuarioExistente(UsuarioEN usuario)
        {
            if (Consultar().Where(x=> x.Usuario == usuario.Usuario).Count() > 0 )
            {
                return true;
            }            
            return false;
        }

        public UsuarioEN ConsultarPorId(long Id)
        {
            IDbConnection _conn = ConexionDB.Conexion();
            _conn.Open();

            SqlCommand _comand = new SqlCommand("CONSULTAR_USUARIOS_POR_ID", _conn as SqlConnection);
            _comand.CommandType = CommandType.StoredProcedure;
            _comand.Parameters.Add(new SqlParameter("@ID", Id));

            IDataReader _reader = _comand.ExecuteReader();
            UsuarioEN en = new UsuarioEN();           

            if (_reader.Read())
            {
                en.Id = _reader.GetInt64(0);
                en.Usuario = _reader.GetString(1);
                en.Clave = _reader.GetString(2);
                en.Rol = rolDAL.ConsultarPorId(_reader.GetInt64(3));
                en.Fecha_Creacion = _reader.GetDateTime(4);               
            }
            _conn.Close();
            return en;
        }

        public UsuarioEN ValidarLogin(UsuarioEN Usuario)
        {
            IDbConnection _conn = ConexionDB.Conexion();
            _conn.Open();

            SqlCommand _comand = new SqlCommand("VALIDAR_LOGIN", _conn as SqlConnection);
            _comand.CommandType = CommandType.StoredProcedure;
            _comand.Parameters.Add(new SqlParameter("@USUARIO", Usuario.Usuario));
            _comand.Parameters.Add(new SqlParameter("@CLAVE", Usuario.Clave));

            IDataReader _reader = _comand.ExecuteReader();
            UsuarioEN en = new UsuarioEN();

            if (_reader.Read())
            {
                en.Id = _reader.GetInt64(0);
                en.Usuario = _reader.GetString(1);
                en.Clave = _reader.GetString(2);
                en.Rol = rolDAL.ConsultarPorId(_reader.GetInt64(3));
                en.Fecha_Creacion = _reader.GetDateTime(4);
            }
            _conn.Close();
            return en;
        }


        public int Eliminar(long Id)
        {
            IDbConnection _conn = ConexionDB.Conexion();
            _conn.Open();

            SqlCommand _comand = new SqlCommand("ELIMINAR_USUARIOS", _conn as SqlConnection);
            _comand.CommandType = CommandType.StoredProcedure;
            _comand.Parameters.Add(new SqlParameter("@ID", Id));          

            int r = _comand.ExecuteNonQuery();
            _conn.Close();
            return r;
        }


    }
}
