using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromiseTask
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string ProductName { get; set; }
        public string CustomerType { get; set; } 
        public string DeliveryAddress { get; set; }
        public string PaymentMethod { get; set; } 
        public OrderStatus Status { get; set; }

        public Order(int id, decimal amount, string productName, string customerType, string deliveryAddress, string paymentMethod)
        {
            Id = id;
            Amount = amount;
            ProductName = productName;
            CustomerType = customerType;
            DeliveryAddress = deliveryAddress;
            PaymentMethod = paymentMethod;
            Status = OrderStatus.New;
        }

        public void UpdateStatus(OrderStatus newStatus)
        {
            Status = newStatus;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Product: {ProductName}, Amount: {Amount}, Status: {Status}, Payment: {PaymentMethod}";
        }
    }
}
