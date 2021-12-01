using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EN
{
    public class PreguntaEN
    {
        public Int64 Id { get; set; }
        public string Pregunta { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public EstadoEN Estado { get; set; }
        public UsuarioEN Usuario { get; set; }
    }
}
