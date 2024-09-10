using ProductApi.Application.Interfaces;
using ProductApi.Infrastructure.Persistence;
using ProductApi.Infrastructure.Repositories;
namespace ProductApi.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ApplicationDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        private ApplicationDbContext _databaseContext;
        public bool IsDisposed { get; protected set; }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
            {
                return;
            }
            if (disposing)
            {

                if (_databaseContext != null)
                {
                    _databaseContext.Dispose();
                    _databaseContext = null;
                }
            }
            IsDisposed = true;
        }
        public async Task SaveAsync()
        {
            await _databaseContext.SaveChangesAsync();
        }
        private IProductRepository _productRepository;
        //with lazy loadingg
        private ICategoryRepository _categoryRepository;
        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_databaseContext);
                }

                return _productRepository;
            }
        }
        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                {
                    _categoryRepository = new CategoryRepository(_databaseContext);
                }

                return _categoryRepository;
            }
        }
        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
