using Microsoft.Extensions.Configuration;
using Repository;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TestGeneral
{
    public class EstudianteCommon
    {
        /// <summary>
        /// Metodo para crear un objeto repositorio
        /// </summary>
        /// <returns></returns>
        public static EstudianteRepository CreaEstudianteRepository()
        {
            IConfiguration configuration = RetrieveConfig();
            return new EstudianteRepository(configuration);
        }


        /// <summary>
        /// Metodo para obtener la cadena de conexion
        /// </summary>
        /// <returns></returns>
        private static IConfiguration RetrieveConfig()
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }

        /// <summary>
        /// Metodo que crea un estudiante
        /// </summary>
        /// <returns></returns>
        public static Estudiante CreaEstudiante(String nombre, String apellidoP,String ApellidoM, int calif)
        {
            Estudiante newEst = new Estudiante(nombre, apellidoP, ApellidoM, calif);
            newEst.ActualizaCalificacionFinal();
            return newEst;
        }
    }
}
