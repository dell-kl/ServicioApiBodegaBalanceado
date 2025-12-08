using Data.Repository.IRepository;
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


    }
}