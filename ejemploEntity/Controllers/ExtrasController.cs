using ejemploEntity.Interfaces;
using ejemploEntity.Models;
using ejemploEntity.Utilitarios;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ejemploEntity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExtrasController : Controller
    {
        private readonly IPokeApi _PokeApi;
        public ControlError err = new ControlError();
        public string clase = "ExtrasController";

        public ExtrasController(IPokeApi pokeapi)
        {
            _PokeApi = pokeapi;
        }

        [HttpGet]
        [Route("getPokeApi")]
        public async Task<Respuesta> getPokeApi(string nomPokemon)
        {
            var resp = new Respuesta();
            var metodo = "getPokeApi";

            try
            {
                resp = await _PokeApi.GetPokeApi(nomPokemon);
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
