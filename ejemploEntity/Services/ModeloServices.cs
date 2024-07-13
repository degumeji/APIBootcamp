using ejemploEntity.Interfaces;
using ejemploEntity.Models;
using ejemploEntity.Utilitarios;
using Microsoft.EntityFrameworkCore;

namespace ejemploEntity.Services
{
    public class ModeloServices : IModelo
    {
        public readonly TestContext _context;
        public ControlError err = new ControlError();
        public string clase = "ModeloServices";

        public ModeloServices(TestContext context) { _context = context; }
        public async Task<Respuesta> getListaModelos(int modeloId, string? nombreModelo)
        {
            var resp = new Respuesta();
            var metodo = "getListaModelos";

            var qry = _context.Modelos;

            try
            {
                if (modeloId == 0 && (nombreModelo == null || nombreModelo == ""))
                {
                    resp.code = "200";
                    resp.data = await qry.Where(x => x.Estado.Equals("A")).ToListAsync();
                    resp.mensaje = "OK";
                }
                else if (modeloId > 0 && (nombreModelo == null || nombreModelo == ""))
                {
                    resp.code = "200";
                    resp.data = await qry.Where(x => x.Estado.Equals("A") && x.ModeloId.Equals(modeloId)).ToListAsync();
                    resp.mensaje = "OK";
                }
                else if (modeloId == 0 && (nombreModelo != null || nombreModelo != ""))
                {
                    resp.code = "200";
                    resp.data = await qry.Where(x => x.Estado.Equals("A") && x.ModeloDescripción.Equals(nombreModelo)).ToListAsync();
                    resp.mensaje = "OK";
                }
                else if (modeloId > 0 && nombreModelo != null && nombreModelo != "")
                {
                    resp.code = "200";
                    resp.data = await qry.Where(x => x.Estado.Equals("A") && x.ModeloId.Equals(modeloId) && x.ModeloDescripción.Equals(nombreModelo)).ToListAsync();
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
        public async Task<Respuesta> postModelo(Modelo modelo)
        {

            var resp = new Respuesta();
            var qry = _context.Modelos;
            var metodo = "postModelo";

            try
            {
                modelo.ModeloId = qry.Max(x => x.ModeloId) + 1;
                modelo.FechaHoraReg = DateTime.Now;

                qry.Add(modelo);
                await _context.SaveChangesAsync();

                resp.code = "200";
                resp.data = modelo;
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
        public async Task<Respuesta> putModelo(Modelo modelo)
        {

            var resp = new Respuesta();
            var mod = new Modelo();
            var qry = _context.Modelos;
            var metodo = "putCliente";

            try
            {
                mod = qry.Where(x => x.ModeloId == modelo.ModeloId).FirstOrDefault();

                if (mod.ModeloId == null || mod.ModeloId == 0)
                {
                    resp.code = "400";
                    resp.data = modelo;
                    resp.mensaje = "No existe el producto";
                }
                else
                {

                    mod.ModeloDescripción = modelo.ModeloDescripción;
                    mod.ModeloId = modelo.ModeloId;
                    mod.FechaHoraReg = DateTime.Now;
                    mod.Estado = modelo.Estado;

                    qry.Update(mod);
                    await _context.SaveChangesAsync();

                    resp.code = "200";
                    resp.data = mod;
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
    }
}
