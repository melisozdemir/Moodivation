using AutoMapper;
using Moodivation.Data.Abstract;
using Moodivation.Entities.Concrete;
using Moodivation.Entities.Dtos;
using Moodivation.Services.Abstract;
using Moodivation.Services.Utilities;
using Moodivation.Shared.Utilities.Results.Abstract;
using Moodivation.Shared.Utilities.Results.ComplexTypes;
using Moodivation.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodivation.Services.Concrete
{
    public class ProductManager : ManagerBase, IProductService
    {
        public ProductManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<IDataResult<ProductListDto>> GetAllAsync()
        {
            var products = await UnitOfWork.Products.GetAllAsync(x => x.IsActive, x => x.Category);

            if (products.Count > -1)
            {
                return new DataResult<ProductListDto>(ResultStatus.Success, new ProductListDto
                {
                    Products = products,
                });
            }
            return new DataResult<ProductListDto>(ResultStatus.Error, "", new ProductListDto
            {
                Products = null,
            });
        }

        public async Task<IDataResult<ProductListDto>> GetAllByCategoryAsync(int categoryId)
        {
            var products = await UnitOfWork.Products.GetAllAsync(x => x.IsActive && x.CategoryId == categoryId, x => x.Category);

            if (products.Count > -1)
            {
                return new DataResult<ProductListDto>(ResultStatus.Success, new ProductListDto
                {
                    Products = products,
                });
            }
            return new DataResult<ProductListDto>(ResultStatus.Error, "", new ProductListDto
            {
                Products = null,
            });
        }

        public async Task<IDataResult<ProductDto>> GetAsync(int productId)
        {
            var product = await UnitOfWork.Products.GetAsync(x => x.Id == productId && x.IsActive, x => x.Category);

            if (product != null)
            {
                return new DataResult<ProductDto>(ResultStatus.Success, "", new ProductDto
                {
                    Product = product
                });
            }

            return new DataResult<ProductDto>(ResultStatus.Error, Messages.Product.NotFound(isPlural: false), null);
        }

        public async Task<IDataResult<ProductDto>> AddAsync(ProductCreateDto productCreateDto)
        {
            var category = await UnitOfWork.Categories.GetAsync(x => x.Id == productCreateDto.CategoryId && x.IsActive);

            if (category == null)
                return new DataResult<ProductDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: false), null);

            var product = Mapper.Map<Product>(productCreateDto);

            var added = await UnitOfWork.Products.AddAsync(product);
            await UnitOfWork.SaveAsync();

            added.Category = category;

            return new DataResult<ProductDto>(ResultStatus.Success, Messages.Product.Add(product.Name), new ProductDto
            {
                Product = added
            });
        }

        public async Task<IResult> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            var category = await UnitOfWork.Categories.GetAsync(x => x.Id == productUpdateDto.CategoryId && x.IsActive);

            if (category == null)
                return new Result(ResultStatus.Error, Messages.Category.NotFound(isPlural: false), null);

            var oldProduct = await UnitOfWork.Products.GetAsync(x => x.Id == productUpdateDto.Id);

            if (oldProduct == null)
                return new Result(ResultStatus.Error, Messages.Product.NotFound(isPlural: false));

            var product = Mapper.Map<Product>(productUpdateDto);

            var updated = await UnitOfWork.Products.UpdateAsync(product);
            await UnitOfWork.SaveAsync();

            return new Result(ResultStatus.Success, Messages.Product.Update(product.Name));
        }

        public async Task<IResult> DeleteAsync(int productId)
        {
            var product = await UnitOfWork.Products.GetAsync(x => x.Id == productId);

            if (product != null)
            {
                product.IsActive = false;
                product.UpdatedDate = DateTime.Now;

                var deleted = await UnitOfWork.Products.UpdateAsync(product);
                await UnitOfWork.SaveAsync();

                return new Result(ResultStatus.Success, Messages.Product.Delete(product.Name));
            }

            return new Result(ResultStatus.Error, Messages.Product.NotFound(isPlural: false));
        }
    }
}
