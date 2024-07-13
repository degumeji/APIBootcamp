using ejemploEntity.Models;
using Microsoft.AspNetCore.Mvc;

namespace ejemploEntity.Interfaces
{
    public interface ICategoria
    {
        Task<Respuesta> getListaCategorias(int CategoriaId, string? nombreCategoria);
        Task<Respuesta> postCategoria(Categorium Categoria);
        Task<Respuesta> putCategoria(Categorium Categoria);
        Task<Respuesta> deleteCategoria(int CategoriaId);
    }
}
