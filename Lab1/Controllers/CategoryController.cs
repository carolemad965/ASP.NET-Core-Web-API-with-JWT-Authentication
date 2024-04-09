using Lab1.DTO;
using Lab1.Models;
using Lab1.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository _CategoryRepository)
        {
            categoryRepository = _CategoryRepository;
        }
        [HttpGet("{id:int}")]
        public IActionResult GetByID(int id)
        {
            Category? CategoryData = categoryRepository.GetById(id);
            if (CategoryData == null)
            {
                return NotFound();
            }
            CatID_Name_ProductsDTO CategoryDataModel = new CatID_Name_ProductsDTO {

                Id = id,
                Name = CategoryData.Name,
                productsName = CategoryData.products.Select(p => p.Name).ToList()
              };
            
            return Ok(CategoryDataModel);
            
        }
        [HttpGet]
        public IActionResult GetAll ()
        {
            List<Category> categories = categoryRepository.GetAll(); 
            if(categories == null || categories.Count == 0)
                return NotFound();
            return Ok(categories);
        }
		
		[HttpGet("{name:regex(^[[a-zA-Z0-9-]]+$)}")]
		public IActionResult GetByName(string name)
        {
            Category category = categoryRepository.GetByName(name);
            if(category == null) 
                return NotFound();
            return Ok(category);
        }
        [HttpPost]
        public IActionResult Add(CategoryID_NameDTO category)
        {
            Category NewCategory1 = (new Category
            {
                Id = category.Id,
                Name = category.Name
            });
            categoryRepository.Add(NewCategory1);
            return Ok();
        }
		[HttpPut]
		public IActionResult Update(int id, CategoryID_NameDTO category)
		{
            bool ISUpdated = true;
            Category categoryAfterUpdate = (new Category
            {
                Id=category.Id,
                Name = category.Name
            });
            ISUpdated=categoryRepository.Update( id, categoryAfterUpdate);
            if(ISUpdated)
            {
                return Ok();
            }
            return BadRequest();
		}
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (categoryRepository.Delete(id))
            {
                return Ok();
            }
            return BadRequest();
        }
	}
}
