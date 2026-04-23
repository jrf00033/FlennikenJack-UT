using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FlennikenJack_UT.Data;
using FlennikenJack_UT.Models;
using System.ComponentModel;

namespace FlennikenJack_UT.Pages
{
    public class AddProductModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public AddProductModel(IProductRepository productRepository)//constuctor
        {
            _productRepository = productRepository;
        }
        [BindProperty]
        public Product product { get; set; }

        public void OnGet()
        {
        }
            public IActionResult OnPost()
        {
            _productRepository.AddProduct(product); //creates product in the product table

            return RedirectToPage("/Index");
        }
    }

}

