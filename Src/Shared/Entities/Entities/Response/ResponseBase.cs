using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Entities
{
    /// <summary>
    /// Clase base usada por los clientes que van a transportar datos a traves de las capas, este encapsula:
    /// IsSuccess: usado para saber si cualquier operacion fue exitosa o no
    /// Description: usado para almacenar cualquier tipo de mensaje que sera usado para mostrar al usuario
    /// ErrorList: usado para almacenar las excepciones
    /// </summary>
    public abstract class ResponseBase
    {
        public bool IsSuccess { get; set; }
        public string Description { get; set; }
        public List<Exception> ErrorList { get; set; }
    }
}
