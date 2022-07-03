using Moodivation.Entities.Dtos;
using Moodivation.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moodivation.Services.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<CategoryListDto>> GetAllAsync();
        Task<IDataResult<CategoryDto>> GetAsync(int categoryId);
        Task<IDataResult<CategoryDto>> AddAsync(CategoryCreateDto categoryAddDto);
        Task<IResult> UpdateAsync(CategoryUpdateDto categoryUpdateDto);
        Task<IResult> DeleteAsync(int categoryId);
    }
}
