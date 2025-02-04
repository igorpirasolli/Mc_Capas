using McCapas.Data;
using McCapas.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace McCapas.Services
{
    public class CapaServices : ICapaService
    {
        private readonly AppDbContext _context;

        public CapaServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddProdutoAsync(Capas capas)
        {
            capas.DataAtual = DateTime.Now;

            _context.capas.Add(capas);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteProdutoAsync(int id)
        {
            var produto = await _context.capas.FindAsync(id);
            if (produto != null)
            {
                _context.capas.Remove(produto);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Capas>> GetAllCapas()
        {
            return await _context.capas.ToListAsync();
        }

        public async Task<DataTable> GetData()
        {
            DataTable dataTable = new DataTable();

            dataTable.TableName = "Dados do Tapete";

            dataTable.Columns.Add("Marca", typeof(string));
            dataTable.Columns.Add("Modelo", typeof(string));
            dataTable.Columns.Add("Cor", typeof(string));
            dataTable.Columns.Add("Quantidade", typeof(int));
            dataTable.Columns.Add("Data registrada", typeof(DateTime));

            var dados = await _context.capas.ToListAsync();

            if (dados.Count > 0)
            {
                dados.ForEach(capas =>
                {
                    dataTable.Rows.Add(
                        capas.Marca,
                        capas.Modelo,
                        capas.Cor,
                        capas.Quantidade,
                        capas.DataAtual
                        );
                });
            }
            return dataTable;
        }
    

        public async Task<Capas> GetProdutoByAsyncCapas(int id)
        {
            return await _context.capas.FindAsync(id);
        }

        public async Task UpdateProdutoAsync(Capas capas)
        {
            var capaBD = await _context.capas.FindAsync(capas.Id);

            capaBD.Marca = capas.Marca;
            capaBD.Modelo = capas.Modelo;
            capaBD.Cor = capas.Cor;
            capaBD.Quantidade = capas.Quantidade;

            _context.capas.Update(capaBD);
            await _context.SaveChangesAsync();
        }
    }
}
