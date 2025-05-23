﻿namespace PetShoes.Stock.Api.Core.Application.AppStock.Input
{
    public class StockInput
    {
        public StockInput(Guid productId, 
                            string color, 
                            string size, 
                            int quantity, 
                            decimal price)
        {
            ProductId = productId;
            Color = color;
            Size = size;
            Quantity = quantity;
            Price = price;
        }
        public Guid ProductId { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
