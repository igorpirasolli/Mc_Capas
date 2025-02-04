using ClosedXML.Excel;
using McCapas.Data;
using McCapas.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace McCapas.ServicesTapete
{
    public class TapeteServices : ItapeteServices
    {
        private readonly AppDbContext _appDbContext;

        public TapeteServices(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AdicionarTapete(Tapetes tapetes)
        {
            tapetes.DataAtual = DateTime.Now;

            _appDbContext.tapete.Add(tapetes);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Deletar(int id)
        {
            var delete = await _appDbContext.tapete.FindAsync(id);

            if (delete != null)
            {
                _appDbContext.tapete.Remove(delete);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task EditarTapete(Tapetes tapetes) // aqui como pra editar ele precisa usar minha classe model para editar
                                                        // e pegar novos dados, ele usa a classe como parametro e ja tem o id
                                                        // la dentro
        {
            var tapeteDB = await _appDbContext.tapete.FindAsync(tapetes.Id);

            tapeteDB.Marca = tapetes.Marca;
            tapeteDB.Modelo = tapetes.Modelo;
            tapeteDB.Cor = tapetes.Cor;
            tapeteDB.Quantidade = tapetes.Quantidade;


            _appDbContext.tapete.Update(tapeteDB);
            await _appDbContext.AddRangeAsync();
        }

        public async Task<Tapetes> PegarId(int id) // id pq ele so vai pegar o valor que esta no id e nao usar para nada na
                                          // minha classe model, nao vai usar nenhuma propriedade, caso ele usasse, usaria a classe
                                          // como parametro
        {
            return await _appDbContext.tapete.FindAsync(id);
        }

        public async Task<IEnumerable<Tapetes>> PegarTodosOsTapetes()
        {
            return await _appDbContext.tapete.ToListAsync();

        }
        public async Task<DataTable> GetData()
        {
            DataTable dataTable = new DataTable();
            
            dataTable.TableName = "Dados do Tapete";

            dataTable.Columns.Add("Marca",  typeof(string));
            dataTable.Columns.Add("Modelo",  typeof(string));
            dataTable.Columns.Add("Cor",  typeof(string));
            dataTable.Columns.Add("Quantidade",  typeof(int));
            dataTable.Columns.Add("Data registrada",  typeof(DateTime));

            var dados = await _appDbContext.tapete.ToListAsync();

            if (dados.Count > 0)
            {
                dados.ForEach(tapete =>
                {
                    dataTable.Rows.Add(
                        tapete.Marca,
                        tapete.Modelo,
                        tapete.Cor,
                        tapete.Quantidade,
                        tapete.DataAtual
                        );
                });
            }
            return dataTable;
        }
    }
}
