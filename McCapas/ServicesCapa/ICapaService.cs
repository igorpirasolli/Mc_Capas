using McCapas.Models;
using System.Data;

namespace McCapas.Services
{
    public interface ICapaService
    {
        Task<IEnumerable<Capas>> GetAllCapas();

        Task<Capas> GetProdutoByAsyncCapas(int id);

        Task AddProdutoAsync(Capas capas);

        Task DeleteProdutoAsync(int id);

        Task UpdateProdutoAsync(Capas capas);

        Task<DataTable> GetData();
    }
}
