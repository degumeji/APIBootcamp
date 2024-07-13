using ejemploEntity.Models;

namespace ejemploEntity.Interfaces
{
    public interface IPokeApi
    {
        Task<Respuesta> GetPokeApi(string url);
    }
}
