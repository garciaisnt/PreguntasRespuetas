using EN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PreguntaDAL
    {

        EstadoDAL estadoDAL = new EstadoDAL();
        UsuarioDAL usuarioDAL = new UsuarioDAL();

        public int Insertar(PreguntaEN Pregunta)
        {
            IDbConnection _conn = ConexionDB.Conexion();
            _conn.Open();

            SqlCommand _comand = new SqlCommand("AGREGAR_PREGUNTAS", _conn as SqlConnection);
            _comand.CommandType = CommandType.StoredProcedure;

            _comand.Parameters.Add(new SqlParameter("@PREGUNTA", Pregunta.Pregunta));
            _comand.Parameters.Add(new SqlParameter("@FECHA_CREACION", Pregunta.Fecha_Creacion));
            _comand.Parameters.Add(new SqlParameter("@ID_ESTADO", Pregunta.Estado.Id));
            _comand.Parameters.Add(new SqlParameter("@ID_USUARIO", Pregunta.Usuario.Id));

            int r = _comand.ExecuteNonQuery();
            _conn.Close();
            return r;
        }

        public int CambiarEstadoPregunta(PreguntaEN Pregunta)
        {
            IDbConnection _conn = ConexionDB.Conexion();
            _conn.Open();

            SqlCommand _comand = new SqlCommand("CAMBIAR_ESTADO_PREGUNTA", _conn as SqlConnection);
            _comand.CommandType = CommandType.StoredProcedure;

            _comand.Parameters.Add(new SqlParameter("@ID_ESTADO", Pregunta.Estado.Id));
            _comand.Parameters.Add(new SqlParameter("@ID_PREGUNTA", Pregunta.Id));           

            int r = _comand.ExecuteNonQuery();
            _conn.Close();
            return r;
        }

        public List<PreguntaEN> Consultar()
        {
            IDbConnection _conn = ConexionDB.Conexion();
            _conn.Open();

            SqlCommand _comand = new SqlCommand("CONSULTAR_PREGUNTAS", _conn as SqlConnection);
            _comand.CommandType = CommandType.StoredProcedure;

            IDataReader _reader = _comand.ExecuteReader();
            List<PreguntaEN> Lista = new List<PreguntaEN>();

            while (_reader.Read())
            {
                PreguntaEN pregunta = new PreguntaEN();
                pregunta.Id = _reader.GetInt64(0);
                pregunta.Pregunta = _reader.GetString(1);
                pregunta.Fecha_Creacion = _reader.GetDateTime(2);
                pregunta.Estado = estadoDAL.ConsultarPorId(_reader.GetInt64(3));
                pregunta.Usuario = usuarioDAL.ConsultarPorId(_reader.GetInt64(4));
                Lista.Add(pregunta);
            }

            _conn.Close();
            return Lista;
        }

        public PreguntaEN ConsultarPorId(long Id)
        {
            IDbConnection _conn = ConexionDB.Conexion();
            _conn.Open();

            SqlCommand _comand = new SqlCommand("CONSULTAR_PREGUNTAS_POR_ID", _conn as SqlConnection);
            _comand.CommandType = CommandType.StoredProcedure;
            _comand.Parameters.Add(new SqlParameter("@ID", Id));

            IDataReader _reader = _comand.ExecuteReader();
            PreguntaEN pregunta = new PreguntaEN();

            if (_reader.Read())
            {                
                pregunta.Id = _reader.GetInt64(0);
                pregunta.Pregunta = _reader.GetString(1);
                pregunta.Fecha_Creacion = _reader.GetDateTime(2);
                pregunta.Estado = estadoDAL.ConsultarPorId(_reader.GetInt64(3));
                pregunta.Usuario = usuarioDAL.ConsultarPorId(_reader.GetInt64(4));
            }
            _conn.Close();
            return pregunta;
        }

        public int Eliminar(long Id)
        {
            IDbConnection _conn = ConexionDB.Conexion();
            _conn.Open();

            SqlCommand _comand = new SqlCommand("ELIMINAR_PREGUNTAS", _conn as SqlConnection);
            _comand.CommandType = CommandType.StoredProcedure;
            _comand.Parameters.Add(new SqlParameter("@ID", Id));

            int r = _comand.ExecuteNonQuery();
            _conn.Close();
            return r;
        }



    }
}
