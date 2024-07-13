using ejemploEntity.Interfaces;
using ejemploEntity.Models;
using ejemploEntity.Utilitarios;
using Microsoft.EntityFrameworkCore;

namespace ejemploEntity.Services
{
    public class MarcaServices : IMarca
    {
        public readonly TestContext _context;
        public ControlError err = new ControlError();
        public string clase = "MarcaServices";

        public MarcaServices(TestContext context) { _context = context; }
        public async Task<Respuesta> getListaMarcas(int marcaId, string? nombreMarca)
        {
            var resp = new Respuesta();
            var metodo = "getListaMarcas";

            var qry = _context.Marcas;

            try
            {
                if (marcaId == 0 && (nombreMarca == null || nombreMarca == ""))
                {
                    resp.code = "200";
                    resp.data = await qry.Where(x => x.Estado.Equals("A")).ToListAsync();
                    resp.mensaje = "OK";
                }
                else if (marcaId > 0 && (nombreMarca == null || nombreMarca == ""))
                {
                    resp.code = "200";
                    resp.data = await qry.Where(x => x.Estado.Equals("A") && x.MarcaId.Equals(marcaId)).ToListAsync();
                    resp.mensaje = "OK";
                }
                else if (marcaId == 0 && (nombreMarca != null || nombreMarca != ""))
                {
                    resp.code = "200";
                    resp.data = await qry.Where(x => x.Estado.Equals("A") && x.MarcaNombre.Equals(nombreMarca)).ToListAsync();
                    resp.mensaje = "OK";
                }
                else if (marcaId > 0 && nombreMarca != null && nombreMarca != "")
                {
                    resp.code = "200";
                    resp.data = await qry.Where(x => x.Estado.Equals("A") && x.MarcaId.Equals(marcaId) && x.MarcaNombre.Equals(nombreMarca)).ToListAsync();
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
        public async Task<Respuesta> postMarca(Marca marca)
        {

            var resp = new Respuesta();
            var qry = _context.Marcas;
            var metodo = "postMarca";

            try
            {
                marca.MarcaId = qry.Max(x => x.MarcaId) + 1;
                marca.FechaHoraReg = DateTime.Now;

                qry.Add(marca);
                await _context.SaveChangesAsync();

                resp.code = "200";
                resp.data = marca;
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
        public async Task<Respuesta> putMarca(Marca marca)
        {

            var resp = new Respuesta();
            var mar = new Marca();
            var qry = _context.Marcas;
            var metodo = "putMarca";

            try
            {
                mar = qry.Where(x => x.MarcaId == marca.MarcaId).FirstOrDefault();

                if (mar.MarcaId == null || mar.MarcaId == 0)
                {
                    resp.code = "400";
                    resp.data = marca;
                    resp.mensaje = "No existe la marca";
                }
                else
                {

                    mar.MarcaNombre = marca.MarcaNombre;
                    mar.MarcaId = marca.MarcaId;
                    mar.FechaHoraReg = DateTime.Now;
                    mar.Estado = marca.Estado;

                    qry.Update(mar);
                    await _context.SaveChangesAsync();

                    resp.code = "200";
                    resp.data = mar;
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
        public async Task<Respuesta> deleteMarca(int marcaId)
        {

            var resp = new Respuesta();
            var mar = new Marca();
            var qry = _context.Marcas;
            var metodo = "deleteMarca";

            try
            {
                mar = qry.Where(x => x.MarcaId == marcaId && x.Estado.Equals("A")).FirstOrDefault();

                if (mar.MarcaId == null || mar.MarcaId == 0)
                {
                    resp.code = "400";
                    resp.data = marcaId;
                    resp.mensaje = "No existe o ya fue eliminado el producto";
                }
                else
                {

                    mar.FechaHoraReg = DateTime.Now;
                    mar.Estado = "I";

                    qry.Update(mar);
                    await _context.SaveChangesAsync();

                    resp.code = "200";
                    resp.data = mar;
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
