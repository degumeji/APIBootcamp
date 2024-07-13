using ejemploEntity.DTOs;
using ejemploEntity.Interfaces;
using ejemploEntity.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ejemploEntity.Utilitarios
{
    public class ChuckApi : IChuckApi
    {
        private readonly IConfiguration _config;
        private ControlError err = new ControlError();
        public string clase = "ChuckApi";

        public ChuckApi(IConfiguration config)
        {
            _config = config;
        }

        public async Task<Respuesta> getChuckApi(int num, string? detail)
        {
            // https://json2csharp.com/ -- convertidor JSON to Cls

            var resp = new Respuesta();
            var metodo = "GetChuckApi";
            var url = "";

            try
            {

                if (num == 1 && (detail == null || detail == ""))
                {
                    url = _config.GetValue<string>("Keys:UrlChuckApi:Categories");
                }
                else if (num == 2 && (detail != null || detail != ""))
                {
                    url = $"{_config.GetValue<string>("Keys:UrlChuckApi:RandomWithCategory")}{detail}";
                }
                else if (num == 3 && (detail != null || detail != ""))
                {
                    url = $"{_config.GetValue<string>("Keys:UrlChuckApi:Query")}{detail}";
                }
                else
                {
                    url = _config.GetValue<string>("Keys:UrlChuckApi:Random");
                }

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await client.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                resp.code = "200";

                switch (num)
                {
                    case 1:
                        resp.data = JsonConvert.DeserializeObject<List<string>>(json);
                        break;
                    case 2:
                        resp.data = JsonConvert.DeserializeObject<ChuckApiRandomDto>(json);
                        break;
                    case 3:
                        resp.data = JsonConvert.DeserializeObject<ChuckApiQueryDto>(json);
                        break;
                    default:
                        resp.data = JsonConvert.DeserializeObject<ChuckApiRandomDto>(json);
                        break;
                }

                resp.mensaje = response.EnsureSuccessStatusCode().StatusCode.ToString();
            }
            catch (Exception ex)
            {
                resp.code = "400";
                resp.mensaje = ex.Message;
                err.LogErrorMetodos(clase, metodo, ex.Message);
            }
            return resp;
        }
    }
}
