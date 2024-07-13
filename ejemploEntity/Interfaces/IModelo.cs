using ejemploEntity.Models;

namespace ejemploEntity.Interfaces
{
    public interface IModelo
    {
        Task<Respuesta> getListaModelos(int modeloId, string? nombreModelo);
        Task<Respuesta> postModelo(Modelo modelo);
        Task<Respuesta> putModelo(Modelo modelo);
    }
}
