namespace Shopping.Models
{
    public class Brand
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }

        public int Status { get; set; }
        public List<Product> Products { get; set; }
    }
}
