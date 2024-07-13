using ejemploEntity.Interfaces;
using ejemploEntity.Models;
using ejemploEntity.Utilitarios;
using Microsoft.AspNetCore.Mvc;

namespace ejemploEntity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarcaController : Controller
    {
        private readonly IMarca _marca;
        public ControlError err = new ControlError();
        public string clase = "MarcaController";

        public MarcaController(IMarca marca)
        {
            _marca = marca;
        }

        [HttpGet]
        [Route("getListaMarcas")]
        public async Task<Respuesta> getListaMarcas(int marcaId, string? nombreMarca)
        {
            var resp = new Respuesta();
            var metodo = "getListaMarcas";

            try
            {
                resp = await _marca.getListaMarcas(marcaId, nombreMarca);
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
        [Route("postMarca")]
        public async Task<Respuesta> postMarca([FromBody] Marca marca)
        {
            var resp = new Respuesta();
            var metodo = "postMarca";

            try
            {
                resp = await _marca.postMarca(marca);
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
        [Route("putMarca")]
        public async Task<Respuesta> putMarca([FromBody] Marca marca)
        {
            var resp = new Respuesta();
            var metodo = "putMarca";

            try
            {
                resp = await _marca.putMarca(marca);
            }
            catch (Exception ex)
            {
                resp.code = "400";
                resp.mensaje = $"Error en {clase}: {ex.Message}";
                err.LogErrorMetodos(clase, metodo, ex.Message);
            }

            return resp;
        }

        [HttpDelete]
        [Route("DeleteMarca")]
        public async Task<Respuesta> deleteMarca(int marcaId)
        {
            var resp = new Respuesta();
            var metodo = "DeleteMarca";

            try
            {
                resp = await _marca.deleteMarca(marcaId);
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
