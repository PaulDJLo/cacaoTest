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
    public class EstudianteDALTest
    {
        /// <summary>
        /// Prueba de creacion de un nuevo estudiante
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async void CreacionEstudiante()
        {
            EstudianteRepository repo = EstudianteCommon.CreaEstudianteRepository();
            Estudiante newEst = EstudianteCommon.CreaEstudiante("22","33","44",64);
            newEst.ActualizaCalificacionFinal();
            EstudianteResponse result = await repo.Insert(newEst);
            Assert.True(result.IsSuccess);

        }

        /// <summary>
        /// Prueba para validar que obtenemos la cantidad de alumnos
        /// </summary>
        [Fact]
        public async void TotalEstudiantes()
        {
            EstudianteRepository repo = EstudianteCommon.CreaEstudianteRepository();
            int totalEstudiantes=await repo.GetCountEstudiantes();
            Assert.InRange(totalEstudiantes,1,60);
        }

        /// <summary>
        /// Prueba para validar que obtenemos correctamente la lista de alumnos
        /// </summary>
        [Fact]
        public async void ObtencionListaEstudiantes()
        {
            EstudianteRepository repo =EstudianteCommon.CreaEstudianteRepository();
            EstudianteResponse estudiantes = await repo.GetAll();
            Assert.NotEmpty(estudiantes.Estudiantes);
        }


       
    }
}
