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
    public class ProductoController : ControllerBase {

        // POST api/v1/DataProtector/<ProductoController>/true
        [HttpPost("{encode}")]
        [Consumes("application/json")]
        public IActionResult InitializeAction([FromBody] object jsonData, [FromRoute] bool encode) {
            var data = System.Text.Json.JsonSerializer.Deserialize<Producto>(jsonData.ToString(), new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            });
            var list = new List<Producto>();
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

        private JToken GetEncrypted(List<Producto> Producto) {
            Producto Producto1 = new Producto();

            foreach (var item in Producto) {
                item.Codigo = (!String.IsNullOrEmpty(item.Codigo) ? Cryptography.Crypto.EncryptStringAES(item.Codigo, "ThisIsATest") : item.Codigo);
                item.Descripcion = (!String.IsNullOrEmpty(item.Descripcion) ? Cryptography.Crypto.EncryptStringAES(item.Descripcion, "ThisIsATest") : item.Descripcion);
                item.CodigoUnidadMedida = (!String.IsNullOrEmpty(item.CodigoUnidadMedida) ? Cryptography.Crypto.EncryptStringAES(item.CodigoUnidadMedida, "ThisIsATest") : item.CodigoUnidadMedida);
                item.Modificado = (!String.IsNullOrEmpty(item.Modificado) ? Cryptography.Crypto.EncryptStringAES(item.Modificado, "ThisIsATest") : item.Modificado);
                Producto1 = item;
            }
            var responseJson = System.Text.Json.JsonSerializer.Serialize<Producto>(Producto1);
            JToken jt = JToken.Parse(responseJson);
            return jt;
        }

        private JToken GetDecrypted(List<Producto> Producto) {
            Producto Producto1 = new Producto();

            foreach (var item in Producto)
            {
                item.Codigo = (!String.IsNullOrEmpty(item.Codigo) ? Cryptography.Crypto.DecryptStringAES(item.Codigo, "ThisIsATest") : item.Codigo);
                item.Descripcion = (!String.IsNullOrEmpty(item.Descripcion) ? Cryptography.Crypto.DecryptStringAES(item.Descripcion, "ThisIsATest") : item.Descripcion);
                item.CodigoUnidadMedida = (!String.IsNullOrEmpty(item.CodigoUnidadMedida) ? Cryptography.Crypto.DecryptStringAES(item.CodigoUnidadMedida, "ThisIsATest") : item.CodigoUnidadMedida);
                item.Modificado = (!String.IsNullOrEmpty(item.Modificado) ? Cryptography.Crypto.DecryptStringAES(item.Modificado, "ThisIsATest") : item.Modificado);
                Producto1 = item;
            }
            var responseJson = System.Text.Json.JsonSerializer.Serialize<Producto>(Producto1);
            JToken jt = JToken.Parse(responseJson);
            return jt;
        }

    }
}
