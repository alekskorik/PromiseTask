
using PromiseTask;
using System;
using Xunit;


namespace OrderTests
{

    public class OrderTests
    {
        [Fact]
        public void CreateOrder_ShouldSetStatusToNew()
        {
            var order = new Order(1, 100, "Laptop", "Company", "123 Street", "Card");
            Assert.Equal(OrderStatus.New, order.Status);
        }

        [Fact]
        public void MoveToWarehouse_ShouldSetStatusToInWarehouse()
        {
            var order = new Order(1, 200, "Monitor", "Individual", "456 Avenue", "Card");
            var manager = new OrderManager();

            manager.TestAddOrder(order);
            manager.MoveToWarehouse(1);

            Assert.Equal(OrderStatus.InWarehouse, order.Status);
        }

        [Fact]
        public void MoveToWarehouse_ShouldReturnOrderIfCashOnDeliveryAboveThreshold()
        {
            var order = new Order(1, 3000, "PC", "Company", "789 Road", "Cash on Delivery");
            var manager = new OrderManager();

            manager.TestAddOrder(order);
            manager.MoveToWarehouse(1);

            Assert.Equal(OrderStatus.ReturnedToCustomer, order.Status);
        }

        [Fact]
        public async Task MoveToShipping_ShouldSetStatusToClosed_AfterDelay()
        {
            var order = new Order(1, 100, "Phone", "Company", "456 Avenue", "Card");
            var manager = new OrderManager();

            manager.TestAddOrder(order);
            await manager.MoveToShipping(1); 

            Assert.Equal(OrderStatus.Closed, order.Status);
        }


        [Fact]
        public void MoveToShipping_ShouldSetStatusToError_IfAddressMissing()
        {
            var order = new Order(1, 100, "Tablet", "Company", "", "Card");
            var manager = new OrderManager();

            manager.TestAddOrder(order);
            manager.MoveToShipping(1);

            Assert.Equal(OrderStatus.Error, order.Status);
        }
    }

}