using FlennikenJack_UT.Data;
using FlennikenJack_UT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlennikenJack_UT.Pages
{
    public class ProductsModel : PageModel
    {
        [BindProperty]
        public List<Product> ProductList { get; set; }

        private readonly IProductRepository productRepository;

        public ProductsModel(IProductRepository prodRepos)
        {
            productRepository = prodRepos;
        }
        public void OnGet(int ProductID)
        {
            ProductList = productRepository.GetAllProducts();

        }
    }
}
