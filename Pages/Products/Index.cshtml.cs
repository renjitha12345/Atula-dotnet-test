using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using AtulaHomeFurniture.Models;
using Microsoft.EntityFrameworkCore;
using AtulaHomeFurniture.Data;

namespace AtulaHomeFurniture.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; }

        public async Task OnGetAsync()
        {
            Products = await _context.Products
                .Include(p => p.Categories)
                .ToListAsync();
        }
    }
}
