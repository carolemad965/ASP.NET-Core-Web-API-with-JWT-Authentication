using Lab1.Models;

namespace Lab1.DTO
{
    public class CatID_Name_ProductsDTO
    {
        public int Id {  get; set; }
        public string Name { get; set;}

        public List<string> productsName { get; set; }
    }
}
