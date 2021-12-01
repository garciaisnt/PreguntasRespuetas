using System;
using System.ComponentModel.DataAnnotations;

namespace PreguntasRespuetas.Models
{
    public class RegistroUsuarioDTO
    {

        [Required(ErrorMessage = "El usuario es requerido")]
        public string usuario { get; set; }
        [Required(ErrorMessage = "La clave es requerida")]
        public string clave { get; set; }
        [Required(ErrorMessage = "Seleccione un rol")]
        public Int64 idRol { get; set; }
    }
}