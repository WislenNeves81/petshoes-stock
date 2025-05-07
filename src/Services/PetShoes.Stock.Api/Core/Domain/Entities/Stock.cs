using System;
using Marraia.MongoDb.Core;

namespace PetShoes.Stock.Api.Core.Domain.Entities
{
    public class Stock : Entity<Guid>
    {
        public Stock(){}

        public Guid ProductId { get; set; } 
        public int Size { get; set; } 
        public int Quantity { get; set; } 
        public bool Active { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public Stock(Guid productId, 
                        int size, 
                        int quantity)
        {
            ProductId = productId;
            Size = size;
            Quantity = quantity;

            SetDefaultValues();
        }
        public void Update(int size, 
                           int quantity)
        {
            Size = size;
            Quantity = quantity;
            UpdatedAt = DateTime.Now;
        }
        private void SetDefaultValues()
        {
            Id = Guid.NewGuid();
            Active = true;
        }
       
        public void UpdateQuantity(int quantity)
        {
            Quantity = quantity;
            UpdatedAt = DateTime.Now;
        }
    }
}
