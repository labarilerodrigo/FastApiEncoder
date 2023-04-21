using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FastApiEncoder.Models
{
    [DataContract]
    public class Proveedor {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Modificado { get; set; }
    }
}
