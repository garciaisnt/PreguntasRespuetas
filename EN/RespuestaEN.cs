using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EN
{
    public class RespuestaEN
    {
        public Int64 Id { get; set; }
        public string Respuesta { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public UsuarioEN Usuario { get; set; }
        public PreguntaEN Pregunta { get; set; }
    }
}
