using ejemploEntity.Interfaces;
using ejemploEntity.Models;
using ejemploEntity.Utilitarios;
using Microsoft.EntityFrameworkCore;

namespace ejemploEntity.Services
{
    public class CiudadServices : ICiudad
    {
        public readonly TestContext _context;
        public ControlError err = new ControlError();
        public string clase = "CiudadServices";

        public CiudadServices(TestContext context) { _context = context; }
        public async Task<Respuesta> getListaCiudades(int ciudadId, string? nombreCiudad)
        {
            var resp = new Respuesta();
            var metodo = "getListaCiudades";

            var qry = _context.Ciudads;

            try
            {
                if (ciudadId == 0 && (nombreCiudad == null || nombreCiudad == ""))
                {
                    resp.code = "200";
                    resp.data = await qry.Where(x => x.Estado.Equals("A")).ToListAsync();
                    resp.mensaje = "OK";
                }
                else if (ciudadId > 0 && (nombreCiudad == null || nombreCiudad == ""))
                {
                    resp.code = "200";
                    resp.data = await qry.Where(x => x.Estado.Equals("A") && x.CiudadId.Equals(ciudadId)).ToListAsync();
                    resp.mensaje = "OK";
                }
                else if (ciudadId == 0 && (nombreCiudad != null || nombreCiudad != ""))
                {
                    resp.code = "200";
                    resp.data = await qry.Where(x => x.Estado.Equals("A") && x.CiudadNombre.Equals(nombreCiudad)).ToListAsync();
                    resp.mensaje = "OK";
                }
                else if (ciudadId > 0 && nombreCiudad != null && nombreCiudad != "")
                {
                    resp.code = "200";
                    resp.data = await qry.Where(x => x.Estado.Equals("A") && x.CiudadId.Equals(ciudadId) && x.CiudadNombre.Equals(nombreCiudad)).ToListAsync();
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
        public async Task<Respuesta> postCiudad(Ciudad ciudad)
        {

            var resp = new Respuesta();
            var qry = _context.Ciudads;
            var metodo = "postCiudad";

            try
            {
                ciudad.CiudadId = qry.Max(x => x.CiudadId) + 1;
                ciudad.FechaHoraReg = DateTime.Now;

                qry.Add(ciudad);
                await _context.SaveChangesAsync();

                resp.code = "200";
                resp.data = ciudad;
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
        public async Task<Respuesta> putCiudad(Ciudad ciudad)
        {

            var resp = new Respuesta();
            var ciu = new Ciudad();
            var qry = _context.Ciudads;
            var metodo = "putCiudad";

            try
            {
                ciu = qry.Where(x => x.CiudadId == ciudad.CiudadId).FirstOrDefault();

                if (ciu.CiudadId == null || ciu.CiudadId == 0)
                {
                    resp.code = "400";
                    resp.data = ciudad;
                    resp.mensaje = "No existe la ciudad";
                }
                else
                {

                    ciu.CiudadNombre = ciudad.CiudadNombre;
                    ciu.CiudadId = ciudad.CiudadId;
                    ciu.FechaHoraReg = DateTime.Now;
                    ciu.Estado = ciudad.Estado;

                    qry.Update(ciu);
                    await _context.SaveChangesAsync();

                    resp.code = "200";
                    resp.data = ciu;
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
        public async Task<Respuesta> deleteCiudad(int ciudadId)
        {

            var resp = new Respuesta();
            var ciu = new Ciudad();
            var qry = _context.Ciudads;
            var metodo = "deleteCiudad";

            try
            {
                ciu = qry.Where(x => x.CiudadId == ciudadId && x.Estado.Equals("A")).FirstOrDefault();

                if (ciu.CiudadId == null || ciu.CiudadId == 0)
                {
                    resp.code = "400";
                    resp.data = ciudadId;
                    resp.mensaje = "No existe o ya fue eliminada la ciudad";
                }
                else
                {

                    ciu.FechaHoraReg = DateTime.Now;
                    ciu.Estado = "I";

                    qry.Update(ciu);
                    await _context.SaveChangesAsync();

                    resp.code = "200";
                    resp.data = ciu;
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
