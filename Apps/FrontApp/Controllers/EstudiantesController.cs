using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entities;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace FrontApp.Controllers
{
    public class EstudiantesController : Controller
    {
        /// <summary>
        /// string que almacena el path de la API
        /// </summary>
        private readonly String apiBaseUrl;


        public EstudiantesController(IConfiguration configuration)
        {
            apiBaseUrl = configuration.GetValue<string>("WebAPIBaseUrl");
        }

        // GET: Estudiantes/Details
        public async Task<IActionResult> Details()
        {
            EstudianteResponse list = new EstudianteResponse();
            using (HttpClient client = new HttpClient())
            {
                var responseTask = await client.GetAsync(apiBaseUrl);
                if (responseTask.IsSuccessStatusCode)
                {
                    list = await HttpContentExtensions.ReadAsAsync<EstudianteResponse>(responseTask.Content);
                }
            }

            return View(list.Estudiantes);
        }

        //GET: Estudiantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estudiantes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( [Bind("Nombre,ApellidoPaterno,ApellidoMaterno,Calificacion")]Models.Estudiante newEstudiante)
        {
            if (ModelState.IsValid)
            {
                
                Estudiante nuevoEst = new Estudiante(newEstudiante.Nombre, newEstudiante.ApellidoPaterno, newEstudiante.ApellidoMaterno, newEstudiante.Calificacion);
                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(nuevoEst), Encoding.UTF8, "application/json");
                    string endpoint = apiBaseUrl;
                    using (var response = await client.PostAsync(endpoint, content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, "Estudiante Agregado Correctamente");
                        }
                        else
                        {
                            ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Error al guardar el estudiante, intentalo de nuevo");
                        }
                    }
                }
                return PartialView("Create");
            }
            return View(newEstudiante);
        }

    }
}
