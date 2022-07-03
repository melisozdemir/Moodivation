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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();

            return Ok(products);
        }

        [HttpGet("/api/[controller]/GetAllByCategoryId/{categoryId}")]
        public async Task<IActionResult> GetAllByCategoryId(int categoryId)
        {
            var products = await _productService.GetAllByCategoryAsync(categoryId);

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetAsync(id);

            if (product.ResultStatus == ResultStatus.Success)
                return Ok(product);
            else
                return BadRequest(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto productCreateDto)
        {
            var add = await _productService.AddAsync(productCreateDto);

            if (add.ResultStatus == ResultStatus.Success)
                return Ok(add);
            else
                return BadRequest(add);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            var update = await _productService.UpdateAsync(productUpdateDto);

            if (update.ResultStatus == ResultStatus.Success)
                return Ok(update);
            else
                return BadRequest(update);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _productService.DeleteAsync(id);

            if (delete.ResultStatus == ResultStatus.Success)
                return Ok(delete);
            else
                return BadRequest(delete);
        }
    }
}
