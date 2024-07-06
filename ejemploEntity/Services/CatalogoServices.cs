using ejemploEntity.Interfaces;
using ejemploEntity.Models;
using ejemploEntity.Utilitarios;
using Microsoft.EntityFrameworkCore;

namespace ejemploEntity.Services
{
    public class CatalogoServices : ICatalogo
    {
        public readonly TestContext _context;
        public ControlError err = new ControlError();
        public string clase = "CatalogoServices";

        public CatalogoServices(TestContext context)
        {
            this._context = context;
        }

        public async Task<Respuesta> getCategoria(int catalogoId)
        {
            var resp = new Respuesta();
            var qry = _context.Categoria;
            var metodo = "getCategoria";

            try
            {
                if (catalogoId == 0)
                {
                    resp.data = await qry.ToListAsync();
                }
                else
                {
                    resp.data = await qry.Where(x => x.CategId == catalogoId).ToListAsync();
                }

                resp.code = "200";
                resp.mensaje = "Exito!";
            }
            catch (Exception ex)
            {
                resp.code = "400";
                resp.mensaje = $"Error catalogo services: {ex.Message}";
                err.LogErrorMetodos(clase, metodo, ex.Message);
            }

            return resp;
        }
    }
}
