using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AtulaHomeFurniture.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using FluentValidation;
using AtulaHomeFurniture.Data;

namespace AtulaHomeFurniture.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IValidator<Product> _validator;

        public EditModel(ApplicationDbContext context, IValidator<Product> validator)
        {
            _context = context;
            _validator = validator;
        }

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public List<int> SelectedCategoryIds { get; set; } = new List<int>();

        public List<Category> Categories { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _context.Products
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Product == null)
            {
                return NotFound();
            }

            Categories = await _context.Categories.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Validate the Product using FluentValidation
            var validationResult = await _validator.ValidateAsync(Product);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }
                return Page();
            }

            try
            {
                var productToUpdate = await _context.Products
                    .Include(p => p.Categories)
                    .FirstOrDefaultAsync(p => p.Id == Product.Id);

                if (productToUpdate == null)
                {
                    return NotFound();
                }

                // Update properties
                productToUpdate.Name = Product.Name;
                productToUpdate.Sku = Product.Sku;
                // Load the selected categories based on the selected IDs
                productToUpdate.Categories = await _context.Categories
                    .Where(c => SelectedCategoryIds.Contains(c.Id))
                    .ToListAsync();
                _context.Attach(productToUpdate).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Product updated successfully!";
                return RedirectToPage("./Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
