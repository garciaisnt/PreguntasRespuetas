using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EN
{
    public class UsuarioEN
    {
        public Int64 Id { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public RolEN Rol { get; set; }
        public DateTime Fecha_Creacion { get; set; }


    }
}
