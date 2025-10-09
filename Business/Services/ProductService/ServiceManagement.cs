using Business.Services.IService;
using Data.Repository.IRepository;

namespace Business.Services.ProductService
{
    public class ServiceManagement : IServiceManagement
    {

        public IRawMaterialService _RawMaterialService { get; }

        public readonly IUnitOfWork _unitOfWork;

        public ServiceManagement(IUnitOfWork unitOfWork) {

            _unitOfWork = unitOfWork;

            _RawMaterialService = new RawMaterialService(_unitOfWork);
        }
    }
}
