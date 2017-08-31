using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using Ninject;

namespace FlooringMastery.UI.Workflows
{
	public class OrderListWorkflow : IWorkflow
	{
		public void Execute()
		{
			OrderManager manager = DIContainer.Kernel.Get<OrderManager>();
			var tempOrderInfo = DIContainer.Kernel.Get<OrderInfo>();

			Console.Clear();

			var orderDate = Input.GetOrderDate();
			
			tempOrderInfo = DIContainer.Kernel.Get<OrderInfo>();

			tempOrderInfo.OrderDate = orderDate;

			OrderListResponse response = manager.ListOrders(tempOrderInfo);
			 
			var line = "{0, 6} {1,-35} {2} {3,-20} {4, 5} {5, 10:c}";

			

			if ( response.Success )
			{
				Output.SendToConsole();
				Output.SendToConsole(String.Format(line, "Order", "Customer", "St", "Product", "Area", "Total"));
				Output.SendToConsole();
				foreach (var order in response.Orders)
				Output.SendToConsole(String.Format(line, order.OrderNumber, order.CustomerName, order.State, order.ProductType, order.Area, order.Total));
			}
			else
			{
				Output.SendToConsole("\nNo orders found");
			}

			Output.SendToConsole("\nPress any key to continue...");
			Console.ReadKey();
			;
		}
	}
}
