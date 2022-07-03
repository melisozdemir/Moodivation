using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moodivation.Entities.Dtos;
using Moodivation.Services.Abstract;
using Moodivation.Shared.Utilities.Results.ComplexTypes;
using System.Threading.Tasks;

namespace Moodivation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetAsync(id);

            if (category.ResultStatus == ResultStatus.Success)
                return Ok(category);
            else
                return BadRequest(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDto categoryCreateDto)
        {
            var add = await _categoryService.AddAsync(categoryCreateDto);

            if (add.ResultStatus == ResultStatus.Success)
                return Ok(add);
            else
                return BadRequest(add);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            var update = await _categoryService.UpdateAsync(categoryUpdateDto);

            if (update.ResultStatus == ResultStatus.Success)
                return Ok(update);
            else
                return BadRequest(update);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _categoryService.DeleteAsync(id);

            if (delete.ResultStatus == ResultStatus.Success)
                return Ok(delete);
            else
                return BadRequest(delete);
        }
    }
}
