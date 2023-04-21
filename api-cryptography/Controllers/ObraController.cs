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
    public class ObraController : ControllerBase {

        // POST api/v1/DataProtector/<ObraController>/true
        [HttpPost("{encode}")]
        [Consumes("application/json")]
        public IActionResult InitializeAction([FromBody] object jsonData, [FromRoute] bool encode) {
            var data = System.Text.Json.JsonSerializer.Deserialize<Obra>(jsonData.ToString(), new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            });
            var list = new List<Obra>();
            list.Add(data);

            try {
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
        }

        private JToken GetEncrypted(List<Obra> Obra) {
            Obra Obra1 = new Obra();

            foreach (var item in Obra) {
                item.Codigo = (!String.IsNullOrEmpty(item.Codigo) ? Cryptography.Crypto.EncryptStringAES(item.Codigo, "ThisIsATest") : item.Codigo);
                item.Descripcion = (!String.IsNullOrEmpty(item.Descripcion) ? Cryptography.Crypto.EncryptStringAES(item.Descripcion, "ThisIsATest") : item.Descripcion);
                item.CodigoCliente = (!String.IsNullOrEmpty(item.CodigoCliente) ? Cryptography.Crypto.EncryptStringAES(item.CodigoCliente, "ThisIsATest") : item.CodigoCliente);
                item.Modificado = (!String.IsNullOrEmpty(item.Modificado) ? Cryptography.Crypto.EncryptStringAES(item.Modificado, "ThisIsATest") : item.Modificado);
                item.FechaInicio = (!String.IsNullOrEmpty(item.FechaInicio) ? Cryptography.Crypto.EncryptStringAES(item.FechaInicio, "ThisIsATest") : item.FechaInicio);
                item.FechaFin = (!String.IsNullOrEmpty(item.FechaFin) ? Cryptography.Crypto.EncryptStringAES(item.FechaFin, "ThisIsATest") : item.FechaFin);
                item.FechaBaja = (!String.IsNullOrEmpty(item.FechaBaja) ? Cryptography.Crypto.EncryptStringAES(item.FechaBaja, "ThisIsATest") : item.FechaBaja);
                Obra1 = item;
            }
            var responseJson = System.Text.Json.JsonSerializer.Serialize<Obra>(Obra1);
            JToken jt = JToken.Parse(responseJson);
            return jt;
        }

        private JToken GetDecrypted(List<Obra> Obra) {
            Obra Obra1 = new Obra();

            foreach (var item in Obra)
            {
                item.Codigo = (!String.IsNullOrEmpty(item.Codigo) ? Cryptography.Crypto.DecryptStringAES(item.Codigo, "ThisIsATest") : item.Codigo);
                item.Descripcion = (!String.IsNullOrEmpty(item.Descripcion) ? Cryptography.Crypto.DecryptStringAES(item.Descripcion, "ThisIsATest") : item.Descripcion);
                item.CodigoCliente = (!String.IsNullOrEmpty(item.CodigoCliente) ? Cryptography.Crypto.DecryptStringAES(item.CodigoCliente, "ThisIsATest") : item.CodigoCliente);
                item.Modificado = (!String.IsNullOrEmpty(item.Modificado) ? Cryptography.Crypto.DecryptStringAES(item.Modificado, "ThisIsATest") : item.Modificado);
                item.FechaInicio = (!String.IsNullOrEmpty(item.FechaInicio) ? Cryptography.Crypto.DecryptStringAES(item.FechaInicio, "ThisIsATest") : item.FechaInicio);
                item.FechaFin = (!String.IsNullOrEmpty(item.FechaFin) ? Cryptography.Crypto.DecryptStringAES(item.FechaFin, "ThisIsATest") : item.FechaFin);
                item.FechaBaja = (!String.IsNullOrEmpty(item.FechaBaja) ? Cryptography.Crypto.DecryptStringAES(item.FechaBaja, "ThisIsATest") : item.FechaBaja);
                Obra1 = item;
            }
            var responseJson = System.Text.Json.JsonSerializer.Serialize<Obra>(Obra1);
            JToken jt = JToken.Parse(responseJson);
            return jt;
        }

    }
}
