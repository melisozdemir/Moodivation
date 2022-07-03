using Moodivation.Entities.Dtos;
using Moodivation.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodivation.Services.Abstract
{
    public interface IProductService
    {
        Task<IDataResult<ProductListDto>> GetAllAsync();
        Task<IDataResult<ProductListDto>> GetAllByCategoryAsync(int categoryId);
        Task<IDataResult<ProductDto>> GetAsync(int productId);
        Task<IDataResult<ProductDto>> AddAsync(ProductCreateDto productCreateDto);
        Task<IResult> UpdateAsync(ProductUpdateDto productUpdateDto);
        Task<IResult> DeleteAsync(int productId);
    }
}
