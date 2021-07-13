using Business;
using Microsoft.Extensions.Configuration;
using Repository;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestGeneral
{
    public class EstudianteBLLTest
    {
        /// <summary>
        /// Prueba de creacion de un nuevo estudiante desde la capa BLL
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async void CreacionEstudiante()
        {
            EstudianteBusiness est = new EstudianteBusiness(EstudianteCommon.CreaEstudianteRepository());

            EstudianteResponse result = await est.Insert(EstudianteCommon.CreaEstudiante("Ejemplo","Ej","dos",85));
            Assert.True(result.IsSuccess);
        }

        /// <summary>
        /// Prueba para validar que obtenemos la lista de alumnos desde la capa BLL
        /// </summary>
        [Fact]
        public async void ObtencionListaEstudiantes()
        {
            EstudianteBusiness est = new EstudianteBusiness(EstudianteCommon.CreaEstudianteRepository());

            EstudianteResponse estudiantes = await est.GetAll();
            Assert.NotEmpty(estudiantes.Estudiantes);
        }

    }
}
