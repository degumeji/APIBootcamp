using ejemploEntity.Interfaces;
using ejemploEntity.Models;
using ejemploEntity.Utilitarios;
using Microsoft.AspNetCore.Mvc;

namespace ejemploEntity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : Controller
    {
        private readonly ICategoria _Categoria;
        public ControlError err = new ControlError();
        public string clase = "CategoriaController";

        public CategoriaController(ICategoria Categoria)
        {
            _Categoria = Categoria;
        }

        [HttpGet]
        [Route("getListaCategorias")]
        public async Task<Respuesta> getListaCategorias(int CategoriaId, string? nombreCategoria)
        {
            var resp = new Respuesta();
            var metodo = "getListaCategorias";

            try
            {
                resp = await _Categoria.getListaCategoriaes(CategoriaId, nombreCategoria);
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
        [Route("postCategoria")]
        public async Task<Respuesta> postCategoria([FromBody] Categorium Categoria)
        {
            var resp = new Respuesta();
            var metodo = "postCategoria";

            try
            {
                resp = await _Categoria.postCategoria(Categoria);
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
        [Route("putCategoria")]
        public async Task<Respuesta> putCategoria([FromBody] Categorium Categoria)
        {
            var resp = new Respuesta();
            var metodo = "putCategoria";

            try
            {
                resp = await _Categoria.putCategoria(Categoria);
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
        [Route("deleteCategoria")]
        public async Task<Respuesta> deleteCategoria(int CategoriaId)
        {
            var resp = new Respuesta();
            var metodo = "deleteCategoria";

            try
            {
                resp = await _Categoria.deleteCategoria(CategoriaId);
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
