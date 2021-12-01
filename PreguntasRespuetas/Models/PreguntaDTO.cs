using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PreguntasRespuetas.Models
{
    public class PreguntaDTO
    {
        [Required(ErrorMessage = "La pregunta es requerida")]
        [MaxLength(250)]
        public string pregunta { get; set; }
    }
}