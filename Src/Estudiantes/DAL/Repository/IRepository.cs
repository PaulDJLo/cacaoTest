using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository
    {
        Task<int> GetCountEstudiantes();
        Task<EstudianteResponse> GetAll();
        Task<EstudianteResponse> Insert(Estudiante estudianteNuevo);
    }
}
