using Lab1.DTO;
using Lab1.Models;
using Lab1.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
      public ProductController(IProductRepository _ProductRepository)
        {
            productRepository = _ProductRepository;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
           List<Product> products = productRepository.GetAll();
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetByID(int id)
        {
           Product product = productRepository.GetById(id);
            if(product == null)
            {
                return BadRequest("Can't find this product");
            }
            return Ok(product);
        }
		[HttpGet("{name:regex(^[[a-zA-Z0-9-]]+$)}")]
        public IActionResult GetByName(string name)
        {
            List<Product> products=productRepository.GetByName(name);
            if(products == null)
            {
                return BadRequest("Can't find this product");
            }
            return Ok(products);
        }

		[HttpPost]
        public IActionResult Add(ProductName_Description_Price_CatIDDTO NewProduct)
        {
            if (ModelState.IsValid == true)
            {
                Product product = (new Product
                {
                    Name=NewProduct.Name,
                    Price=NewProduct.Price,
                    Description=NewProduct.Description,
                    CategoryId=NewProduct.CategoryId
                });

                
                productRepository.Add(product);
                return CreatedAtAction("GetByID", new { id = product.Id }, product);
               
            }
            return BadRequest(ModelState);
        }
        [HttpPut]
        public IActionResult Update(int id,ProductName_Description_Price_CatIDDTO product)
        {
            Product UpdatedProduct = (new Product
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId=product.CategoryId
            });
            bool ISUpdated = productRepository.Update(id, UpdatedProduct);
            if (ISUpdated)
            {
                return NoContent();
            }
            return BadRequest("Can't Updated");
            
        }
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            bool IsDeleted = productRepository.Delete(id);
            if(IsDeleted)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Can't Deleted");
            }
        }



    }
}
