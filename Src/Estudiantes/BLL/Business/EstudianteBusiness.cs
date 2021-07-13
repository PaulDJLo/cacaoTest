using Shared.Entities;
using System;
using System.Threading.Tasks;

namespace Business
{
    /// <summary>
    /// Clase que define la logica de negocios del estudiante
    /// </summary>
    public class EstudianteBusiness
    {
        private readonly IRepository _repository;
        public EstudianteBusiness(IRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void ActualizaCalificacionFinal(Estudiante estudianteNuevo)
        {
            estudianteNuevo.ActualizaCalificacionFinal();
        }
        public async Task<EstudianteResponse> Insert(Estudiante estudianteNuevo)
        {
            EstudianteResponse estRespo = new EstudianteResponse();
            bool validacion = await ValidaCantidadEstudiantes();
            if (validacion)
            {
                estRespo = await _repository.Insert(estudianteNuevo);
                return estRespo;
            }
            else
            {
                estRespo.IsSuccess = false;
                estRespo.Description = "No se pueden inscribir mas alumnos, capacidad maxima 60 alumnos ";
                return estRespo;
            }
        }

        private async Task<bool> ValidaCantidadEstudiantes()
        {
            int totalEstudiantes = await _repository.GetCountEstudiantes();
            if (totalEstudiantes < 60)
            {
                return true;
            }
            return false;
        }
        public async Task<EstudianteResponse> GetAll()
        {
            return await _repository.GetAll();
        }
    }
}
