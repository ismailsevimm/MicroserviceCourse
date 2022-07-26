using MSCourse.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSCourse.Services.Order.Domain.OrderAggregate
{
    /// <summary>
    /// Ef Core Features
    /// - Owned Types
    /// - Shadow Property
    /// - Backing Field
    /// </summary>
    public class Order : EntityBase, IAggregateRoot
    {
        public DateTime CreatedTime { get; private set; }

        public Address Address { get; private set; }

        public string BuyerId { get; private set; }

        public decimal GetTotalPrice => _orderItems.Sum(x=>x.Price);

        //Backing Field
        private readonly List<OrderItem> _orderItems;

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order()
        {

        }

        public Order(string buyerId, Address address)
        {
            _orderItems = new List<OrderItem>();
            CreatedTime = DateTime.Now;
            BuyerId = buyerId;
            Address = address;
        }

        public void AddOrderItem(string productId, string productName, string pictureUrl, decimal price)
        {
            var existsProduct = _orderItems.Any(x=>x.ProductId == productId);

            if (!existsProduct)
            {
                var newItem = new OrderItem(productId, productName, pictureUrl, price);

                _orderItems.Add(newItem);
            }
        }
    }
}
