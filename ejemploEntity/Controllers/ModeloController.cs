using ejemploEntity.Interfaces;
using ejemploEntity.Models;
using ejemploEntity.Utilitarios;
using Microsoft.AspNetCore.Mvc;

namespace ejemploEntity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ModeloController : Controller
    {
        private readonly IModelo _modelo;
        public ControlError err = new ControlError();
        public string clase = "ModeloController";

        public ModeloController(IModelo modelo)
        {
            _modelo = modelo;
        }

        [HttpGet]
        [Route("getListaModelos")]
        public async Task<Respuesta> getListaModelos(int modeloId, string? nombreModelo)
        {
            var resp = new Respuesta();
            var metodo = "getListaModelos";

            try
            {
                resp = await _modelo.getListaModelos(modeloId, nombreModelo);
            }
            catch (Exception ex)
            {
                resp.code = "400";
                resp.mensaje = $"Error en {clase}: {ex.Message}";
                err.LogErrorMetodos(clase, metodo, ex.Message);
            }

            return resp;
        }

        [HttpPost]
        [Route("postModelo")]
        public async Task<Respuesta> postCliente([FromBody] Modelo modelo)
        {
            var resp = new Respuesta();
            var metodo = "postModelo";

            try
            {
                resp = await _modelo.postModelo(modelo);
            }
            catch (Exception ex)
            {
                resp.code = "400";
                resp.mensaje = $"Error en {clase}: {ex.Message}";
                err.LogErrorMetodos(clase, metodo, ex.Message);
            }

            return resp;
        }

        [HttpPut]
        [Route("putModelo")]
        public async Task<Respuesta> putModelo([FromBody] Modelo modelo)
        {
            var resp = new Respuesta();
            var metodo = "putModelo";

            try
            {
                resp = await _modelo.putModelo(modelo);
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
