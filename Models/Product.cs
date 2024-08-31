namespace AtulaHomeFurniture.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }

        // Navigation property
        public List<Category> Categories { get; set; } = new();
    }
}
