using McCapas.Models;
using System.Data;

namespace McCapas.ServicesMaterial
{
    public interface IMaterialService
    {
        public Task<IEnumerable<Material>> GetMaterialsAsync();
        public Task<Material> GetByIdAsync(int id);
        public Task AddAsync(Material material);
        public Task UpdateAsync(Material material);
        public Task DeleteAsync(int id);

        public Task<DataTable> GetData();

    }
}
