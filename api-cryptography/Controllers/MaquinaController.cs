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
    public class MaquinaController : ControllerBase {

        // POST api/v1/DataProtector/<MaquinaController>/true
        [HttpPost("{encode}")]
        [Consumes("application/json")]
        public IActionResult InitializeAction([FromBody] object jsonData, [FromRoute] bool encode) {
            var data = System.Text.Json.JsonSerializer.Deserialize<Maquina>(jsonData.ToString(), new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            });
            var list = new List<Maquina>();
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

        private JToken GetEncrypted(List<Maquina> Maquina) {
            Maquina Maquina1 = new Maquina();

            foreach (var item in Maquina) {
                item.Codigo = (!String.IsNullOrEmpty(item.Codigo) ? Cryptography.Crypto.EncryptStringAES(item.Codigo, "ThisIsATest") : item.Codigo);
                item.Matricula = (!String.IsNullOrEmpty(item.Matricula) ? Cryptography.Crypto.EncryptStringAES(item.Matricula, "ThisIsATest") : item.Matricula);
                item.Descripcion = (!String.IsNullOrEmpty(item.Descripcion) ? Cryptography.Crypto.EncryptStringAES(item.Descripcion, "ThisIsATest") : item.Descripcion);
                item.Modificado = (!String.IsNullOrEmpty(item.Modificado) ? Cryptography.Crypto.EncryptStringAES(item.Modificado, "ThisIsATest") : item.Modificado);
                item.FechaBaja = (!String.IsNullOrEmpty(item.FechaBaja) ? Cryptography.Crypto.EncryptStringAES(item.FechaBaja, "ThisIsATest") : item.FechaBaja);
                Maquina1 = item;
            }
            var responseJson = System.Text.Json.JsonSerializer.Serialize<Maquina>(Maquina1);
            JToken jt = JToken.Parse(responseJson);
            return jt;
        }

        private JToken GetDecrypted(List<Maquina> Maquina) {
            Maquina Maquina1 = new Maquina();

            foreach (var item in Maquina)
            {
                item.Codigo = (!String.IsNullOrEmpty(item.Codigo) ? Cryptography.Crypto.DecryptStringAES(item.Codigo, "ThisIsATest") : item.Codigo);
                item.Matricula = (!String.IsNullOrEmpty(item.Matricula) ? Cryptography.Crypto.DecryptStringAES(item.Matricula, "ThisIsATest") : item.Matricula);
                item.Descripcion = (!String.IsNullOrEmpty(item.Descripcion) ? Cryptography.Crypto.DecryptStringAES(item.Descripcion, "ThisIsATest") : item.Descripcion);
                item.Modificado = (!String.IsNullOrEmpty(item.Modificado) ? Cryptography.Crypto.DecryptStringAES(item.Modificado, "ThisIsATest") : item.Modificado);
                item.FechaBaja = (!String.IsNullOrEmpty(item.FechaBaja) ? Cryptography.Crypto.DecryptStringAES(item.FechaBaja, "ThisIsATest") : item.FechaBaja);
                Maquina1 = item;
            }
            var responseJson = System.Text.Json.JsonSerializer.Serialize<Maquina>(Maquina1);
            JToken jt = JToken.Parse(responseJson);
            return jt;
        }

    }
}
