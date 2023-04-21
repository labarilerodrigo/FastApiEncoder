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
    public class OperarioController : ControllerBase {

        // POST api/v1/DataProtector/<OperarioController>/true
        [HttpPost("{encode}")]
        [Consumes("application/json")]
        public IActionResult InitializeAction([FromBody] object jsonData, [FromRoute] bool encode) {
            var data = System.Text.Json.JsonSerializer.Deserialize<Operario>(jsonData.ToString(), new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            });
            var list = new List<Operario>();
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

        private JToken GetEncrypted(List<Operario> Operario) {
            Operario Operario1 = new Operario();

            foreach (var item in Operario) {
                item.Codigo = (!String.IsNullOrEmpty(item.Codigo) ? Cryptography.Crypto.EncryptStringAES(item.Codigo, "ThisIsATest") : item.Codigo);
                item.Nombre = (!String.IsNullOrEmpty(item.Nombre) ? Cryptography.Crypto.EncryptStringAES(item.Nombre, "ThisIsATest") : item.Nombre);
                item.CodigoProveedor = (!String.IsNullOrEmpty(item.CodigoProveedor) ? Cryptography.Crypto.EncryptStringAES(item.CodigoProveedor, "ThisIsATest") : item.CodigoProveedor);
                item.Usuario = (!String.IsNullOrEmpty(item.Usuario) ? Cryptography.Crypto.EncryptStringAES(item.Usuario, "ThisIsATest") : item.Usuario);
                item.Contraseña = (!String.IsNullOrEmpty(item.Contraseña) ? Cryptography.Crypto.EncryptStringAES(item.Contraseña, "ThisIsATest") : item.Contraseña);
                item.Modificado = (!String.IsNullOrEmpty(item.Modificado) ? Cryptography.Crypto.EncryptStringAES(item.Modificado, "ThisIsATest") : item.Modificado);
                Operario1 = item;
            }
            var responseJson = System.Text.Json.JsonSerializer.Serialize<Operario>(Operario1);
            JToken jt = JToken.Parse(responseJson);
            return jt;
        }

        private JToken GetDecrypted(List<Operario> Operario) {
            Operario Operario1 = new Operario();

            foreach (var item in Operario)
            {
                item.Codigo = (!String.IsNullOrEmpty(item.Codigo) ? Cryptography.Crypto.DecryptStringAES(item.Codigo, "ThisIsATest") : item.Codigo);
                item.Nombre = (!String.IsNullOrEmpty(item.Nombre) ? Cryptography.Crypto.DecryptStringAES(item.Nombre, "ThisIsATest") : item.Nombre);
                item.CodigoProveedor = (!String.IsNullOrEmpty(item.CodigoProveedor) ? Cryptography.Crypto.DecryptStringAES(item.CodigoProveedor, "ThisIsATest") : item.CodigoProveedor);
                item.Usuario = (!String.IsNullOrEmpty(item.Usuario) ? Cryptography.Crypto.DecryptStringAES(item.Usuario, "ThisIsATest") : item.Usuario);
                item.Contraseña = (!String.IsNullOrEmpty(item.Contraseña) ? Cryptography.Crypto.DecryptStringAES(item.Contraseña, "ThisIsATest") : item.Contraseña);
                item.Modificado = (!String.IsNullOrEmpty(item.Modificado) ? Cryptography.Crypto.DecryptStringAES(item.Modificado, "ThisIsATest") : item.Modificado);
                Operario1 = item;
            }
            var responseJson = System.Text.Json.JsonSerializer.Serialize<Operario>(Operario1);
            JToken jt = JToken.Parse(responseJson);
            return jt;
        }

    }
}
