using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AtulaHomeFurniture.Models;
using Microsoft.EntityFrameworkCore;
using AtulaHomeFurniture.Data;

namespace AtulaHomeFurniture.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; } = new Product();

        // Property to bind the selected category IDs
        [BindProperty]
        public List<int> SelectedCategoryIds { get; set; } = new List<int>();

        public List<Category> Categories { get; set; }

        public async Task OnGetAsync()
        {
            Categories = await _context.Categories.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Categories = await _context.Categories.ToListAsync();
                return Page();
            }

            // Load the selected categories based on the selected IDs
            Product.Categories = await _context.Categories
                .Where(c => SelectedCategoryIds.Contains(c.Id))
                .ToListAsync();



            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Product created successfully!";
            return RedirectToPage("Index"); // Redirect to a page listing products
        }
    }
}
