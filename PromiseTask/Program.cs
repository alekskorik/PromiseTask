using PromiseTask;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        OrderManager orderManager = new OrderManager();

        while (true)
        {
            Console.WriteLine("Order Management System");
            Console.WriteLine("1. Create Sample Order");
            Console.WriteLine("2. Move Order to Warehouse");
            Console.WriteLine("3. Move Order to Shipping");
            Console.WriteLine("4. View Orders");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    orderManager.CreateOrder();
                    break;
                case "2":
                    orderManager.MoveToWarehouse();
                    break;
                case "3":
                    await orderManager.MoveToShipping();
                    break;
                case "4":
                    orderManager.ViewOrders();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.\n");
                    break;
            }
        }
    }
}
