using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class EstudianteResponse : ResponseBase
    {
        public List<Estudiante> Estudiantes { get; set; }


        public EstudianteResponse() 
        {
            Estudiantes = new List<Estudiante>();
        }

        public EstudianteResponse(List<Estudiante> estudiantes)
        {
            Estudiantes = estudiantes;
            ListError = new List<Exception>();
        }
    }
}
