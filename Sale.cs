using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    using System;
    using System.Linq;

    public class Sale
    {
        public String Item { get; set; }
        public String Customer { get; set; }
        public double PricePerItem { get; set; }
        public int Quantity { get; set; }
        public String Address { get; set; }
        public bool ExpeditedShipping { get; set; }

        public static void Main(String[] args)
        {
            Sale[] Sales = new Sale[10];
            //1
            var priceOverTen =
                from aSale in Sales
                where aSale.PricePerItem > 10
                select aSale;

            var expensiveSales = Sales.Where(s => s.PricePerItem > 10.0);

            //2
            var quantityIsOne =
                from aSale in Sales
                where aSale.Quantity == 1
                orderby aSale.PricePerItem descending
                select aSale;

            var singleSales = Sales.Where(s => s.Quantity == 1).OrderByDescending(s => s.PricePerItem);
            //3
            var teaWithoutExpeditedShipping =
                from aSale in Sales
                where aSale.Item == "Tea" && !aSale.ExpeditedShipping
                select aSale;

            var teaNoExpeditedShipping = Sales.Where(s => s.Item == "Tea").Where(s => !s.ExpeditedShipping);

            //4
            var addressesWhereCostOfTotalOrderIsOver100 =
                from aSale in Sales
                where aSale.PricePerItem * aSale.Quantity > 100
                select aSale.Address;

            var addresses = Sales.Where(s => (s.PricePerItem * s.Quantity > 100.00)).Select(s => s.Address);

            //5
            var theSales =
                from aSale in Sales
                where aSale.Customer.Contains("LLC") || aSale.Customer.Contains("llc")
                select new
                {
                    aSale.Item,
                    totalPrice = aSale.PricePerItem * aSale.Quantity,
                    shipping = aSale.Address + "EXPEDITE"
                } into newSales
                orderby newSales.totalPrice
                select newSales;

            var llcCustomers = Sales.Where(s => s.Customer.Contains("llc") ||
                    s.Customer.Contains("LLC")).Select(s => new
                    {
                        s.Item,
                        totalPrice = (s.PricePerItem * s.Quantity),
                        shipping = s.Address + (s.ExpeditedShipping ? "Expedite" : "")
                    }).OrderBy(s => s.totalPrice);


        }
    }

}
