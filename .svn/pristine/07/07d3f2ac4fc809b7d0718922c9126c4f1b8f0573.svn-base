using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WareHousemanagementSystemForClient.Interfaces.Interfaces;

namespace WareHouseManagementSystemForClient.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProdutCategory()
        {
            try
            {
                var productCategories = await _productRepository.GetAllProductCategory();
                if (productCategories == null)
                {
                    return NotFound();
                }
                return Ok(productCategories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
