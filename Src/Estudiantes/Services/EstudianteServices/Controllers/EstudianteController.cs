using Business;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstudianteServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstudianteController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly EstudianteBusiness _business;

        public EstudianteController(IRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _business = new EstudianteBusiness(repository);
        }

        [HttpGet]
        public async Task<ActionResult<EstudianteResponse>> Get()
        {
            return await _business.GetAll();
        }

        // POST api/values
        [HttpPost]
        public async Task<EstudianteResponse> Post([FromBody] Estudiante value)
        {
            _business.ActualizaCalificacionFinal(value);
            return await _business.Insert(value);
        }

    }
}
