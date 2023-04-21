using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FastApiEncoder.Models
{
    [DataContract]
    public class UnidadMedida {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }
}
