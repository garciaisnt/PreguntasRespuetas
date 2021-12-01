using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PreguntasRespuetas.Models
{
    public class LoginDTO
    {
     
        [Required(ErrorMessage = "El usuario es requerido")]
        public string usuario { get; set; }
        [Required(ErrorMessage = "La clave es requerida")]
        public string clave { get; set; }
    }
}