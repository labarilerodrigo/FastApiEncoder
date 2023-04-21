using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FastApiEncoder.Models
{
    [DataContract]
    public class Presupuesto {
        public string NumeroDocumento { get; set; }
        public string CodigoCliente { get; set; }
        public string CodigoObra { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string FechaBaja { get; set; }
    }
}
