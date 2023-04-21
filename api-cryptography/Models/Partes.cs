using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace FastApiEncoder.Models
{

    //public class RootPartes
    //{
    //    //    public RootPartes() {
    //    //        Partes = new Partes();
    //    //        Detalle = new List<Detalle>();
    //    //    }
    //    //    public Partes Partes { get; set; }
    //    //    public List<Detalle> Detalle { get; set; }
    //    //}

        [DataContract]
        public partial class Partes {
            public string Id { get; set; }
            public string Tipo { get; set; }
            public string Fecha { get; set; }
            public string NumeroParte { get; set; }
            public string Maquina { get; set; }
            public string Operario { get; set; }
            public string Proveedor { get; set; }
            public string Horometro { get; set; }
            public string Kilometros { get; set; }
            public List<Detalle> Detalle { get; set; }
        }

        [DataContract]
        public partial class Detalle {
            public string Id { get; set; }
            public string Cliente { get; set; }
            public string Obra { get; set; }
            public string Producto { get; set; }
            public string CtdEmpleada { get; set; }
            public string Servicio { get; set; }
            public string NumeroDoc { get; set; }
            public string UM { get; set; }
            public string Toneladas { get; set; }
            public string Viajes { get; set; }
            public string Descripcion { get; set; }
            public string Descripcion1 { get; set; }
            public string Pendiente { get; set; }
        }
    }
