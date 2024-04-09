using Lab1.Models;

namespace Lab1.Repository
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();

		Category GetById(int id);

		Category GetByName(string name);

		void Add(Category category);

		bool Update(int id,Category category);

		bool Delete(int id);

	}
}