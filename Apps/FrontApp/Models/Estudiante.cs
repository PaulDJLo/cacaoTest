using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontApp.Models
{
    public class Estudiante
    {
        [Required(ErrorMessage ="el campo {0} es obligatorio")]
        [StringLength(200)]
        public String Nombre { get; set; }
        [Required(ErrorMessage = "el campo {0} es obligatorio")]
        [StringLength(200)]
        public String ApellidoPaterno { get; set; }
        [StringLength(200)]
        public String ApellidoMaterno { get; set; }

        [Range(0,100,ErrorMessage ="El campo {0} debe ser un numero entre {1} y {2}")]
        public int Calificacion { get; set; }


    }
}
