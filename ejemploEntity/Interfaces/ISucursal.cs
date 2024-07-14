using ejemploEntity.Models;
using Microsoft.AspNetCore.Mvc;

namespace ejemploEntity.Interfaces
{
    public interface ISucursal
    {
        Task<Respuesta> getListaSucursal(int SucursalId, string? nombreSucursal);
        Task<Respuesta> postSucursal(Sucursal Sucursal);
        Task<Respuesta> putSucursal(Sucursal Sucursal);
        Task<Respuesta> deleteSucursal(int SucursalId);
    }
}
