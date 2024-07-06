using ejemploEntity.Interfaces;
using ejemploEntity.Models;
using ejemploEntity.Utilitarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ejemploEntity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogoController : Controller
    {
        private readonly ICatalogo _catalogo;
        public ControlError err = new ControlError();
        public string clase = "";

        public CatalogoController(ICatalogo catalogo)
        {
            this._catalogo = catalogo;
        }

        [HttpGet]
        [Route("getCategoria")]
        public async Task<Respuesta> getCategoria(int catalogoId)
        {
            var resp = new Respuesta();
            var metodo = "getCategoria";

            try
            {
                resp = await _catalogo.getCategoria(catalogoId);
            }
            catch (Exception ex)
            {

                resp.code = "400";
                resp.mensaje = $"Error en CatalogoController: {ex.Message}";
                err.LogErrorMetodos(clase, metodo, ex.Message);
            }

            return resp;
        }
    }
}
