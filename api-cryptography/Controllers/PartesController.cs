using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using FastApiEncoder.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FastApiEncoder.Controllers
{
    [Route("api/v1/DataProtector/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class PartesController : ControllerBase {

        // POST api/v1/DataProtector/<PartesController>/true
        [HttpPost("{encode}")]
        [Consumes("application/json")]
        public IActionResult InitializeAction([FromBody] object jsonData, [FromRoute] bool encode) {
            var data = System.Text.Json.JsonSerializer.Deserialize<Partes>(jsonData.ToString(), new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            });

            var list = new List<Partes>();
            list.Add(data);

            try
            {
                if (encode)
                    return Ok(GetEncrypted(list));
                else
                    return Ok(GetDecrypted(list));
            }
            catch (Exception ex) {
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError) {
                    Content = new StringContent(string.Format("Failed while trying to {0} from JSON file.", (encode ? "encode" : "decode"))),
                    ReasonPhrase = ex.Message
                };
                return BadRequest(resp);
            }
            return Ok();
        }

        private JToken GetEncrypted(List<Partes> Partes)
        {
            Partes Partes1 = new Partes();

            foreach (var item in Partes) {
                item.Id = (!String.IsNullOrEmpty(item.Id) ? Cryptography.Crypto.EncryptStringAES(item.Id, "ThisIsATest") : item.Id);
                item.Tipo = (!String.IsNullOrEmpty(item.Tipo) ? Cryptography.Crypto.EncryptStringAES(item.Tipo, "ThisIsATest") : item.Tipo);
                item.Fecha = (!String.IsNullOrEmpty(item.Fecha) ? Cryptography.Crypto.EncryptStringAES(item.Fecha, "ThisIsATest") : item.Fecha);
                item.NumeroParte = (!String.IsNullOrEmpty(item.NumeroParte) ? Cryptography.Crypto.EncryptStringAES(item.NumeroParte, "ThisIsATest") : item.NumeroParte);
                item.Maquina = (!String.IsNullOrEmpty(item.Maquina) ? Cryptography.Crypto.EncryptStringAES(item.Maquina, "ThisIsATest") : item.Maquina);
                item.Operario = (!String.IsNullOrEmpty(item.Operario) ? Cryptography.Crypto.EncryptStringAES(item.Operario, "ThisIsATest") : item.Operario);
                item.Proveedor = (!String.IsNullOrEmpty(item.Proveedor) ? Cryptography.Crypto.EncryptStringAES(item.Proveedor, "ThisIsATest") : item.Proveedor);
                item.Horometro = (!String.IsNullOrEmpty(item.Horometro) ? Cryptography.Crypto.EncryptStringAES(item.Horometro, "ThisIsATest") : item.Horometro);
                item.Kilometros = (!String.IsNullOrEmpty(item.Kilometros) ? Cryptography.Crypto.EncryptStringAES(item.Kilometros, "ThisIsATest") : item.Kilometros);

                foreach (var detalle in item.Detalle) {
                    detalle.Id = (!String.IsNullOrEmpty(detalle.Id) ? Cryptography.Crypto.EncryptStringAES(detalle.Id, "ThisIsATest") : detalle.Id);
                    detalle.Cliente = (!String.IsNullOrEmpty(detalle.Cliente) ? Cryptography.Crypto.EncryptStringAES(detalle.Cliente, "ThisIsATest") : detalle.Cliente);
                    detalle.Obra = (!String.IsNullOrEmpty(detalle.Obra) ? Cryptography.Crypto.EncryptStringAES(detalle.Obra, "ThisIsATest") : detalle.Obra);
                    detalle.Producto = (!String.IsNullOrEmpty(detalle.Producto) ? Cryptography.Crypto.EncryptStringAES(detalle.Producto, "ThisIsATest") : detalle.Producto);
                    detalle.CtdEmpleada = (!String.IsNullOrEmpty(detalle.CtdEmpleada) ? Cryptography.Crypto.EncryptStringAES(detalle.CtdEmpleada, "ThisIsATest") : detalle.CtdEmpleada);
                    detalle.Servicio = (!String.IsNullOrEmpty(detalle.Servicio) ? Cryptography.Crypto.EncryptStringAES(detalle.Servicio, "ThisIsATest") : detalle.Servicio);
                    detalle.NumeroDoc = (!String.IsNullOrEmpty(detalle.NumeroDoc) ? Cryptography.Crypto.EncryptStringAES(detalle.NumeroDoc, "ThisIsATest") : detalle.NumeroDoc);
                    detalle.UM = (!String.IsNullOrEmpty(detalle.UM) ? Cryptography.Crypto.EncryptStringAES(detalle.UM, "ThisIsATest") : detalle.UM);
                    detalle.Toneladas = (!String.IsNullOrEmpty(detalle.Toneladas) ? Cryptography.Crypto.EncryptStringAES(detalle.Toneladas, "ThisIsATest") : detalle.Toneladas);
                    detalle.Viajes = (!String.IsNullOrEmpty(detalle.Viajes) ? Cryptography.Crypto.EncryptStringAES(detalle.Viajes, "ThisIsATest") : detalle.Viajes);
                    detalle.Descripcion = (!String.IsNullOrEmpty(detalle.Descripcion) ? Cryptography.Crypto.EncryptStringAES(detalle.Descripcion, "ThisIsATest") : detalle.Descripcion);
                    detalle.Descripcion1 = (!String.IsNullOrEmpty(detalle.Descripcion1) ? Cryptography.Crypto.EncryptStringAES(detalle.Descripcion1, "ThisIsATest") : detalle.Descripcion1);
                    detalle.Pendiente = (!String.IsNullOrEmpty(detalle.Pendiente) ? Cryptography.Crypto.EncryptStringAES(detalle.Pendiente, "ThisIsATest") : detalle.Pendiente);
                }
                Partes1 = item;
            }
            var responseJson = System.Text.Json.JsonSerializer.Serialize<Partes>(Partes1);
            JToken jt = JToken.Parse(responseJson);
            return jt;
        }

        private JToken GetDecrypted(List<Partes> Partes)
        {
            Partes Partes1 = new Partes();

            foreach (var item in Partes) {
                item.Id = (!String.IsNullOrEmpty(item.Id) ? Cryptography.Crypto.DecryptStringAES(item.Id, "ThisIsATest") : item.Id);
                item.Tipo = (!String.IsNullOrEmpty(item.Tipo) ? Cryptography.Crypto.DecryptStringAES(item.Tipo, "ThisIsATest") : item.Tipo);
                item.Fecha = (!String.IsNullOrEmpty(item.Fecha) ? Cryptography.Crypto.DecryptStringAES(item.Fecha, "ThisIsATest") : item.Fecha);
                item.NumeroParte = (!String.IsNullOrEmpty(item.NumeroParte) ? Cryptography.Crypto.DecryptStringAES(item.NumeroParte, "ThisIsATest") : item.NumeroParte);
                item.Maquina = (!String.IsNullOrEmpty(item.Maquina) ? Cryptography.Crypto.DecryptStringAES(item.Maquina, "ThisIsATest") : item.Maquina);
                item.Operario = (!String.IsNullOrEmpty(item.Operario) ? Cryptography.Crypto.DecryptStringAES(item.Operario, "ThisIsATest") : item.Operario);
                item.Proveedor = (!String.IsNullOrEmpty(item.Proveedor) ? Cryptography.Crypto.DecryptStringAES(item.Proveedor, "ThisIsATest") : item.Proveedor);
                item.Horometro = (!String.IsNullOrEmpty(item.Horometro) ? Cryptography.Crypto.DecryptStringAES(item.Horometro, "ThisIsATest") : item.Horometro);
                item.Kilometros = (!String.IsNullOrEmpty(item.Kilometros) ? Cryptography.Crypto.DecryptStringAES(item.Kilometros, "ThisIsATest") : item.Kilometros);

                foreach (var detalle in item.Detalle) {
                    detalle.Id = (!String.IsNullOrEmpty(detalle.Id) ? Cryptography.Crypto.DecryptStringAES(detalle.Id, "ThisIsATest") : detalle.Id);
                    detalle.Cliente = (!String.IsNullOrEmpty(detalle.Cliente) ? Cryptography.Crypto.DecryptStringAES(detalle.Cliente, "ThisIsATest") : detalle.Cliente);
                    detalle.Obra = (!String.IsNullOrEmpty(detalle.Obra) ? Cryptography.Crypto.DecryptStringAES(detalle.Obra, "ThisIsATest") : detalle.Obra);
                    detalle.Producto = (!String.IsNullOrEmpty(detalle.Producto) ? Cryptography.Crypto.DecryptStringAES(detalle.Producto, "ThisIsATest") : detalle.Producto);
                    detalle.CtdEmpleada = (!String.IsNullOrEmpty(detalle.CtdEmpleada) ? Cryptography.Crypto.DecryptStringAES(detalle.CtdEmpleada, "ThisIsATest") : detalle.CtdEmpleada);
                    detalle.Servicio = (!String.IsNullOrEmpty(detalle.Servicio) ? Cryptography.Crypto.DecryptStringAES(detalle.Servicio, "ThisIsATest") : detalle.Servicio);
                    detalle.NumeroDoc = (!String.IsNullOrEmpty(detalle.NumeroDoc) ? Cryptography.Crypto.DecryptStringAES(detalle.NumeroDoc, "ThisIsATest") : detalle.NumeroDoc);
                    detalle.UM = (!String.IsNullOrEmpty(detalle.UM) ? Cryptography.Crypto.DecryptStringAES(detalle.UM, "ThisIsATest") : detalle.UM);
                    detalle.Toneladas = (!String.IsNullOrEmpty(detalle.Toneladas) ? Cryptography.Crypto.DecryptStringAES(detalle.Toneladas, "ThisIsATest") : detalle.Toneladas);
                    detalle.Viajes = (!String.IsNullOrEmpty(detalle.Viajes) ? Cryptography.Crypto.DecryptStringAES(detalle.Viajes, "ThisIsATest") : detalle.Viajes);
                    detalle.Descripcion = (!String.IsNullOrEmpty(detalle.Descripcion) ? Cryptography.Crypto.DecryptStringAES(detalle.Descripcion, "ThisIsATest") : detalle.Descripcion);
                    detalle.Descripcion1 = (!String.IsNullOrEmpty(detalle.Descripcion1) ? Cryptography.Crypto.DecryptStringAES(detalle.Descripcion1, "ThisIsATest") : detalle.Descripcion1);
                    detalle.Pendiente = (!String.IsNullOrEmpty(detalle.Pendiente) ? Cryptography.Crypto.DecryptStringAES(detalle.Pendiente, "ThisIsATest") : detalle.Pendiente);
                }
                Partes1 = item;
            }
            var responseJson = System.Text.Json.JsonSerializer.Serialize<Partes>(Partes1);
            JToken jt = JToken.Parse(responseJson);
            return jt;
        }

    }
}
