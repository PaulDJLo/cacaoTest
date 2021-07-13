using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities
{
    /// <summary>
    /// Clase usada para transportar datos a traves de las capas, este encapsula:
    /// lista de los alumnos: valores obtenidos de la BD
    /// </summary>
    public class EstudianteResponse : ResponseBase
    {
        public List<Estudiante> Estudiantes { get; set; }


        public EstudianteResponse() 
        {
            Estudiantes = new List<Estudiante>();
            ErrorList = new List<Exception>();
        }

        public EstudianteResponse(List<Estudiante> estudiantes)
        {
            Estudiantes = estudiantes;
            ErrorList = new List<Exception>();
        }
    }
}
