using Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EstudianteRepository : IRepository
    {
        private readonly string _connectionString;
        public EstudianteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnection");
        }

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
                        response.Succes = true;
                        response.Description = "Agregado correctamente";
                    }
                    catch (Exception ex)
                    {
                        response.Succes = false;
                        response.Description = ex.Message;
                        response.ListError.Add(ex);
                    }
                    return response;
                }
            }
        }

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
                    response.Succes = true;
                    
                    return response;
                }
            }
        }

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
