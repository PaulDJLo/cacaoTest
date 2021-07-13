using Newtonsoft.Json;
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
        /// Prueba de creacion de un nuevo estudiante desde los servicios Web 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async void CreacionEstudiante()
        {
            EstudianteResponse list = new EstudianteResponse();
            Estudiante nuevoEst = EstudianteCommon.CreaEstudiante("lasTest","lastTest2","testo3",61);
            using (HttpClient client = new HttpClient())
            {
                //se serializa el estudiante nuevo
                StringContent content = new StringContent(JsonConvert.SerializeObject(nuevoEst), Encoding.UTF8, "application/json");
                string endpoint = "https://localhost:44376/Estudiante";
                using (var response = await client.PostAsync(endpoint, content))
                {
                    //se obtiene el resultado del post
                    list = await HttpContentExtensions.ReadAsAsync<EstudianteResponse>(response.Content);
                }
            }
            Assert.True(list.IsSuccess);
        }

        /// <summary>
        /// Prueba para validar que obtenemos la lista de alumnos desde servicios Web 
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
