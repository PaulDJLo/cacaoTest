using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public abstract class ResponseBase
    {
        public bool Succes { get; set; }
        public string Description { get; set; }
        public List<Exception> ListError { get; set; }
    }
}
