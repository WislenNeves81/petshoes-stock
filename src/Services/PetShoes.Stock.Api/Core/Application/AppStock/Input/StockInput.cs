namespace PetShoes.Stock.Api.Core.Application.AppStock.Input
{
    public class StockInput
    {
        public StockInput(){}

        public StockInput(Guid productId, 
                            int size, 
                            int quantity)
        {
            ProductId = productId;
            Size = size;
            Quantity = quantity;
        }
        public Guid ProductId { get; set; }
        public int Size { get; set; }
        public int Quantity { get; set; }
    }
}
