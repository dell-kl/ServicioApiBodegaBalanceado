using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Services.IService;
using Domain;
using Domain.DTO;

namespace ServicioApiBodegaBalanceado.Business.Services.IService
{
    public interface IManufacturedServices : IService<ManufacturedDto, Production>
    {
        public Task<IEnumerable<DataCatalogProduct>> GetDataCatalogProduct(string guid);
    }
}