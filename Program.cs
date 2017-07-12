using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            Sale[] sales = new Sale[10];

            totalProfit(sales, s => s.Item == "Coffee",
                s => s.PricePerItem * s.Quantity * .8,
                (Sale s, double d) => Console.Out.WriteLine("Coffee Item for " +  s.Customer + ", total profit: " + d),
                (Sale s) => Console.Out.WriteLine("Non - Coffee item, skipping"));

            totalProfit(sales, s => s.Quantity > 1,
                s => s.ExpeditedShipping ? s.PricePerItem * s.Quantity + 20 : s.PricePerItem * s.Quantity,
                (Sale s, double d) => Console.Out.WriteLine(s.ExpeditedShipping ? "Expedited shipping sale of" + s.Item + " - Extra $20 profit" : ""),
                (Sale s) => Console.Out.WriteLine("Single Order Item " + s.Item + ", Excluded"));
        }
        public static double totalProfit(Sale[] sales, Func<Sale, bool> include,
            Func<Sale, double> profit, Action<Sale, double> includedSaleProfit,
            Action<Sale> notIncludedSale)
        {
            double total = 0;

            for (int i = 0; i < sales.Length; i++) {
                if (include(sales[i]))
                {
                    double prof = profit(sales[i]);
                    includedSaleProfit(sales[i], prof);
                    total += prof;
                }
                else
                {
                    notIncludedSale(sales[i]);
                }
            }
            return total;
        }
        
    }
}
