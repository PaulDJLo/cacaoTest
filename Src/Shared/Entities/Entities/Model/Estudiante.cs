using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities
{
    /// <summary>
    /// Clase que modela los datos del estudiante
    /// </summary>
    public class Estudiante
    {
        public int Id { get; set; }
        public String Nombre { get; set; }
        public String ApellidoPaterno { get; set; }
        public String ApellidoMaterno { get; set; }
        public Calificaciones Calificaciones { get; set; }
        public Estudiante() { }

        public Estudiante(String nombre, String ApellidoP, String ApellidoM, int califOrign)
        {
            Nombre = nombre;
            ApellidoPaterno = ApellidoP;
            ApellidoMaterno = ApellidoM;
            Calificaciones = new Calificaciones(califOrign);
        }
        public Estudiante(String nombre, String ApellidoP, String ApellidoM, int califOrign, int califFinal)
        {
            Nombre = nombre;
            ApellidoPaterno = ApellidoP;
            ApellidoMaterno = ApellidoM;
            Calificaciones = new Calificaciones(califOrign, califFinal);
        }

        /// <summary>
        /// Metodo que actualiza la calificacion Final
        /// </summary>
        public void ActualizaCalificacionFinal()
        {
            Calificaciones.ActualizaCalificacionFinal();
        }

        /// <summary>
        /// Metodo que devuelve la calificacion original
        /// </summary>
        /// <returns></returns>
        public int RetrieveCalificacionOriginal()
        {
            return Calificaciones.CalificacionOriginal;
        }

        /// <summary>
        /// Metodo que devuelve la calificacion Final
        /// </summary>
        /// <returns></returns>
        public int RetrieveCalificacionFinal()
        {
            return Calificaciones.CalificacionFinal;
        }

        /// <summary>
        /// se Sobreescribe el metodo toString para dar formato de nombre completo
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder name = new StringBuilder();
            name.AppendFormat("{0} {1} {2}", Nombre, ApellidoPaterno, ApellidoMaterno);
            return name.ToString();
        }

    }
}
