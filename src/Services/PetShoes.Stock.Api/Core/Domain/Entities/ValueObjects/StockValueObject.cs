namespace PetShoes.Stock.Api.Core.Domain.Entities.ValueObjects
{
    public class StockValueObject
    {
        public StockValueObject() { }
        public StockValueObject(Guid productId,
                                Guid stockId,
                                int size,
                                int quantity)
        {
            ProductId = productId;
            Id = stockId;
            Size = size;
            Quantity = quantity;
        }
        public Guid ProductId { get; set; }
        public Guid Id { get; set; }
        public int Size { get; set; }
        public int Quantity { get; set; }
        public DateTime UpdatedAt { get; set; }

        public void UpdateQuantity(int quantity)
        {
            Quantity -= quantity;
            UpdatedAt = DateTime.Now;
        }
    }
}
