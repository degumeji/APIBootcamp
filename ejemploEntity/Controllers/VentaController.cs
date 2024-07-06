using ejemploEntity.Interfaces;
using ejemploEntity.Models;
using ejemploEntity.Utilitarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ejemploEntity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentaController : Controller
    {
        private readonly IVentas _ventas;
        public ControlError err = new ControlError();
        public string clase = "VentaController";

        public VentaController(IVentas ventas)
        {
            _ventas = ventas;
        }

        [HttpGet]
        [Route("getListaVentas")]
        public async Task<Respuesta> getListaVentas(string? numFactura)
        {
            var resp = new Respuesta();
            var metodo = "getListaVentas";

            try
            {
                resp = await _ventas.getListaVentas(numFactura);
            }
            catch (Exception ex)
            {
                resp.code = "400";
                resp.mensaje = $"Error en VentaController {ex.Message}";
                err.LogErrorMetodos(clase, metodo, ex.Message);
            }

            return resp;
        }

        [HttpGet]
        [Route("getVentaCliente")]
        public async Task<Respuesta> getVentaCliente(string? numFactura, DateTime? fecha, string? vendedor, float? precio)
        {
            var resp = new Respuesta();
            var metodo = "getVentaCliente";

            try
            {
                resp = await _ventas.getVentaCliente(numFactura, fecha, vendedor, precio);
            }
            catch (Exception ex)
            {
                resp.code = "400";
                resp.mensaje = $"Error en VentaController {ex.Message}";
                err.LogErrorMetodos(clase, metodo, ex.Message);
            }

            return resp;
        }

        [HttpPost]
        [Route("PostVenta")]
        public async Task<Respuesta> PostVenta(Venta venta)
        {
            var resp = new Respuesta();
            var metodo = "PostVenta";

            try
            {
                resp = await _ventas.PostVenta(venta);
            }
            catch (Exception ex)
            {
                resp.code = "400";
                resp.mensaje = $"Error en VentaController {ex.Message}";
                err.LogErrorMetodos(clase, metodo, ex.Message);
            }

            return resp;
        }

        [HttpPut]
        [Route("PutVenta")]
        public async Task<Respuesta> PutVenta(Venta venta)
        {
            var resp = new Respuesta();
            var metodo = "PutVenta";

            try
            {
                resp = await _ventas.PutVenta(venta);
            }
            catch (Exception ex)
            {
                resp.code = "400";
                resp.mensaje = $"Error en VentaController {ex.Message}";
                err.LogErrorMetodos(clase, metodo, ex.Message);
            }

            return resp;
        }
    }
}
