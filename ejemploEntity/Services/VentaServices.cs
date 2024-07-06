using ejemploEntity.DTOs;
using ejemploEntity.Interfaces;
using ejemploEntity.Models;
using ejemploEntity.Utilitarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ejemploEntity.Services
{
    public class VentaServices : IVentas
    {
        public readonly TestContext _context; // INYECCIÓN DE DEPENDENCIA
        public ControlError err = new ControlError();
        public string servicio = "VentaServices";

        public VentaServices(TestContext context) { _context = context; }
        public async Task<Respuesta> getListaVentas(string? numFactura)
        {
            var resp = new Respuesta();
            var metodo = "getListaVentas";

            var qryVen = _context.Ventas;
            var qryCli = _context.Clientes;
            var qryPro = _context.Productos;
            var qryMod = _context.Modelos;
            var qryCat = _context.Categoria;
            var qrySuc = _context.Sucursals;

            try
            {
                if (numFactura != null && numFactura != "0")
                {
                    resp.data = await (from v in qryVen
                                       join cl in qryCli on v.ClienteId equals cl.ClienteId
                                       join pr in qryPro on v.ProductoId equals pr.ProductoId
                                       join mo in qryMod on v.ModeloId equals mo.ModeloId
                                       join ct in qryCat on v.CategId equals ct.CategId
                                       join sc in qrySuc on v.SucursalId equals sc.SucursalId
                                       where v.NumFact.Equals(numFactura) && v.Estado.Equals("Registrada")
                                       select new VentaDto
                                       {
                                           IdFactura = v.IdFactura,
                                           NumFact = v.NumFact,
                                           FechaHora = v.FechaHora,
                                           ClienteDetalle = cl.ClienteNombre,
                                           ProductoDetalle = pr.ProductoDescrip,
                                           ModeloDetalle = mo.ModeloDescripción,
                                           CategDetalle = ct.CategNombre,
                                           SucursalDetalle = sc.SucursalNombre,
                                           Caja = v.Caja,
                                           Vendedor = v.Vendedor,
                                           Precio = v.Precio,
                                           Unidades = v.Unidades,
                                           Estado = v.Estado
                                       }).ToListAsync();
                }
                else
                {
                    // resp.data = await qryPro.Where(x => x.ProductoId == productoId).ToListAsync();
                }

                resp.code = "200";
                resp.mensaje = "Correcto!";

                err.LogErrorMetodos($"{servicio}\\{metodo}", "Error");
            }
            catch (Exception ex)
            {
                resp.code = "400";
                resp.mensaje = $"Error en VentaServices {ex.Message}";
                err.LogErrorMetodos($"{servicio}\\{metodo}", ex.Message);
            }

            return resp;
        }
        public async Task<Respuesta> getVentaCliente(string? numFactura, DateTime? fecha, string? vendedor, float? precio)
        {
            var resp = new Respuesta();
            var metodo = "getVentaCliente";

            var qryVen = _context.Ventas;
            var qryCli = _context.Clientes;
            var qryPro = _context.Productos;
            var qryMod = _context.Modelos;
            var qryCat = _context.Categoria;
            var qrySuc = _context.Sucursals;

            try
            {
                resp.code = "200";
                resp.mensaje = "Correcto!";

                /*IQueryable<VentaDto> qry = (from v in qryVen
                                                  join cl in qryCli on v.ClienteId equals cl.ClienteId
                                                  join pr in qryPro on v.ProductoId equals pr.ProductoId
                                                  join mo in qryMod on v.ModeloId equals mo.ModeloId
                                                  join ct in qryCat on v.CategId equals ct.CategId
                                                  join sc in qrySuc on v.SucursalId equals sc.SucursalId
                                                  where v.Estado.Equals("Registrada")
                                                  select new VentaDto
                                                  {
                                                      IdFactura = v.IdFactura,
                                                      NumFact = v.NumFact,
                                                      FechaHora = v.FechaHora,
                                                      ClienteDetalle = cl.ClienteNombre,
                                                      ProductoDetalle = pr.ProductoDescrip,
                                                      ModeloDetalle = mo.ModeloDescripción,
                                                      CategDetalle = ct.CategNombre,
                                                      SucursalDetalle = sc.SucursalNombre,
                                                      Caja = v.Caja,
                                                      Vendedor = v.Vendedor,
                                                      Precio = v.Precio,
                                                      Unidades = v.Unidades,
                                                      Estado = v.Estado
                                                  });*/

                if ((numFactura != null || numFactura != "0") && fecha == null && vendedor == null && precio == null)
                {
                    resp.data = await (from v in qryVen
                                       join cl in qryCli on v.ClienteId equals cl.ClienteId
                                       join pr in qryPro on v.ProductoId equals pr.ProductoId
                                       join mo in qryMod on v.ModeloId equals mo.ModeloId
                                       join ct in qryCat on v.CategId equals ct.CategId
                                       join sc in qrySuc on v.SucursalId equals sc.SucursalId
                                       where v.NumFact == numFactura
                                             && v.Estado.Equals("Registrada")
                                       select new VentaDto
                                       {
                                           IdFactura = v.IdFactura,
                                           NumFact = v.NumFact,
                                           FechaHora = v.FechaHora,
                                           ClienteDetalle = cl.ClienteNombre,
                                           ProductoDetalle = pr.ProductoDescrip,
                                           ModeloDetalle = mo.ModeloDescripción,
                                           CategDetalle = ct.CategNombre,
                                           SucursalDetalle = sc.SucursalNombre,
                                           Caja = v.Caja,
                                           Vendedor = v.Vendedor,
                                           Precio = v.Precio,
                                           Unidades = v.Unidades,
                                           Estado = v.Estado
                                       }).ToListAsync();
                }
                else if ((numFactura == null || numFactura == "0") && fecha != null && vendedor == null && precio == null)
                {
                    resp.data = await (from v in qryVen
                                       join cl in qryCli on v.ClienteId equals cl.ClienteId
                                       join pr in qryPro on v.ProductoId equals pr.ProductoId
                                       join mo in qryMod on v.ModeloId equals mo.ModeloId
                                       join ct in qryCat on v.CategId equals ct.CategId
                                       join sc in qrySuc on v.SucursalId equals sc.SucursalId
                                       where v.Estado.Equals("Registrada")
                                             && v.FechaHora >= fecha
                                       select new VentaDto
                                       {
                                           IdFactura = v.IdFactura,
                                           NumFact = v.NumFact,
                                           FechaHora = v.FechaHora,
                                           ClienteDetalle = cl.ClienteNombre,
                                           ProductoDetalle = pr.ProductoDescrip,
                                           ModeloDetalle = mo.ModeloDescripción,
                                           CategDetalle = ct.CategNombre,
                                           SucursalDetalle = sc.SucursalNombre,
                                           Caja = v.Caja,
                                           Vendedor = v.Vendedor,
                                           Precio = v.Precio,
                                           Unidades = v.Unidades,
                                           Estado = v.Estado
                                       }).ToListAsync();
                }
                else if ((numFactura == null || numFactura == "0") && fecha == null && vendedor != null && precio == null)
                {
                    resp.data = await (from v in qryVen
                                       join cl in qryCli on v.ClienteId equals cl.ClienteId
                                       join pr in qryPro on v.ProductoId equals pr.ProductoId
                                       join mo in qryMod on v.ModeloId equals mo.ModeloId
                                       join ct in qryCat on v.CategId equals ct.CategId
                                       join sc in qrySuc on v.SucursalId equals sc.SucursalId
                                       where v.Estado.Equals("Registrada")
                                             && v.Vendedor.Equals(vendedor)
                                       select new VentaDto
                                       {
                                           IdFactura = v.IdFactura,
                                           NumFact = v.NumFact,
                                           FechaHora = v.FechaHora,
                                           ClienteDetalle = cl.ClienteNombre,
                                           ProductoDetalle = pr.ProductoDescrip,
                                           ModeloDetalle = mo.ModeloDescripción,
                                           CategDetalle = ct.CategNombre,
                                           SucursalDetalle = sc.SucursalNombre,
                                           Caja = v.Caja,
                                           Vendedor = v.Vendedor,
                                           Precio = v.Precio,
                                           Unidades = v.Unidades,
                                           Estado = v.Estado
                                       }).ToListAsync();
                }
                else if ((numFactura == null || numFactura == "0") && fecha == null && vendedor == null && precio != null)
                {
                    resp.data = await (from v in qryVen
                                       join cl in qryCli on v.ClienteId equals cl.ClienteId
                                       join pr in qryPro on v.ProductoId equals pr.ProductoId
                                       join mo in qryMod on v.ModeloId equals mo.ModeloId
                                       join ct in qryCat on v.CategId equals ct.CategId
                                       join sc in qrySuc on v.SucursalId equals sc.SucursalId
                                       where v.Estado.Equals("Registrada")
                                             && v.Precio >= precio
                                       select new VentaDto
                                       {
                                           IdFactura = v.IdFactura,
                                           NumFact = v.NumFact,
                                           FechaHora = v.FechaHora,
                                           ClienteDetalle = cl.ClienteNombre,
                                           ProductoDetalle = pr.ProductoDescrip,
                                           ModeloDetalle = mo.ModeloDescripción,
                                           CategDetalle = ct.CategNombre,
                                           SucursalDetalle = sc.SucursalNombre,
                                           Caja = v.Caja,
                                           Vendedor = v.Vendedor,
                                           Precio = v.Precio,
                                           Unidades = v.Unidades,
                                           Estado = v.Estado
                                       }).ToListAsync();
                }
                else if ((numFactura != null || numFactura != "0") && fecha != null && vendedor != null && precio != null)
                {
                    resp.data = await (from v in qryVen
                                       join cl in qryCli on v.ClienteId equals cl.ClienteId
                                       join pr in qryPro on v.ProductoId equals pr.ProductoId
                                       join mo in qryMod on v.ModeloId equals mo.ModeloId
                                       join ct in qryCat on v.CategId equals ct.CategId
                                       join sc in qrySuc on v.SucursalId equals sc.SucursalId
                                       where v.Estado.Equals("Registrada")
                                             && v.NumFact == numFactura
                                             && v.FechaHora >= fecha
                                             && v.Vendedor.Equals(vendedor)
                                             && v.Precio >= precio
                                       select new VentaDto
                                       {
                                           IdFactura = v.IdFactura,
                                           NumFact = v.NumFact,
                                           FechaHora = v.FechaHora,
                                           ClienteDetalle = cl.ClienteNombre,
                                           ProductoDetalle = pr.ProductoDescrip,
                                           ModeloDetalle = mo.ModeloDescripción,
                                           CategDetalle = ct.CategNombre,
                                           SucursalDetalle = sc.SucursalNombre,
                                           Caja = v.Caja,
                                           Vendedor = v.Vendedor,
                                           Precio = v.Precio,
                                           Unidades = v.Unidades,
                                           Estado = v.Estado
                                       }).ToListAsync();
                }
                else
                {
                    resp.code = "200";
                    resp.mensaje = "No existe!!";
                }
            }
            catch (Exception ex)
            {
                resp.code = "400";
                resp.mensaje = $"Error en VentaServices {ex.Message}";
                err.LogErrorMetodos($"{servicio}\\{metodo}", ex.Message);
            }

            return resp;
        }
        public async Task<Respuesta> PostVenta(Venta venta)
        {
            var resp = new Respuesta();
            var qry = _context.Ventas;
            var metodo = "PostVenta";

            try
            {

                var query = qry.OrderByDescending(x => x.IdFactura).Select(x => x.IdFactura).FirstOrDefault();

                venta.IdFactura = Convert.ToInt32(query) + 1;
                venta.FechaHora = DateTime.Now;

                qry.Add(venta);
                await _context.SaveChangesAsync();

                resp.code = "200";
                resp.mensaje = "Agregado correctamente!";
            }
            catch (Exception ex)
            {
                resp.code = "999";
                resp.mensaje = $"Se generado una novedad, Error: {ex.Message}";
                err.LogErrorMetodos($"{servicio}\\{metodo}", ex.Message);
            }
            return resp;
        }
        public async Task<Respuesta> PutVenta(Venta venta)
        {
            var resp = new Respuesta();
            var qry = _context.Ventas;
            var metodo = "PutVenta";

            try
            {
                var vta = qry.Where(x => x.NumFact == venta.NumFact).FirstOrDefault();

                if (vta.IdFactura == null || vta.IdFactura == 0)
                {
                    resp.code = "400";
                    resp.data = vta;
                    resp.mensaje = "No existe el producto";
                }
                else
                {

                    vta.FechaHora = DateTime.Now;
                    vta.ClienteId = venta.ClienteId;
                    vta.ProductoId = venta.ProductoId;
                    vta.ModeloId = venta.ModeloId;
                    vta.CategId = venta.CategId;
                    vta.MarcaId = venta.MarcaId;
                    vta.SucursalId = venta.SucursalId;
                    vta.Caja = venta.Caja;
                    vta.Vendedor = venta.Vendedor;
                    vta.Precio = venta.Precio;
                    vta.Unidades = venta.Unidades;
                    vta.Estado = venta.Estado;

                    qry.Update(vta);
                    await _context.SaveChangesAsync();

                    resp.code = "200";
                    resp.data = vta;
                    resp.mensaje = "Actualizado exitosamente";
                }

                resp.code = "000";
                resp.mensaje = "OK";
            }
            catch (Exception ex)
            {
                resp.code = "999";
                resp.mensaje = $"Se generado una novedad, Error: {ex.Message}";
                err.LogErrorMetodos($"{servicio}\\{metodo}", ex.Message);
            }
            return resp;
        }
        public async Task<Respuesta> GetVentaReporte()
        {
            var resp = new Respuesta();
            var metodo = "GetVentaReporte";

            try
            {
                resp.code = "200";
                resp.data = await _context.Ventas
                    .Where(x => x.Precio > 100)
                    .GroupBy(x => x.Precio)
                    .Select(g => new
                    {
                        CantidadRegistro = g.Count(),
                        ValorConsultado = g.Key
                    })
                    .ToListAsync();
                resp.mensaje = "Todo OK";
            }
            catch (Exception ex)
            {
                resp.code = "400";
                resp.mensaje = $"Se generado una novedad, Error: {ex.Message}";
                err.LogErrorMetodos($"{servicio}\\{metodo}", ex.Message);
            }

            return resp;
        }
        public async Task<Respuesta> GetVentaReporte()
        {
            var resp = new Respuesta();

            try
            {
                resp.code = "200";
                resp.data = await _context.Ventas
                    .Where(x => x.Precio > 100)
                    .GroupBy(x => x.Precio)
                    .Select(g => new
                    {
                        CantidadRegistro = g.Count(),
                        ValorConsultado = g.Key
                    })
                    .ToListAsync();
                resp.mensaje = "Todo OK";
            }
            catch (Exception ex)
            {
                resp.code = "400";
                resp.mensaje = $"Se generado una novedad, Error: {ex.Message}";
            }

            return resp;
        }
    }
}
