using ejemploEntity.Models;
using Microsoft.AspNetCore.Mvc;

namespace ejemploEntity.Interfaces
{
    public interface ICiudad
    {
        Task<Respuesta> getListaCiudades(int ciudadId, string? nombreCiudad);
        Task<Respuesta> postCiudad(Ciudad ciudad);
        Task<Respuesta> putCiudad(Ciudad ciudad);
        Task<Respuesta> deleteCiudad(int ciudadId);
    }
}
