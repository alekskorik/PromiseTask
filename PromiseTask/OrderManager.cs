﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromiseTask
{
    public class OrderManager
    {
        private List<Order> orders = new List<Order>();
        private int orderCounter = 1;

        public void CreateOrder()
        {
            try
            {
                Console.Write("Enter product name: ");
                string productName = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(productName))
                    throw new ArgumentException("Product name cannot be empty.");

                Console.Write("Enter order amount: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
                    throw new ArgumentException("Invalid amount. Must be a positive number.");

                string customerType;
                do
                {
                    Console.Write("Enter customer type (Company/Individual): ");
                    customerType = Console.ReadLine()?.Trim().ToLowerInvariant();

                    if (customerType == "company") customerType = "Company";
                    else if (customerType == "individual") customerType = "Individual";
                    else Console.WriteLine("Error: Please enter 'Company' or 'Individual'.");

                } while (customerType != "Company" && customerType != "Individual");

                string deliveryAddress;
                do
                {
                    Console.Write("Enter delivery address (Required): ");
                    deliveryAddress = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(deliveryAddress))
                        Console.WriteLine("Error: Delivery address cannot be empty.");

                } while (string.IsNullOrWhiteSpace(deliveryAddress));

                string paymentMethod;
                do
                {
                    Console.Write("Enter payment method (Card/Cash on Delivery): ");
                    paymentMethod = Console.ReadLine()?.Trim().ToLowerInvariant();

                    if (paymentMethod == "card") paymentMethod = "Card";
                    else if (paymentMethod == "cash on delivery") paymentMethod = "Cash on Delivery";
                    else Console.WriteLine("Error: Please enter 'Card' or 'Cash on Delivery'.");

                } while (paymentMethod != "Card" && paymentMethod != "Cash on Delivery");

                Order order = new Order(orderCounter++, amount, productName, customerType, deliveryAddress, paymentMethod);
                orders.Add(order);
                Console.WriteLine("Order created successfully!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }



        public void MoveToWarehouse()
        {
            Console.Write("Enter Order ID to move to warehouse: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Order order = orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                if (order.Amount >= 2500 && order.PaymentMethod == "Cash on Delivery")
                {
                    order.UpdateStatus(OrderStatus.ReturnedToCustomer);
                    Console.WriteLine("Order returned to customer due to cash on delivery policy.\n");
                }
                else
                {
                    order.UpdateStatus(OrderStatus.InWarehouse);
                    Console.WriteLine("Order moved to warehouse.\n");
                }
            }
            else
            {
                Console.WriteLine("Order not found.\n");
            }
        }

        public void MoveToWarehouse(int id)
        {
            try
            {
                Order order = orders.FirstOrDefault(o => o.Id == id);
                if (order == null) throw new InvalidOperationException("Order not found.");

                if (order.Amount >= 2500 && order.PaymentMethod == "Cash on Delivery")
                {
                    order.UpdateStatus(OrderStatus.ReturnedToCustomer);
                    Console.WriteLine("Order returned to customer due to cash on delivery policy.\n");
                }
                else
                {
                    order.UpdateStatus(OrderStatus.InWarehouse);
                    Console.WriteLine("Order moved to warehouse.\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        public async Task MoveToShipping()
        {
            Console.Write("Enter Order ID to move to shipping: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Order order = orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                if (string.IsNullOrEmpty(order.DeliveryAddress))
                {
                    order.UpdateStatus(OrderStatus.Error);
                    Console.WriteLine("Order has an error due to missing address.\n");
                }
                else
                {
                    order.UpdateStatus(OrderStatus.InShipping);
                    Console.WriteLine("Order is being shipped...");

                    await Task.Delay(5000); 
                    order.UpdateStatus(OrderStatus.Closed);
                    Console.WriteLine("Order has been shipped.\n");
                }
            }
            else
            {
                Console.WriteLine("Order not found.\n");
            }
        }

        public async Task MoveToShipping(int id)
        {
            try
            {
                Order order = orders.FirstOrDefault(o => o.Id == id);
                if (order == null) throw new InvalidOperationException("Order not found.");

                if (string.IsNullOrEmpty(order.DeliveryAddress))
                {
                    order.UpdateStatus(OrderStatus.Error);
                    throw new InvalidOperationException("Order has an error due to missing address.");
                }

                order.UpdateStatus(OrderStatus.InShipping);
                Console.WriteLine("Order is being shipped...");

                await Task.Delay(5000);
                order.UpdateStatus(OrderStatus.Closed);
                Console.WriteLine("Order has been shipped.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        public void ViewOrders()
        {
            if (orders.Count == 0)
            {
                Console.WriteLine("No orders found.\n");
                return;
            }

            foreach (var order in orders)
            {
                Console.WriteLine(order);
            }
            Console.WriteLine();
        }

        public void TestAddOrder(Order order)
        {
            orders.Add(order);
        }
    }
}
