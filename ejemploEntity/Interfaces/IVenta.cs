using ejemploEntity.Models;

namespace ejemploEntity.Interfaces
{
    public interface IVentas
    {
        Task<Respuesta> getListaVentas(string? numFactura);
        Task<Respuesta> getVentaCliente(string? numFactura, DateTime? fecha, string? vendedor, float? precio);
        Task<Respuesta> PosVenta(Venta venta);
    }
}
