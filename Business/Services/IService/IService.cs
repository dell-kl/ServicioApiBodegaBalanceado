namespace Business.Services.IService
{
    public interface IService<T, J> where T: class where J : class
    {
        public IEnumerable<J> Obtener();
        public Task<IEnumerable<J>> Obtener(int skip, string data);

        public void Agregate(T entityDto);

        public J Buscar(T entityDto);

        public Task<J> Buscar(Guid id);
    
        public void Actualizar(T datos, J entity);

        public void Actualizar(J entity);

        public void Eliminar(J entity);
    }
}
