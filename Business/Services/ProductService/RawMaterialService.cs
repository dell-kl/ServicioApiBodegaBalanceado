using Business.Services.IService;
using Data.Repository.IRepository;
using Domain;
using Domain.DTO;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Encodings.Web;

namespace Business.Services.ProductService
{
    public class RawMaterialService : IRawMaterialService
    {
        private readonly IUnitOfWork _unitOfWork;


        public RawMaterialService(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public void Actualizar(RawMaterialDto datos, RawMaterial entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(RawMaterial entity)
        {
            throw new NotImplementedException();
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
            IEnumerable<Accounting> coleccionAccounting = entityDto.KgMonitoringDtos.Select(item => {

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

        public Task<RawMaterial> Buscar(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(RawMaterial entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RawMaterial> Obtener()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RawMaterial>> Obtener(int skip, string data)
        {
            return await _unitOfWork.RawMaterialRepository.Buscar(skip, data);
        }

        public async Task SaveImages(IEnumerable<IFormFile> formFiles, Guid guid)
        {
            if ( formFiles.Any())
            {
                if (formFiles.Count() > 5)
                    formFiles = formFiles.Take(5);

                RawMaterial rawMaterial = await _unitOfWork.RawMaterialRepository.Buscar( (item => item.RawMaterial_guid.Equals(guid)) );
                ICollection<ImageRawMaterial> imagenesRawMaterial = new List<ImageRawMaterial>();

                string PathUbication = $"{Directory.GetCurrentDirectory()}\\FilesPublic\\ImageRawMaterial";

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
                    imagenesRawMaterial.Add(imagenRawMaterial);
                    
                    if ( formFile.Length <= 3145728 && ExtensionAllowd.Contains(Extension) )
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
        }

       
    }
}
