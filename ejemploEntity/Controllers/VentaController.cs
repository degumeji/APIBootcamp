using ejemploEntity.Interfaces;
using ejemploEntity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ejemploEntity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentaController : Controller
    {
        private readonly IVentas _ventas;

        public VentaController(IVentas ventas)
        {
            _ventas = ventas;
        }

        [HttpGet]
        [Route("getListaVentas")]
        public async Task<Respuesta> getListaVentas(string? numFactura)
        {
            var resp = new Respuesta();

            try
            {
                resp = await _ventas.getListaVentas(numFactura);
            }
            catch (Exception ex)
            {
                resp.code = "400";
                resp.mensaje = $"Error en VentaController {ex.Message}";
            }

            return resp;
        }

        [HttpGet]
        [Route("getVentaCliente")]
        public async Task<Respuesta> getVentaCliente(string? numFactura, DateTime? fecha, string? vendedor, float? precio)
        {
            var resp = new Respuesta();

            try
            {
                resp = await _ventas.getVentaCliente(numFactura, fecha, vendedor, precio);
            }
            catch (Exception ex)
            {
                resp.code = "400";
                resp.mensaje = $"Error en VentaController {ex.Message}";
            }

            return resp;
        }

        [HttpPost]
        [Route("PosVenta")]
        public async Task<Respuesta> PosVenta(Venta venta)
        {
            var resp = new Respuesta();

            try
            {
                resp = await _ventas.PosVenta(venta);
            }
            catch (Exception ex)
            {
                resp.code = "400";
                resp.mensaje = $"Error en VentaController {ex.Message}";
            }

            return resp;
        }
    }
}
