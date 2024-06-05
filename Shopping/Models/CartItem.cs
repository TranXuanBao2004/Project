namespace Shopping.Models
{
    public class CartItem
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity{ get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public double Total { 
            get
            {
                return Quantity * Price;
            }
        }
        public CartItem()
        {
            
        }
        public CartItem(Product product)
        {
            this.ProductId = product.Id;
            this.ProductName = product.Name;
            this.Price = product.Price;
            this.Quantity = 1;
            this.Image = product.Images;

        }
    }
}
