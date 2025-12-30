using Business.Services.IService;
using Data.Repository.IRepository;
using Domain;
using Domain.DTO;
using Domain.DTO.RequestDto;
using Utility.Exceptions;

namespace Business.Services.ProductService
{
    public class ProductionServices : IProductionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductionServices(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void Actualizar(ProductionDto datos, Production entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(Production entity)
        {
            throw new NotImplementedException();
        }

        public async Task generarProduccion(ProductionDto entityDto)
        {
            // vamos a tener que definir la parte de nuestro usuario que va a generar la produccion.
            // Este es una busqueda de ejemplo, en base a un GUID fijo. POrque todavia no hay una 
            // implementacion dinamica de usuario.
             Profile profile = await _unitOfWork.ProfileRepository.Buscar(item => item.UserUser_id == 1, "User,Rol");
            
             CatalogProduction catalogProduction = await _unitOfWork.CatalogProductRepository.Buscar(item => item.CatalogProduction_guid.Equals(Guid.Parse(entityDto.catalogoIdentificador)));
             // DataCatalogProduct dataCatalogProducto = await _unitOfWork.DataCatalogProductRepository.Buscar(item => item.DataCatalogProduct_guid.Equals(Guid.Parse(entityDto.dataCatalogoIdentificador)));
             // dataCatalogProducto.CatalogProduction = catalogProduction;
             
             Production production = new Production()
             {
                 Production_KGTotal = entityDto.materialesProduccion.Sum(item => item.KgUse_dto),
                 Production_name = entityDto.nombreProduccion,
                 Production_numberTimesManufactured = entityDto.numeroVecesProduccion,
                 CatalogProduction = catalogProduction,
                 ProductManufactureds = [
                     new ProductManufactured()
                     {
                         ProductManufactured_count = 0,
                     }
                 ],
                 Profile = profile
             };
            
             ICollection<MaterialProduction> materialProductions = new List<MaterialProduction>();
             ICollection<RawMaterial> rawMaterials = new List<RawMaterial>();

             foreach (MaterialProducionDto item in entityDto.materialesProduccion)
             {
                 RawMaterial rawMaterial = await _unitOfWork.RawMaterialRepository
                     .Buscar(element => element.RawMaterial_guid.Equals(Guid.Parse(item.id_dto)));

                 double cantidadAUsar = (item.KgUse_dto * entityDto.numeroVecesProduccion);

                 if (rawMaterial.RawMaterial_KgTotal >= cantidadAUsar)
                 {
                     rawMaterial.RawMaterial_KgTotal -= cantidadAUsar;
                
                     MaterialProduction materialProduction = new MaterialProduction()
                     {
                         MaterialProduction_KGUsed = cantidadAUsar,
                         RawMaterial = rawMaterial,
                         Production = production
                     };
                
                     materialProductions.Add(materialProduction);
                     rawMaterials.Add(rawMaterial);
                 }
                 else
                     throw new OperationAbortExceptions("No hay suficiente materia prima para realizar la produccion. Revisa la bodega.");
             }
            
             await _unitOfWork.RawMaterialRepository.UpdateAll(rawMaterials);
             await _unitOfWork.MaterialProductionRepository.CreateAll(materialProductions);
            
             _unitOfWork.Save();
             _unitOfWork.Dispose();
        }

        public void Agregate(ProductionDto entityDto)
        {

        }

        public Production Buscar(ProductionDto entityDto)
        {
            throw new NotImplementedException();
        }

        public Task<Production> Buscar(Guid id, string properties = "")
        {
            throw new NotImplementedException();
        }

        public void Eliminar(Production entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Production> Obtener()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Production>> Obtener(int skip, string data)
        {
            //vamos a obtener por partes los datos de produccion.
            return _unitOfWork.ProductionRepository.Buscar(item => item.Production_status == ESTADO.PRODUCCION,skip, data);
        }

        public async Task editarEstadoProduccion(IEnumerable<ProductionRequestDto> productionRequestDto)
        {
            ICollection<Production> listadoProductions = new List<Production>();

            foreach (ProductionRequestDto item in productionRequestDto)
            {
                Production production = await _unitOfWork.ProductionRepository.Buscar(
                    element => element.Production_guid.Equals(Guid.Parse(item.Identificador))
                );
                production.Production_status = (ESTADO)item.Estado;
                production.Production_updated = DateTime.Now;

                listadoProductions.Add(production);
            }

            await _unitOfWork.ProductionRepository.UpdateAll(listadoProductions);

            _unitOfWork.Save();
            _unitOfWork.Dispose();
        }
    }
}