using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities
{
    /// <summary>
    /// Clase que modela las calificaciones de los estudiantes.
    /// </summary>
    public class Calificaciones
    {

        #region Propiedades
        public int Id { get; set; }
        public Int32 CalificacionOriginal { get; set; }
        public Int32 CalificacionFinal { get; set; }
        #endregion


        #region Constructores
        public Calificaciones() { }
        public Calificaciones(int califOrig)
        {
            CalificacionOriginal = califOrig;
        }
        public Calificaciones(int califOrig, int califFinal)
        {
            CalificacionOriginal = califOrig;
            CalificacionFinal = califFinal;
        }
        #endregion

        
        /// <summary>
        /// Metodo que actualiza la calificacion final
        /// </summary>
        public void ActualizaCalificacionFinal()
        {
            CalificacionFinal = Calificaciones.CalculaCalificacionFinal(CalificacionOriginal);
        } 
        

        /// <summary>
        /// Metodo que calcula la Calificacion Final segun las reglas del negocio
        /// </summary>
        /// <param name="califOrig"></param>
        /// <returns></returns>
        public static int CalculaCalificacionFinal(int califOrig)
        {
            if (califOrig < 38)
                return califOrig;
            int sigMultiplo = CalculaSiguienteMultiplo(califOrig);
            int result = (sigMultiplo % califOrig);
            if (result < 3)
            {
                return sigMultiplo;
            }
            else
            {
                return califOrig;
            }
        }

        /// <summary>
        /// Se obtiene el siguiente numero multiplo de 5 de la calificacion original
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static int CalculaSiguienteMultiplo(int n)
        {
            return (n / 5 + 1) * 5;
        }
    }
}
