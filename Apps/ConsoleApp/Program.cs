using Business;
using Entities;
using Microsoft.Extensions.Configuration;
using Repository;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration configuration = builder.Build();
            //string config = configuration.GetConnectionString("defaultConnection");
            //Console.WriteLine(config);
            EstudianteRepository repo = new EstudianteRepository(configuration);
            EstudianteBusiness est = new EstudianteBusiness(repo);
            Estudiante resl =new  Estudiante("Bertha","Higareda","Beltran", 84);
            est.ActualizaCalificacionFinal(resl);
            
            est.Insert(resl).Wait();
            var all = est.GetAll().Result as List<Estudiante>;
            all.ForEach(p =>
            {
                Console.WriteLine("alumno: " + p.Nombre);

            });

            //var res = repo.GetAll().Result as List<Estudiante>;
            //Console.WriteLine(res[0].Nombre + " " + res[0].CalificacionOriginal);

            //Estudiante newEst=
            Console.ReadKey();
        }
    }
}
