using Business.Services.IService;
using Data.Repository.IRepository;
using Domain;
using Domain.DTO;
using Utility.Exceptions;

namespace Business.Services.ProductService
{
    public class CatalogProductService : ICatalogProductService
    {

        private readonly IUnitOfWork _unitOfWork;

        public CatalogProductService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void Actualizar(CatalogProductDto datos, CatalogProduction entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(CatalogProduction entity)
        {
            throw new NotImplementedException();
        }

        public async void Agregate(CatalogProductDto entityDto)
        {
            IEnumerable<DataCatalogProduct> dataCatalogsProducts = entityDto.dataCatalogProducts.Select(item =>
            {
                CatalogProduction catalogProduction = new CatalogProduction()
                {
                    CatalogProduction_name = entityDto.nombreProducto,
                };

                DataCatalogProduct dataCatalog = new DataCatalogProduct
                {
                    DataCatalogProduct_countTotal = item.cantidadTotal,
                    KG_Catalog = new KG_Catalog()
                    {
                        KGCatalog_cantidad = item.pesoKg
                    },
                    Price_KG = new Price_KG()
                    {
                        PriceKG_price = item.precio
                    },
                    CatalogProduction = catalogProduction
                };

                return dataCatalog;
            });

            await _unitOfWork.DataCatalogProductRepository.CreateAll(dataCatalogsProducts);

            _unitOfWork.Save();
            _unitOfWork.Dispose();
        }

        public async Task AgregateDataProduct(CatalogProductDto catalogProductDto)
        {
            if (catalogProductDto.identificador.Any())
            {
                var catalogProduction = await Buscar(Guid.Parse(catalogProductDto.identificador));

                IEnumerable<DataCatalogProduct> dataCatalogsProducts = catalogProductDto.dataCatalogProducts.Select(item =>
                {
                    DataCatalogProduct dataCatalog = new DataCatalogProduct
                    {
                        DataCatalogProduct_countTotal = item.cantidadTotal,
                        KG_Catalog = new KG_Catalog()
                        {
                            KGCatalog_cantidad = item.pesoKg
                        },
                        Price_KG = new Price_KG()
                        {
                            PriceKG_price = item.precio
                        },
                        CatalogProduction = catalogProduction
                    };

                    return dataCatalog;
                });


                await _unitOfWork.DataCatalogProductRepository.CreateAll(dataCatalogsProducts);

                _unitOfWork.Save();
                _unitOfWork.Dispose();

                return;
            }

            throw new OperationAbortExceptions();
        }

        public CatalogProduction Buscar(CatalogProductDto entityDto)
        {
            throw new NotImplementedException();
        }

        public Task<CatalogProduction> Buscar(Guid id, string properties = "") => _unitOfWork.CatalogProductRepository.Buscar(item => item.CatalogProduction_guid.Equals(id), properties);

        public void Eliminar(CatalogProduction entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CatalogProduction> Obtener()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CatalogProduction>> Obtener(int skip, string data)
        {
            throw new NotImplementedException();
        }
    }
}