using ejemploEntity.Interfaces;
using ejemploEntity.Models;
using ejemploEntity.Utilitarios;
using Microsoft.AspNetCore.Mvc;

namespace ejemploEntity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SucursalController : Controller
    {
        private readonly ISucursal _Sucursal;
        public ControlError err = new ControlError();
        public string clase = "SucursalController";

        public SucursalController(ISucursal Sucursal)
        {
            _Sucursal = Sucursal;
        }

        [HttpGet]
        [Route("getListaSucursal")]
        public async Task<Respuesta> getListaSucursal(int SucursalId, string? nombreSucursal)
        {
            var resp = new Respuesta();
            var metodo = "getListaSucursals";

            try
            {
                resp = await _Sucursal.getListaSucursal(SucursalId, nombreSucursal);
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
        [Route("postSucursal")]
        public async Task<Respuesta> postSucursal([FromBody] Sucursal Sucursal)
        {
            var resp = new Respuesta();
            var metodo = "postSucursal";

            try
            {
                resp = await _Sucursal.postSucursal(Sucursal);
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
        [Route("putSucursal")]
        public async Task<Respuesta> putSucursal([FromBody] Sucursal Sucursal)
        {
            var resp = new Respuesta();
            var metodo = "putSucursal";

            try
            {
                resp = await _Sucursal.putSucursal(Sucursal);
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
        [Route("deleteSucursal")]
        public async Task<Respuesta> deleteSucursal(int SucursalId)
        {
            var resp = new Respuesta();
            var metodo = "deleteSucursal";

            try
            {
                resp = await _Sucursal.deleteSucursal(SucursalId);
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
