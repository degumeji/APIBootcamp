using ejemploEntity.Models;

namespace ejemploEntity.Interfaces
{
    public interface IMarca
    {
        Task<Respuesta> getListaMarcas(int marcaId, string? nombreMarca);
        Task<Respuesta> postMarca(Marca marca);
        Task<Respuesta> putMarca(Marca marca);
        Task<Respuesta> deleteMarca(int marcaId);
    }
}
