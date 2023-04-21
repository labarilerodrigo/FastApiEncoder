using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FastApiEncoder.Models
{
    [DataContract]
    public class Maquina {
        public string Codigo { get; set; }
        public string Matricula { get; set; }
        public string Descripcion { get; set; }
        public string Modificado { get; set; }
        public string FechaBaja { get; set; }
    }
}
