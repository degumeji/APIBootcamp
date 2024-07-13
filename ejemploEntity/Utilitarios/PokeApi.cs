using ejemploEntity.DTOs;
using ejemploEntity.Interfaces;
using ejemploEntity.Models;
using Newtonsoft.Json;

namespace ejemploEntity.Utilitarios
{
    public class PokeApi :IPokeApi
    {
        private readonly IConfiguration _config;
        private ControlError err = new ControlError();
        public string clase = "PokeApi";

        public PokeApi(IConfiguration config) {
            _config = config;
        }

        public async Task<Respuesta> GetPokeApi(string url)
        {
            var resp = new Respuesta();
            var metodo = "GetPokeApi";

            try
            {
                url = $"{_config.GetValue<string>("Keys:UrlPokeApi")}{url}";
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await client.SendAsync(request);
                var json = await response.Content.ReadAsStringAsync();

                resp.code = "200";
                resp.data = JsonConvert.DeserializeObject<PokeApiDto>(json);
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
