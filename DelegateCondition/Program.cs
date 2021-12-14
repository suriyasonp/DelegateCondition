using System;
using System.Collections.Generic;
using System.Linq;

namespace DelegateCondition
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var order = new List<Order>()
            {
                new Order()
                {
                    ProductName = "Apple",
                    ItemPrice = 30.50,
                    Quantity = 2
                },
                new Order()
                {
                    ProductName = "Kiwi",
                    ItemPrice = 50.25,
                    Quantity = 12
                },
                new Order()
                {
                    ProductName = "Watermalon",
                    ItemPrice = 15.75,
                    Quantity = 10
                }
            };

            var totalPrice = order.Sum(o => o.Quantity * o.ItemPrice);

            var discountPrice = CalculateTotalAfterDiscount(totalPrice);

            Console.WriteLine("***********Your purchased items************");

            foreach (var item in order)
            {
                Console.WriteLine(item.ProductName + ", Price/unit:" + item.ItemPrice + ", Quantity: " + item.Quantity + ", Total: " + item.ItemPrice * item.Quantity);
            }
            Console.WriteLine("*******************************************");
            Console.WriteLine("Total before discount: " + totalPrice);
            Console.WriteLine("Discount: " + (totalPrice - discountPrice));
            Console.WriteLine("Net price: " + discountPrice);
            Console.WriteLine("*******Thank you for purchasing************");
            Console.ReadKey();
        }

        private static double CalculateTotalAfterDiscount(double total)
        {
            return SetDelegateCondition().First(s => s.predicate(total)).action(total);
        }

        private static IList<(Predicate<double> predicate, Func<double, double> action)> SetDelegateCondition()
        {
            var cond = new List<(Predicate<double> predicate, Func<double, double> action)>
            {
                (
                (totalPrice) => totalPrice >= 50 && totalPrice < 100,
                (totalPrice) => totalPrice - (totalPrice * 2) / 100
                ),
                (
                (totalPrice) => totalPrice >= 100 && totalPrice < 500,
                (totalPrice) => totalPrice - (totalPrice * 5) / 100
                ),
                (
                (totalPrice) => totalPrice >= 500,
                (totalPrice) => totalPrice - (totalPrice * 10) / 100
                ),
                (
                (totalPrice) => true,
                (totalPrice) => totalPrice
                )
            };

            return cond;
        }
    }
}