namespace ProductApi.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public bool IsDisposed { get; }
        public Task SaveAsync();
        IProductRepository ProductRepository { get; }
    }
}