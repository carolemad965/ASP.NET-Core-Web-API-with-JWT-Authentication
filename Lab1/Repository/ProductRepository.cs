using Lab1.Models;

namespace Lab1.Repository
{
    public class ProductRepository: IProductRepository
    {
        private readonly Context context;
        public ProductRepository(Context _context) { 
        context = _context;
        }
       public List<Product> GetAll()
        {
           List<Product> products = context.Products.ToList();
            return products;
        }
        public Product GetById(int id) { 
        Product? product = context.Products.FirstOrDefault(p => p.Id == id); 
            
            return product;
        }
        public List <Product> GetByName(string name)
        {
           List<Product > products = context.Products.Where(products => products.Name == name).ToList();
            return products;
            
        }
        public void Add(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }
        public bool Update(int id,Product product)
        {
            Product ProductFromDb=GetById(id);
            bool IsValid = true;
            if(ProductFromDb == null)
            {
                IsValid = false;
            }
            else if(ProductFromDb.Id != id) {

                IsValid = false;
            }

            if(IsValid)
            {
             ProductFromDb.Name= product.Name;
             ProductFromDb.Description= product.Description;
             ProductFromDb.Price= product.Price;
                context.SaveChanges();
            }
            return IsValid;
        }
        public bool Delete(int id)
        {
            Product ProductFromDB=GetById(id);
            if(ProductFromDB!=null)
            {
                context.Remove(ProductFromDB);
                context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
