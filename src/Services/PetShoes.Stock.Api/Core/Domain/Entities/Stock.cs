using System;
using Marraia.MongoDb.Core;

namespace PetShoes.Stock.Api.Core.Domain.Entities
{
    public class Stock : Entity<Guid>
    {
        public Stock(){}

        public Guid ProductId { get; set; } 
        public string Color { get; set; } 
        public int Size { get; set; } 
        public int Quantity { get; set; } 
        public decimal Price { get; set; }
        public bool Active { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public Stock(Guid productId, 
                        string color, 
                        int size, 
                        int quantity, 
                        decimal price)
        {
            ProductId = productId;
            Color = color;
            Size = size;
            Quantity = quantity;
            Price = price;

            SetDefaultValues();
        }
        public void Update(string color, 
                           int size, 
                           int quantity, 
                           decimal price)
        {
            Color = color;
            Size = size;
            Quantity = quantity;
            Price = price;
            UpdatedAt = DateTime.Now;
        }
        private void SetDefaultValues()
        {
            Id = Guid.NewGuid();
            Active = true;
        }
        public void UpdatePrice(decimal price)
        {
            Price = price;
            UpdatedAt = DateTime.Now;
        }
        public void UpdateQuantity(int quantity)
        {
            Quantity = quantity;
            UpdatedAt = DateTime.Now;
        }
    }
}
