using Moodivation.Data.Abstract;
using Moodivation.Data.Concrete.EntityFramework.Contexts;
using Moodivation.Data.Concrete.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodivation.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MoodivationContext _context;

        private EfCategoryRepository _categoryRepository;
        private EfProductRepository _productRepository;
        public UnitOfWork(MoodivationContext context)
        {
            _context = context;
        }
        public ICategoryRepository Categories => _categoryRepository ??= new EfCategoryRepository(_context);
        public IProductRepository Products => _productRepository ??= new EfProductRepository(_context);

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
