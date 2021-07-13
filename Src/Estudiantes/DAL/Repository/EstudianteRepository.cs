using Shared.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// Clase que define las operaciones a realizar a la BD
    /// </summary>
    public class EstudianteRepository : IRepository
    {
        //cadena de conexion de la BD
        private readonly string _connectionString;
        public EstudianteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnection");
        }

        /// <summary>
        /// Metodo Insert para guardar un nuevo estudiante
        /// </summary>
        /// <param name="estudianteNuevo"></param>
        /// <returns></returns>
        public async Task<EstudianteResponse> Insert(Estudiante estudianteNuevo)
        {
            EstudianteResponse response = new EstudianteResponse();
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_INSERT_ESTUDIANTES", sql))
                {
                    try
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@Nombre", estudianteNuevo.Nombre));
                        cmd.Parameters.Add(new SqlParameter("@ApellidoPaterno", estudianteNuevo.ApellidoPaterno));
                        cmd.Parameters.Add(new SqlParameter("@ApellidoMaterno", estudianteNuevo.ApellidoMaterno));
                        cmd.Parameters.Add(new SqlParameter("@CalifOrig", estudianteNuevo.RetrieveCalificacionOriginal()));
                        cmd.Parameters.Add(new SqlParameter("@CalifFinal", estudianteNuevo.RetrieveCalificacionFinal()));
                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        response.IsSuccess = true;
                        response.Description = "Agregado correctamente";
                    }
                    catch (Exception ex)
                    {
                        response.IsSuccess = false;
                        response.Description = ex.Message;
                        response.ErrorList.Add(ex);
                    }
                    return response;
                }
            }
        }

        /// <summary>
        /// Metodo para obtener la lista de todos los estudiantes almacenados en la BD
        /// </summary>
        /// <returns></returns>
        public async Task<EstudianteResponse> GetAll()
        {
            EstudianteResponse response = new EstudianteResponse();
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_LISTA_ESTUDIANTES", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var estudiantes = new List<Estudiante>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            estudiantes.Add(MapToValue(reader));
                        }
                    }
                    response.Estudiantes = estudiantes;
                    response.IsSuccess = true;
                    
                    return response;
                }
            }
        }

        /// <summary>
        /// Metodo usado para saber la cantidad de estudiantes almacenados en la BD
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetCountEstudiantes()
        {
            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_TOTAL_ESTUDIANTES", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    int response=0;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response= (int)reader["TotalEstudiantes"];
                        }
                    }

                    return response;
                }
            }
        }

        /// <summary>
        /// Metodo usado para obtener la informacion del objeto SqlDataReader y convertirla a un nuevo objeto estudiante 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private Estudiante MapToValue(SqlDataReader reader)
        {

            return new Estudiante(
                (string)reader["Nombre"],
                (string)reader["ApellidoPaterno"],
                (string)reader["ApellidoMaterno"],
                (int)reader["CalificacionOriginal"], 
                (int)reader["CalificacionFinal"]);
           
        }
    }
}
