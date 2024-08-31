namespace AtulaHomeFurniture.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
