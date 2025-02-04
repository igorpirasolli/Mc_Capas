using McCapas.Models;
using System.Data;

namespace McCapas.ServicesTapete
{
    public interface ItapeteServices
    {
        public Task<IEnumerable<Tapetes>> PegarTodosOsTapetes();
        public Task AdicionarTapete(Tapetes tapetes);
        public Task EditarTapete(Tapetes tapetes);
        public Task<Tapetes> PegarId(int id);
        public Task Deletar(int id);

        public Task<DataTable> GetData();
        

    }
}
