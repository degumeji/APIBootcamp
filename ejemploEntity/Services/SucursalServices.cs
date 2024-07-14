using ejemploEntity.Interfaces;
using ejemploEntity.Models;
using ejemploEntity.Utilitarios;
using Microsoft.EntityFrameworkCore;

namespace ejemploEntity.Services
{
    public class SucursalServices : ISucursal
    {
        public readonly TestContext _context;
        public ControlError err = new ControlError();
        public string clase = "SucursalServices";

        public SucursalServices(TestContext context) { _context = context; }
        public async Task<Respuesta> getListaSucursal(int SucursalId, string? nombreSucursal)
        {
            var resp = new Respuesta();
            var metodo = "getListaSucursal";

            var qry = _context.Sucursals;

            try
            {
                if (SucursalId == 0 && (nombreSucursal == null || nombreSucursal == ""))
                {
                    resp.code = "200";
                    resp.data = await qry.Where(x => x.Estado.Equals("A")).ToListAsync();
                    resp.mensaje = "OK";
                }
                else if (SucursalId > 0 && (nombreSucursal == null || nombreSucursal == ""))
                {
                    resp.code = "200";
                    resp.data = await qry.Where(x => x.Estado.Equals("A") && x.SucursalId.Equals(SucursalId)).ToListAsync();
                    resp.mensaje = "OK";
                }
                else if (SucursalId == 0 && (nombreSucursal != null || nombreSucursal != ""))
                {
                    resp.code = "200";
                    resp.data = await qry.Where(x => x.Estado.Equals("A") && x.SucursalNombre.Equals(nombreSucursal)).ToListAsync();
                    resp.mensaje = "OK";
                }
                else if (SucursalId > 0 && nombreSucursal != null && nombreSucursal != "")
                {
                    resp.code = "200";
                    resp.data = await qry.Where(x => x.Estado.Equals("A") && x.SucursalId.Equals(SucursalId) && x.SucursalNombre.Equals(nombreSucursal)).ToListAsync();
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
        public async Task<Respuesta> postSucursal(Sucursal Sucursal)
        {

            var resp = new Respuesta();
            var qry = _context.Sucursals;
            var metodo = "postSucursal";

            try
            {
                Sucursal.SucursalId = qry.Max(x => x.SucursalId) + 1;
                Sucursal.FechaHoraReg = DateTime.Now;

                qry.Add(Sucursal);
                await _context.SaveChangesAsync();

                resp.code = "200";
                resp.data = Sucursal;
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
        public async Task<Respuesta> putSucursal(Sucursal Sucursal)
        {

            var resp = new Respuesta();
            var suc = new Sucursal();
            var qry = _context.Sucursals;
            var metodo = "putSucursal";

            try
            {
                suc = qry.Where(x => x.SucursalId == Sucursal.SucursalId).FirstOrDefault();

                if (suc.SucursalId == null || suc.SucursalId == 0)
                {
                    resp.code = "400";
                    resp.data = Sucursal;
                    resp.mensaje = "No existe la Sucursal";
                }
                else
                {

                    suc.SucursalNombre = Sucursal.SucursalNombre;
                    suc.SucursalId = Sucursal.SucursalId;
                    suc.FechaHoraReg = DateTime.Now;
                    suc.Estado = Sucursal.Estado;

                    qry.Update(suc);
                    await _context.SaveChangesAsync();

                    resp.code = "200";
                    resp.data = suc;
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
        public async Task<Respuesta> deleteSucursal(int SucursalId)
        {

            var resp = new Respuesta();
            var suc = new Sucursal();
            var qry = _context.Sucursals;
            var metodo = "deleteSucursal";

            try
            {
                suc = qry.Where(x => x.SucursalId == SucursalId && x.Estado.Equals("A")).FirstOrDefault();

                if (suc.SucursalId == null || suc.SucursalId == 0)
                {
                    resp.code = "400";
                    resp.data = SucursalId;
                    resp.mensaje = "No existe o ya fue eliminada la Sucursal";
                }
                else
                {

                    suc.FechaHoraReg = DateTime.Now;
                    suc.Estado = "I";

                    qry.Update(suc);
                    await _context.SaveChangesAsync();

                    resp.code = "200";
                    resp.data = suc;
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
