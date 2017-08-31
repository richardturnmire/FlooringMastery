using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;

namespace FlooringMastery.UI
{
    partial class Output
    {
        public static void SendToConsole(OrderInfo orderInfo)
        {
            Console.WriteLine("******************************************************************");
            Console.WriteLine($"Order          : {orderInfo.Order.OrderNumber,6} \t\t\t Date: {orderInfo.OrderDate:MM-dd-yyyy}");
            Console.WriteLine();
            Console.WriteLine($"Customer Name  : {orderInfo.Order.CustomerName}");
            Console.WriteLine($"State          : {orderInfo.Order.State}");
            Console.WriteLine();
            Console.WriteLine($"Product        : { orderInfo.Order.ProductType}");
            Console.WriteLine();
            Console.WriteLine($"Area (sq. ft.) : {orderInfo.Order.Area}");
            Console.WriteLine();
            Console.WriteLine($"\tMaterials  : {orderInfo.Order.MaterialCost,10:c}");
            Console.WriteLine($"\tLabor      : {orderInfo.Order.LaborCost,10:c}");
            Console.WriteLine($"\tTax        : {orderInfo.Order.Tax,10:c}");
            Console.WriteLine($"\tTotal      : {orderInfo.Order.Total,10:c}");
            Console.WriteLine("******************************************************************");
            Console.WriteLine();

        }

		public static void SendToConsole(Exception ex)
		{
			if (ex != null ) Console.WriteLine(ex.ToString());
		}

		public static void SendToConsole(string outputString)
		{
			Console.WriteLine(outputString);
		}

		public static void SendToConsole()
		{
			Console.WriteLine();
		}
	}
}
