using Business.Services.IService;
using Data.Repository.IRepository;
using Domain;
using Domain.DTO;
using Domain.DTO.RequestDto;
using Microsoft.AspNetCore.Http;
using Microsoft.Data;
using Utility.DetectSO;
using Utility.Exceptions;

namespace Business.Services.ProductService
{
    public class RawMaterialService : IRawMaterialService
    {
        private readonly IUnitOfWork _unitOfWork;


        public RawMaterialService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Actualizar(RawMaterialDto datos, RawMaterial entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(RawMaterial entity)
        {
            _unitOfWork.RawMaterialRepository.Update(entity);
        }

        public async Task AddStockRawMaterial(StockRawMaterial stockRawMaterial)
        {
            RawMaterial rawMaterial = await this.Buscar(Guid.Parse(stockRawMaterial.Identificador));

            KgMonitoring kgMonitoring = new KgMonitoring()
            {
                KgMonitoring_KGStandard = stockRawMaterial.kgStandard,
                KgMonitoring_Total = (stockRawMaterial.kgStandard * stockRawMaterial.Amount),
                KgMonitoring_priceUnit = (decimal)stockRawMaterial.PriceUnit,
                KgMonitoring_priceTotal = (decimal)stockRawMaterial.PriceUnit * stockRawMaterial.Amount,
                RawMaterial = rawMaterial
            };

            rawMaterial.RawMaterial_KgTotal += kgMonitoring.KgMonitoring_Total;
            rawMaterial.RawMaterial_updated = DateTime.Now;

            Accounting accounting = new Accounting()
            {
                Accounting_egreso = kgMonitoring.KgMonitoring_priceTotal,
                KgMonitoring = kgMonitoring
            };

            this.Actualizar(rawMaterial);
            await _unitOfWork.Accounting.Create(accounting);

            _unitOfWork.Save();
            _unitOfWork.Dispose();
        }

        public void Agregate(RawMaterialDto entityDto)
        {
            double KgTotal = entityDto.KgMonitoringDtos.Sum((item) => (item.cantidad_dto * item.kg_standard));

            RawMaterial rawMaterial = new RawMaterial()
            {
                RawMaterial_guid = Guid.Parse(entityDto.id_dto!),
                RawMaterial_name = entityDto.nombre_dto,
                RawMaterial_KgTotal = KgTotal
            };

            _unitOfWork.RawMaterialRepository.Create(rawMaterial);

            // luego tenemos que guardar todas las cosas en nuestra tabla -KGMonitoring-
            IEnumerable<Accounting> coleccionAccounting = entityDto.KgMonitoringDtos.Select(item =>
            {

                KgMonitoring kgMonitoring = new KgMonitoring()
                {
                    KgMonitoring_KGStandard = item.kg_standard,
                    KgMonitoring_Total = (item.cantidad_dto * item.kg_standard),
                    KgMonitoring_priceUnit = item.price_dto,
                    KgMonitoring_priceTotal = (item.cantidad_dto * item.price_dto),
                    RawMaterial = rawMaterial
                };

                Accounting accounting = new Accounting()
                {
                    Accounting_egreso = kgMonitoring.KgMonitoring_priceTotal,
                    KgMonitoring = kgMonitoring
                };

                return accounting;
            });

            // luego tenemos que guardar en nuestro -Accounting- para registrar los egresos realizados.
            _unitOfWork.Accounting.CreateAll(coleccionAccounting);


            _unitOfWork.Save();
            _unitOfWork.Dispose();

        }

        public RawMaterial Buscar(RawMaterialDto entityDto)
        {
            throw new NotImplementedException();
        }

        public Task<RawMaterial> Buscar(Guid id, string properties = "") => _unitOfWork.RawMaterialRepository.Buscar((item => item.RawMaterial_guid.Equals(id)), properties);

        public async Task DeleteImages(ICollection<DataImage> images)
        {

            string PathUbication = $"{Directory.GetCurrentDirectory()}\\FilesPublic\\ImageRawMaterial";

            foreach (DataImage image in images)
            {

                if (File.Exists($"{PathUbication}\\{image.Url}"))
                    File.Delete($"{PathUbication}\\{image.Url}");

                ImageRawMaterial imageRawMaterial = await _unitOfWork.ImageRawMaterialRepository.Buscar((item => item.ImageRawMaterial_guid.Equals(Guid.Parse(image.Identificador))));

                _unitOfWork.ImageRawMaterialRepository.Delete(imageRawMaterial);
            }

            _unitOfWork.Save();
            _unitOfWork.Dispose();
        }

        public async Task EditDataRawMaterial(RawMaterial rawMaterial)
        {
            this.Actualizar(rawMaterial);

            _unitOfWork.Save();
            _unitOfWork.Dispose();
        }

        public void Eliminar(RawMaterial entity)
        {
            throw new NotImplementedException();
        }

        public async Task<RawMaterialDetailsRequestDto> GetDetailesRawMaterial(Guid guid)
        {
            RawMaterial rawMaterial = await this.Buscar(guid, "ImageRawMaterials, KgMonitorings");
            RawMaterialDetailsRequestDto rawMaterialDetails = new RawMaterialDetailsRequestDto()
            {
                KgTotal = rawMaterial.RawMaterial_KgTotal,
                TotalCompras = rawMaterial.KgMonitorings.Count(),
                UltimaCompra = rawMaterial.KgMonitorings.OrderByDescending(item => item.KgMonitoring_id).First().KgMonitoring_priceTotal,
                imagenes =
                    rawMaterial.ImageRawMaterials.Any() ?

                    rawMaterial.ImageRawMaterials.Select(item =>
                    {
                        return new DataImage()
                        {
                            Identificador = item.ImageRawMaterial_guid.ToString(),
                            Url = item.ImageRawMaterial_url,
                            Estado = false
                        };
                    }) : [new DataImage() { Url = "default_icon.png" }]
            };


            return rawMaterialDetails;
        }

        public IEnumerable<RawMaterial> Obtener()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RawMaterial>> Obtener(int skip, string data) => await _unitOfWork.RawMaterialRepository.Buscar(skip, data);

        public async Task<ICollection<DataImage>> SaveImages(IEnumerable<IFormFile> formFiles, Guid guid)
        {
            ICollection<DataImage> datImages = new List<DataImage>();

            if (formFiles.Any())
            {
                if (formFiles.Count() > 5)
                    formFiles = formFiles.Take(5);

                RawMaterial rawMaterial = await this.Buscar(guid, "ImageRawMaterials");
                int totalImagenesFormFiles = formFiles.Count();
                int conteoImagenesTotal = Math.Abs(rawMaterial.ImageRawMaterials.Count() - 10);

                if (conteoImagenesTotal == 0)
                    throw new OperationAbortExceptions();


                if (totalImagenesFormFiles > conteoImagenesTotal)
                    formFiles = formFiles.Take(conteoImagenesTotal);

                ICollection<ImageRawMaterial> imagenesRawMaterial = new List<ImageRawMaterial>();

                string pathPartial = "\\FilesPublic\\ImageRawMaterial";

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

                    ImageRawMaterial imagenRawMaterial = new ImageRawMaterial()
                    {
                        ImageRawMaterial_guid = identificadorIMG,
                        ImageRawMaterial_url = NewFileName,
                        RawMaterial = rawMaterial
                    };
                    DataImage dataImage = new()
                    {
                        Identificador = imagenRawMaterial.ImageRawMaterial_guid.ToString(),
                        Url = imagenRawMaterial.ImageRawMaterial_url,
                        Estado = false
                    };
                    imagenesRawMaterial.Add(imagenRawMaterial);
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

                await _unitOfWork.ImageRawMaterialRepository.CreateAll(imagenesRawMaterial);

                _unitOfWork.Save();
                _unitOfWork.Dispose();
            }

            return datImages;
        }


    }
}
