using ejemploEntity.Interfaces;
using ejemploEntity.Models;
using ejemploEntity.Utilitarios;
using Microsoft.EntityFrameworkCore;

namespace ejemploEntity.Services
{
    public class CategoriaServices : ICategoria
    {
        public readonly TestContext _context;
        public ControlError err = new ControlError();
        public string clase = "CategoriaServices";

        public CategoriaServices(TestContext context) { _context = context; }
        public async Task<Respuesta> getListaCategorias(int CategoriaId, string? nombreCategoria)
        {
            var resp = new Respuesta();
            var metodo = "getListaCategorias";

            var qry = _context.Categoria;

            try
            {
                if (CategoriaId == 0 && (nombreCategoria == null || nombreCategoria == ""))
                {
                    resp.code = "200";
                    resp.data = await qry.Where(x => x.Estado.Equals("A")).ToListAsync();
                    resp.mensaje = "OK";
                }
                else if (CategoriaId > 0 && (nombreCategoria == null || nombreCategoria == ""))
                {
                    resp.code = "200";
                    resp.data = await qry.Where(x => x.Estado.Equals("A") && x.CategId.Equals(CategoriaId)).ToListAsync();
                    resp.mensaje = "OK";
                }
                else if (CategoriaId == 0 && (nombreCategoria != null || nombreCategoria != ""))
                {
                    resp.code = "200";
                    resp.data = await qry.Where(x => x.Estado.Equals("A") && x.CategNombre.Equals(nombreCategoria)).ToListAsync();
                    resp.mensaje = "OK";
                }
                else if (CategoriaId > 0 && nombreCategoria != null && nombreCategoria != "")
                {
                    resp.code = "200";
                    resp.data = await qry.Where(x => x.Estado.Equals("A") && x.CategId.Equals(CategoriaId) && x.CategNombre.Equals(nombreCategoria)).ToListAsync();
                    resp.mensaje = "OK";
                }

            }
            catch (Exception ex)
            {
                resp.code = "400";
                resp.mensaje = $"Error en {clase}: {ex.Message}";
                err.LogErrorMetodos(clase, metodo, ex.Message);
            }

            return resp;
        }
        public async Task<Respuesta> postCategoria(Categorium Categoria)
        {

            var resp = new Respuesta();
            var qry = _context.Categoria;
            var metodo = "postCategoria";

            try
            {
                Categoria.CategId = qry.Max(x => x.CategId) + 1;
                Categoria.FechaHoraReg = DateTime.Now;

                qry.Add(Categoria);
                await _context.SaveChangesAsync();

                resp.code = "200";
                resp.data = Categoria;
                resp.mensaje = "Registrado exitosamente";

            }
            catch (Exception ex)
            {
                resp.code = "400";
                resp.mensaje = $"Error en {clase}: {ex.Message}";
                err.LogErrorMetodos(clase, metodo, ex.Message);
            }

            return resp;
        }
        public async Task<Respuesta> putCategoria(Categorium Categoria)
        {

            var resp = new Respuesta();
            var cat = new Categorium();
            var qry = _context.Categoria;
            var metodo = "putCategoria";

            try
            {
                cat = qry.Where(x => x.CategId == Categoria.CategId).FirstOrDefault();

                if (cat.CategId == null || cat.CategId == 0)
                {
                    resp.code = "400";
                    resp.data = Categoria;
                    resp.mensaje = "No existe la Categoria";
                }
                else
                {

                    cat.CategNombre = Categoria.CategNombre;
                    cat.CategId = Categoria.CategId;
                    cat.FechaHoraReg = DateTime.Now;
                    cat.Estado = Categoria.Estado;

                    qry.Update(cat);
                    await _context.SaveChangesAsync();

                    resp.code = "200";
                    resp.data = cat;
                    resp.mensaje = "Actualizado exitosamente";
                }

            }
            catch (Exception ex)
            {
                resp.code = "400";
                resp.mensaje = $"Error en {clase}: {ex.Message}";
                err.LogErrorMetodos(clase, metodo, ex.Message);
            }

            return resp;
        }
        public async Task<Respuesta> deleteCategoria(int CategoriaId)
        {

            var resp = new Respuesta();
            var cat = new Categorium();
            var qry = _context.Categoria;
            var metodo = "deleteCategoria";

            try
            {
                cat = qry.Where(x => x.CategId == CategoriaId && x.Estado.Equals("A")).FirstOrDefault();

                if (cat.CategId == null || cat.CategId == 0)
                {
                    resp.code = "400";
                    resp.data = CategoriaId;
                    resp.mensaje = "No existe o ya fue eliminada la Categoria";
                }
                else
                {

                    cat.FechaHoraReg = DateTime.Now;
                    cat.Estado = "I";

                    qry.Update(cat);
                    await _context.SaveChangesAsync();

                    resp.code = "200";
                    resp.data = cat;
                    resp.mensaje = "Eliminado exitosamente";
                }

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
