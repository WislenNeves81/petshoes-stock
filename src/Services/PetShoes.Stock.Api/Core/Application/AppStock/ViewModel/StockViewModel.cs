namespace PetShoes.Stock.Api.Core.Application.AppStock.ViewModel
{
    public class StockViewModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
