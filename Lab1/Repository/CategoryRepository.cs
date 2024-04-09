using Lab1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Lab1.Repository
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly Context context;
        private readonly IProductRepository productRepository;
        public CategoryRepository(Context _context,IProductRepository _ProductRepository) 
        { 
            context = _context;
            productRepository = _ProductRepository;
        }
		public List<Category> GetAll()
		{
			return context.Categories.ToList();
		}
		public Category GetById(int id)
        {
            Category? category = context.Categories.Include(c=>c.products).FirstOrDefault(c => c.Id == id);

            return category;
        }
        public Category GetByName(string name)
        {
            Category? category=context.Categories.FirstOrDefault(c => c.Name == name);
            return category;
        }
      public void Add(Category category)
        {
            context.Categories.Add(category);
			context.SaveChanges();
		}
        public bool Update(int id ,Category category)
        {
            Category categoryFromDB=GetById(id);
            if(categoryFromDB == null)
            {
                return false;

            }
            categoryFromDB.Name = category.Name;
            categoryFromDB.Id = id;
            context.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            Category category = context.Categories.FirstOrDefault(c=>c.Id == id);
            if(category == null )
            {
                return false;
            }
            context.Categories.Remove(category);
            context.SaveChanges();
            return true;
        }
	}
}
