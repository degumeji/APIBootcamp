using ejemploEntity.Interfaces;
using ejemploEntity.Models;
using ejemploEntity.Utilitarios;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ejemploEntity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChuckApiController : Controller
    {
        private readonly IChuckApi _ChuckApi;
        public ControlError err = new ControlError();
        public string clase = "ChuckApiController";

        public ChuckApiController(IChuckApi chuckapi)
        {
            _ChuckApi = chuckapi;
        }

        [HttpGet]
        [Route("getChuckApi")]
        public async Task<Respuesta> getChuckApi(int num, string? detail)
        {
            var resp = new Respuesta();
            var metodo = "getChuckApi";

            try
            {
                resp = await _ChuckApi.getChuckApi(num, detail);
            }
            catch (Exception ex)
            {
                resp.code = "400";
                resp.mensaje = $"Error en {clase}: {ex.Message}";
                err.LogErrorMetodos(clase, metodo, ex.Message);
            }

            return resp;
        }
    }
}
