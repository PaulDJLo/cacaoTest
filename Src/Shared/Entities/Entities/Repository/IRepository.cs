using System.Threading.Tasks;

namespace Shared.Entities
{
    /// <summary>
    /// Interfaz que define el contrato que debe usar el cliente que la implemente, usada para definir las 
    /// operaciones que se realizaran a la BD.
    /// </summary>
    public interface IRepository
    {
        Task<int> GetCountEstudiantes();
        Task<EstudianteResponse> GetAll();
        Task<EstudianteResponse> Insert(Estudiante estudianteNuevo);
    }
}
