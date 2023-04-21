using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
//using System.Text.Json;
using Newtonsoft.Json;
using System.Dynamic;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text.Json;
using FastApiEncoder.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FastApiEncoder.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class DataProtectorController : ControllerBase {

        // POST api/v1/<DataProtectorController>/true
        [HttpPost("{encode}")]
        [Consumes("application/json")]
        public IActionResult InitializeAction([FromBody] dynamic jsonData, [FromRoute] bool encode) {
            // Deserialize JSON file to .NET dynamic object
            dynamic data = JsonConvert.DeserializeObject<dynamic>(jsonData.ToString());

            try {
                if (encode)
                    return Ok(GetEncrypted(data));
                else
                    return Ok(GetDecrypted(data));
            }
            catch (Exception ex) {
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError) {
                    Content = new StringContent(string.Format("Failed while trying to {0} from JSON file.", (encode ? "encode" : "decode"))),
                    ReasonPhrase = ex.Message
                };
                return BadRequest(resp);
            }
        }

        private dynamic GetEncrypted(dynamic jsonData) {
            var dict = Helpers.JsonConversionExtensions.ToDictionary(jsonData);
            List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>(dict);
            System.Collections.Generic.Dictionary<string, object> listTmp = list.ToDictionary(k => k.Key, k => k.Value);

            // it works but it doesn't have recursion
            foreach (var item in list) {
                //if (item.Key.Contains("Detalle")) {
                //    foreach((JObject)child in (JToken)item) {

                //    }
                //}
                KeyValuePair<string, object> kvp = new KeyValuePair<string, object>(item.Key, (!String.IsNullOrEmpty(item.Value.ToString()) ?
                    Cryptography.Crypto.EncryptStringAES(item.Value.ToString(), "ThisIsATest") : 
                    item.Value.ToString()));
                listTmp.Remove(kvp.Key);
                listTmp.Add(kvp.Key, kvp.Value);
            }
            dynamic responseJson = JsonConvert.SerializeObject(listTmp);
            JToken jt = JToken.Parse(responseJson);
            return jt;

        }

        private dynamic GetDecrypted(dynamic jsonData) {
            var dict = Helpers.JsonConversionExtensions.ToDictionary(jsonData);
            List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>(dict);
            System.Collections.Generic.Dictionary<string, object> listTmp = list.ToDictionary(k => k.Key, k => k.Value);

            // it works but it doesn't have recursion
            foreach (var item in list)
            {
                KeyValuePair<string, object> kvp = new KeyValuePair<string, object>(item.Key, (!String.IsNullOrEmpty(item.Value.ToString()) ?
                                    Cryptography.Crypto.DecryptStringAES(item.Value.ToString(), "ThisIsATest") :
                                    item.Value.ToString())); listTmp.Remove(kvp.Key);
                listTmp.Add(kvp.Key, kvp.Value);
            }
            dynamic responseJson = JsonConvert.SerializeObject(listTmp);
            JToken jt = JToken.Parse(responseJson);
            return jt;
        }

    }
}
