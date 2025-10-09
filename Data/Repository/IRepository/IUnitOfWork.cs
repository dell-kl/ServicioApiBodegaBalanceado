namespace Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        public IRawMaterialRepository RawMaterialRepository { get; }
        public IKGMonitoring KgMonitoring { get; }
        public IAccounting Accounting { get; }
        public IImageRawMaterial ImageRawMaterialRepository { get; } // Agregado
        public void Save();
    }
}
