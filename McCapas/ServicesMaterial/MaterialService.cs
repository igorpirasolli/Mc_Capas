using DocumentFormat.OpenXml.InkML;
using McCapas.Data;
using McCapas.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace McCapas.ServicesMaterial
{
    public class MaterialService : IMaterialService
    {
        private readonly AppDbContext _appDbContext;

        public MaterialService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddAsync(Material material)
        {
            material.DataAtual = DateTime.Now;

            _appDbContext.materialDeFabricacao.Add(material);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var material = await _appDbContext.materialDeFabricacao.FindAsync(id);
            if (material != null)
            {
                _appDbContext.materialDeFabricacao.Remove(material);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Material> GetByIdAsync(int id)
        {
            return await _appDbContext.materialDeFabricacao.FindAsync(id);
        }

        public async Task<DataTable> GetData()
        {
            {
                DataTable dataTable = new DataTable();

                dataTable.TableName = "Dados do Tapete";

                dataTable.Columns.Add("Material", typeof(string));
                dataTable.Columns.Add("Cor", typeof(string));
                dataTable.Columns.Add("Quantidade", typeof(int));
                dataTable.Columns.Add("Data registrada", typeof(DateTime));

                var dados = await _appDbContext.materialDeFabricacao.ToListAsync();

                if (dados.Count > 0)
                {
                    dados.ForEach(Material =>
                    {
                        dataTable.Rows.Add(
                            Material.TipoDoMaterial,
                            Material.Cor,
                            Material.Quantidade,
                            Material.DataAtual
                            );
                    });
                }
                return dataTable;
            }
        }

        public async Task<IEnumerable<Material>> GetMaterialsAsync()
        {
           return await _appDbContext.materialDeFabricacao.ToListAsync();
        }

        public async Task UpdateAsync(Material material)
        {
            var materialBD = await _appDbContext.materialDeFabricacao.FindAsync(material.Id);

            materialBD.TipoDoMaterial = material.Cor;
            materialBD.Cor = material.Cor;
            materialBD.Quantidade = material.Quantidade;

            _appDbContext.materialDeFabricacao.Update(materialBD);
            await _appDbContext.SaveChangesAsync();

        }
    }
}
