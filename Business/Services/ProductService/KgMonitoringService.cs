using Business.Services.IService;
using Data.Repository.IRepository;
using Domain;
using Domain.DTO;

namespace Business.Services.ProductService
{
    public class KgMonitoringService : IKGMonitoringService
    {
        private readonly IUnitOfWork _unitOfWork;

        public KgMonitoringService(IUnitOfWork unitOfWork) { 
            _unitOfWork = unitOfWork;
        }

        public void Actualizar(KGMonitoringDto datos, KgMonitoring entity)
        {
            throw new NotImplementedException();
        }

        public void Actualizar(KgMonitoring entity)
        {
            throw new NotImplementedException();
        }

        public void Agregate(KGMonitoringDto entityDto)
        {
            throw new NotImplementedException();
        }

        public KgMonitoring Buscar(KGMonitoringDto entityDto)
        {
            throw new NotImplementedException();
        }

        public Task<KgMonitoring> Buscar(Guid id, string properties = "")
        {
            throw new NotImplementedException();
        }

        public void Eliminar(KgMonitoring entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<KgMonitoring> Obtener()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<KgMonitoring>> Obtener(int skip, string data)
        {
            throw new NotImplementedException();
        }
    }
}
