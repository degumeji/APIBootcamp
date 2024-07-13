using ejemploEntity.Interfaces;
using ejemploEntity.Models;
using ejemploEntity.Utilitarios;
using Microsoft.AspNetCore.Mvc;

namespace ejemploEntity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CiudadController : Controller
    {
        private readonly ICiudad _ciudad;
        public ControlError err = new ControlError();
        public string clase = "CiudadController";

        public CiudadController(ICiudad ciudad)
        {
            _ciudad = ciudad;
        }

        [HttpGet]
        [Route("getListaCiudades")]
        public async Task<Respuesta> getListaCiudades(int ciudadId, string? nombreCiudad)
        {
            var resp = new Respuesta();
            var metodo = "getListaCiudades";

            try
            {
                resp = await _ciudad.getListaCiudades(ciudadId, nombreCiudad);
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
        [Route("postCiudad")]
        public async Task<Respuesta> postCiudad([FromBody] Ciudad ciudad)
        {
            var resp = new Respuesta();
            var metodo = "postCiudad";

            try
            {
                resp = await _ciudad.postCiudad(ciudad);
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
        [Route("putCiudad")]
        public async Task<Respuesta> putCiudad([FromBody] Ciudad ciudad)
        {
            var resp = new Respuesta();
            var metodo = "putCiudad";

            try
            {
                resp = await _ciudad.putCiudad(ciudad);
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
        [Route("deleteCiudad")]
        public async Task<Respuesta> deleteCiudad(int ciudadId)
        {
            var resp = new Respuesta();
            var metodo = "deleteCiudad";

            try
            {
                resp = await _ciudad.deleteCiudad(ciudadId);
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
