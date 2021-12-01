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
    public class RespuestaDAL
    {

        UsuarioDAL usuarioDAL = new UsuarioDAL();
        PreguntaDAL preguntaDAL = new PreguntaDAL();

        public int Insertar(RespuestaEN Respuesta)
        {
            IDbConnection _conn = ConexionDB.Conexion();
            _conn.Open();

            SqlCommand _comand = new SqlCommand("AGREGAR_RESPUESTAS", _conn as SqlConnection);
            _comand.CommandType = CommandType.StoredProcedure;

            _comand.Parameters.Add(new SqlParameter("@RESPUESTA", Respuesta.Respuesta));
            _comand.Parameters.Add(new SqlParameter("@FECHA_CREACION", Respuesta.Fecha_Creacion));
            _comand.Parameters.Add(new SqlParameter("@ID_USUARIO", Respuesta.Usuario.Id));
            _comand.Parameters.Add(new SqlParameter("@ID_PREGUNTA", Respuesta.Pregunta.Id));

            int r = _comand.ExecuteNonQuery();
            _conn.Close();
            return r;
        }        

        public List<RespuestaEN> ConsultarPorIdPregunta(long Id)
        {
            IDbConnection _conn = ConexionDB.Conexion();
            _conn.Open();

            SqlCommand _comand = new SqlCommand("CONSULTAR_RESPUESTAS_POR_ID_PREGUNTA", _conn as SqlConnection);
            _comand.CommandType = CommandType.StoredProcedure;
            _comand.Parameters.Add(new SqlParameter("@ID_PREGUNTA", Id));

            IDataReader _reader = _comand.ExecuteReader();
            List<RespuestaEN> lista = new List<RespuestaEN>();

            while (_reader.Read())
            {
                RespuestaEN respuesta = new RespuestaEN();
                respuesta.Id = _reader.GetInt64(0);
                respuesta.Respuesta = _reader.GetString(1);
                respuesta.Fecha_Creacion = _reader.GetDateTime(2);
                respuesta.Usuario = usuarioDAL.ConsultarPorId(_reader.GetInt64(3));
                respuesta.Pregunta = preguntaDAL.ConsultarPorId(_reader.GetInt64(4));
                lista.Add(respuesta);
            }
            _conn.Close();
            return lista;
        }

        public int Eliminar(long Id)
        {
            IDbConnection _conn = ConexionDB.Conexion();
            _conn.Open();

            SqlCommand _comand = new SqlCommand("ELIMINAR_RESPUESTAS", _conn as SqlConnection);
            _comand.CommandType = CommandType.StoredProcedure;
            _comand.Parameters.Add(new SqlParameter("@ID", Id));

            int r = _comand.ExecuteNonQuery();
            _conn.Close();
            return r;
        }

    }
}
