using ejemploEntity.Interfaces;
using ejemploEntity.Models;
using ejemploEntity.Utilitarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ejemploEntity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : Controller
    {
        private readonly IProducto _producto;
        public ControlError err = new ControlError();
        public string clase = "ProductoController";

        public ProductoController(IProducto producto)
        {
            _producto = producto;
        }

        [HttpGet]
        [Route("getListaProductos")]
        public async Task<Respuesta> getListaProductos(int productoId, double precio)
        {
            var resp = new Respuesta();
            var metodo = "getListaProductos";

            try
            {
                resp = await _producto.getListaProductos(productoId, precio);
            }
            catch (Exception ex)
            {
                resp.code = "400";
                resp.mensaje = $"Error en Productocontroller {ex.Message}";
                err.LogErrorMetodos(clase, metodo, ex.Message);
            }

            return resp;
        }

        [HttpPost]
        [Route("PostProducto")]
        public async Task<Respuesta> PostProducto([FromBody] Producto producto)
        {

            var resp = new Respuesta();
            var metodo = "PostProducto";

            try
            {
                resp = await _producto.PostProducto(producto);
            }
            catch (Exception ex)
            {
                resp.code = "400";
                resp.mensaje = $"Error en Productocontroller {ex.Message}";
                err.LogErrorMetodos(clase, metodo, ex.Message);
            }

            return resp;
        }

        [HttpPut]
        [Route("PutProducto")]
        public async Task<Respuesta> PutProducto([FromBody] Producto producto)
        {

            var resp = new Respuesta();
            var metodo = "PutProducto";

            try
            {
                resp = await _producto.PutProducto(producto);
            }
            catch (Exception ex)
            {
                resp.code = "400";
                resp.mensaje = $"Error en Productocontroller {ex.Message}";
                err.LogErrorMetodos(clase, metodo, ex.Message);
            }

            return resp;
        }
    }
}
