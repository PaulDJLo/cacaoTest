using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace TestGeneral
{
    public class EstudianteServicesTest
    {

        /// <summary>
        /// Prueba de creacion de un nuevo estudiante desde la capa BLL
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async void CreacionEstudiante()
        {
            //EstudianteBusiness est = new EstudianteBusiness(EstudianteCommon.CreaEstudianteRepository());

            //EstudianteResponse result = await est.Insert(EstudianteCommon.CreaEstudiante("Ejemplo", "Ej", "dos", 85));
            //Assert.True(result.IsSuccess);
        }

        /// <summary>
        /// Prueba para validar que obtenemos la lista de alumnos desde la capa BLL
        /// </summary>
        [Fact]
        public async void ObtencionListaEstudiantes()
        {
            EstudianteResponse list = new EstudianteResponse();
            using (HttpClient client = new HttpClient())
            {
                var responseTask = await client.GetAsync("https://localhost:44376/Estudiante");
                if (responseTask.IsSuccessStatusCode)
                {
                    list = await HttpContentExtensions.ReadAsAsync<EstudianteResponse>(responseTask.Content);
                }
            } 
            
            Assert.NotEmpty(list.Estudiantes);
        }
    }
}
