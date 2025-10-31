using Business.Services.IService;
using Data.Repository.IRepository;
using Domain;
using Utility.DetectSO;
using Domain.DTO;
using Microsoft.AspNetCore.Http;
using ServicioApiBodegaBalanceado.Domain.DTO;
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

        public async Task DeleteImages(ICollection<DataImageDto> images)
        {
            string PathUbication = $"{Directory.GetCurrentDirectory()}\\FilesPublic\\ImageCatalogProduction";

            foreach (DataImageDto image in images)
            {
                if (File.Exists($"{PathUbication}\\{image.Url}"))
                    File.Delete($"{PathUbication}\\{image.Url}");

                ImageCatalogProduction imageCatalogProduction = await _unitOfWork.ImageCatalogProductionRepository.Buscar(
                    item => item.ImageCatalogProduction_guid.Equals(Guid.Parse(image.Identificador))
                );

                _unitOfWork.ImageCatalogProductionRepository.Delete(imageCatalogProduction);
            }

            _unitOfWork.Save();
            _unitOfWork.Dispose();
        }

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

        public async Task<ICollection<DataImageDto>> SaveImages(IEnumerable<IFormFile> formFiles, Guid guid)
        {
            ICollection<DataImageDto> datImages = new List<DataImageDto>();

            if (formFiles.Any())
            {
                if (formFiles.Count() > 5)
                    formFiles = formFiles.Take(5);

                CatalogProduction catalogProduction = await this.Buscar(guid, "ImageCatalogProductions");
                int totalImagenesFormFiles = formFiles.Count();
                int conteoImagenesTotal = Math.Abs(catalogProduction.ImageCatalogProductions.Count() - 10);

                if (conteoImagenesTotal == 0)
                    throw new OperationAbortExceptions();


                if (totalImagenesFormFiles > conteoImagenesTotal)
                    formFiles = formFiles.Take(conteoImagenesTotal);

                ICollection<ImageCatalogProduction> imagenesCatalogProduction = new List<ImageCatalogProduction>();

                string pathPartial = "\\FilesPublic\\ImageCatalogProduction";

                if (DetectSystemOperation.IsLinux())
                    pathPartial = pathPartial.Replace("\\", "//");

                string PathUbication = $"{Directory.GetCurrentDirectory()}{pathPartial}";

                foreach (IFormFile formFile in formFiles)
                {
                    Guid identificadorIMG = Guid.NewGuid();
                    string PathFile = Path.GetFullPath(formFile.FileName);
                    string Extension = Path.GetExtension(formFile.FileName);
                    string[] ExtensionAllowd = { ".png", ".jpg", "jpeg" };
                    var NewFileName = $"IMG_{identificadorIMG.ToString()}{Extension}";

                    ImageCatalogProduction imagenCatalogoProducto = new ImageCatalogProduction()
                    {
                        ImageCatalogProduction_guid = identificadorIMG,
                        ImageCatalogProduction_name = NewFileName,
                        CatalogProduction = catalogProduction
                    };
                    DataImageDto dataImage = new()
                    {
                        Identificador = imagenCatalogoProducto.ImageCatalogProduction_guid.ToString(),
                        Url = imagenCatalogoProducto.ImageCatalogProduction_name,
                        Estado = false
                    };
                    imagenesCatalogProduction.Add(imagenCatalogoProducto);
                    datImages.Add(dataImage);

                    if (formFile.Length <= 3145728 && ExtensionAllowd.Contains(Extension))
                    {
                        if (!Directory.Exists(PathUbication))
                            Directory.CreateDirectory(PathUbication);

                        NewFileName = Path.Combine(PathUbication, NewFileName);

                        using (var stream = new FileStream(NewFileName, FileMode.Create))
                        {
                            formFile.CopyTo(stream);
                        }
                    }
                }

                await _unitOfWork.ImageCatalogProductionRepository.CreateAll(imagenesCatalogProduction);

                _unitOfWork.Save();
                _unitOfWork.Dispose();
            }

            return datImages;
        }
    }
}