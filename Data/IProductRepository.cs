
using FlennikenJack_UT.Models;


namespace FlennikenJack_UT.Data
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        void AddProduct(Product product);
        
    }
}
  

