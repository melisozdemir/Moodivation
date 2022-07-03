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
    public class CategoryManager : ManagerBase, ICategoryService
    {
        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<IDataResult<CategoryListDto>> GetAllAsync()
        {
            var categories = await UnitOfWork.Categories.GetAllAsync(x => x.IsActive);

            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "", new CategoryListDto
            {
                Categories = null,
            });
        }

        public async Task<IDataResult<CategoryDto>> GetAsync(int categoryId)
        {
            var category = await UnitOfWork.Categories.GetAsync(x => x.Id == categoryId && x.IsActive);
            if (category != null)
            {
                return new DataResult<CategoryDto>(ResultStatus.Success, new CategoryDto
                {
                    Category = category
                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: false), null);
        }

        public async Task<IDataResult<CategoryDto>> AddAsync(CategoryCreateDto categoryCreateDto)
        {
            var category = Mapper.Map<Category>(categoryCreateDto);

            var added = await UnitOfWork.Categories.AddAsync(category);
            await UnitOfWork.SaveAsync();

            return new DataResult<CategoryDto>(ResultStatus.Success, Messages.Category.Add(category.Name), new CategoryDto
            {
                Category = added
            });
        }

        public async Task<IResult> UpdateAsync(CategoryUpdateDto categoryUpdateDto)
        {
            var oldCategory = await UnitOfWork.Categories.GetAsync(x => x.Id == categoryUpdateDto.Id && x.IsActive);

            if (oldCategory == null)
                return new Result(ResultStatus.Error, Messages.Category.NotFound(isPlural: false));

            var category = Mapper.Map<Category>(categoryUpdateDto);

            var updated = await UnitOfWork.Categories.UpdateAsync(category);
            await UnitOfWork.SaveAsync();

            return new Result(ResultStatus.Success, Messages.Category.Update(category.Name));
        }

        public async Task<IResult> DeleteAsync(int categoryId)
        {
            var category = await UnitOfWork.Categories.GetAsync(x => x.Id == categoryId && x.IsActive);

            if (category != null)
            {
                category.IsActive = false;
                category.UpdatedDate = DateTime.Now;

                var deleted = await UnitOfWork.Categories.UpdateAsync(category);
                await UnitOfWork.SaveAsync();

                return new Result(ResultStatus.Success, Messages.Category.Delete(category.Name));
            }

            return new Result(ResultStatus.Error, Messages.Category.NotFound(isPlural:false));
        }
    }
}
