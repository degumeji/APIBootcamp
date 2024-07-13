using ejemploEntity.Models;

namespace ejemploEntity.Interfaces
{
    public interface IChuckApi
    {
        Task<Respuesta> getChuckApi(int num, string? detail);
    }
}
