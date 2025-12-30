using Data.Repository.IRepository;
using Domain;
using Domain.DTO;
using ServicioApiBodegaBalanceado.Business.Services.IService;

namespace Business.Services.ProductService
{
    public class ManufacturedServices : IManufacturedServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public ManufacturedServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public IEnumerable<Production> Obtener()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Production>> Obtener(int skip, string data)
        {
            return _unitOfWork.ProductionRepository.Buscar(item => item.Production_status == ESTADO.FABRICADO,skip, data);
        }

        //Vamos a utilizar este metodo para agregar productos a bodega.
        public async void Agregate(ManufacturedDto entityDto)
        {
            //esto seria mas bien una actualizacion, en base a que ya tenemos
            // un ProductManufactured registro ya existente de Production. 
            // Igual tendriamos que actualizar "DataCatalogProduct_countTotal" incrementando su valor en la tabla DataCatalogProduct
            bool resultado = Guid.TryParse(entityDto.identificadorDataCatalogProduct, out Guid identificadorDataCatalogProduct);
            bool resultado2 = Guid.TryParse(entityDto.identificadorProduccion, out Guid identificadorProduccion);

            if (resultado && resultado2 && entityDto.cantidadCostales > 0)
            {
                Production production = await _unitOfWork.ProductionRepository.Buscar(item => item.Production_guid.Equals(identificadorProduccion),
                    "ProductManufactureds");

                if (production.ProductManufactureds.Any())
                {
                    ProductManufactured productManufactured = production.ProductManufactureds.First();
                    productManufactured.ProductManufactured_count = 1000;
                    
                    _unitOfWork.ProductManufacturedRepository.Update(productManufactured);
                }
            }
            
            _unitOfWork.Save();
            _unitOfWork.Dispose();
        }

        public Production Buscar(ManufacturedDto entityDto)
        {
            throw new NotImplementedException();
        }

        public Task<Production> Buscar(Guid id, string properties = "")
        {
            throw new NotImplementedException();
        }

        public void Actualizar(ManufacturedDto datos, Production entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(Production entity)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Production entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DataCatalogProduct>> GetDataCatalogProduct(string guid)
        {   
            bool resultado = Guid.TryParse(guid, out Guid identificadorCatalogProduct);

            if (resultado)
            {
                 CatalogProduction catalogProduction = await _unitOfWork.CatalogProductRepository.Buscar(item =>
                    item.CatalogProduction_guid.Equals(identificadorCatalogProduct), "DataCatalogProduct");

                 if (catalogProduction.DataCatalogProduct.Any())
                     return catalogProduction.DataCatalogProduct;
            }

            return new List<DataCatalogProduct>();
        }
    }
}