using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities;
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
        /// <summary>
        /// Action Details para mostrar lista de Estudiantes
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Details()
        {
            EstudianteResponse list = new EstudianteResponse();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var responseTask = await client.GetAsync(apiBaseUrl);
                    if (responseTask.IsSuccessStatusCode)
                    {
                        list = await HttpContentExtensions.ReadAsAsync<EstudianteResponse>(responseTask.Content);
                    }
                }
            }
            catch (Exception ex)
            {
                list.ErrorList.Add(ex);
            }
            return View(list.Estudiantes);
        }

        //GET: Estudiantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estudiantes/Create
        /// <summary>
        /// Action Create para crear un nuevo estudiante
        /// </summary>
        /// <param name="newEstudiante"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,ApellidoPaterno,ApellidoMaterno,Calificacion")] Models.Estudiante newEstudiante)
        {
            if (ModelState.IsValid)
            {
                //objeto EstudianteResponse donde se guardara el resultado de la operacion post.
                EstudianteResponse list = new EstudianteResponse();
                //conversion del objeto estudiante del modelo al objeto Entities.Estudiante
                Estudiante nuevoEst = new Estudiante(newEstudiante.Nombre, newEstudiante.ApellidoPaterno, newEstudiante.ApellidoMaterno, newEstudiante.Calificacion);
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        //se serializa el estudiante nuevo
                        StringContent content = new StringContent(JsonConvert.SerializeObject(nuevoEst), Encoding.UTF8, "application/json");
                        string endpoint = apiBaseUrl;
                        using (var response = await client.PostAsync(endpoint, content))
                        {
                            //se obtiene el resultado del post
                            list = await HttpContentExtensions.ReadAsAsync<EstudianteResponse>(response.Content);
                            if (list.IsSuccess)
                            {
                                //Se ha creado correctamente el estudiante
                                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Success, list.Description);
                            }
                            else
                            {
                                //Se valida si existen excepciones 
                                if (list.ErrorList.Count > 0)
                                {
                                    //al existir excepciones se muestra el mensaje de la exception 
                                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, list.Description);

                                }
                                else
                                {
                                    //al no existir excepciones lo que existe es la validacion de capacidad maxima de alumnos 60
                                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Warning, list.Description);
                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, ex.Message);
                }
            }
            else
            {
                ViewBag.Alert = CommonServices.ShowAlert(Alerts.Danger, "Model State invalido");

            }
            return PartialView("Create");
        }
    }
}
