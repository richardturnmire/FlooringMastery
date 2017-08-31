using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using FlooringMastery.BLL;
using FlooringMastery.Models.Responses;
using FlooringMastery.Models;
using FlooringMastery.UI;


namespace FlooringMastery.UI.Workflows
{
	public class OrderLookupWorkflow : IWorkflow
	{
		public void Execute()
		{
			OrderManager manager = DIContainer.Kernel.Get<OrderManager>();
			var tempOrderInfo = DIContainer.Kernel.Get<OrderInfo>();

			Console.Clear();

			var orderDate = Input.GetOrderDate();
			var orderNumber = Input.GetOrderNumber();

			tempOrderInfo = DIContainer.Kernel.Get<OrderInfo>();

			tempOrderInfo.OrderDate = orderDate;
			tempOrderInfo.OrderNumber = orderNumber;

			OrderLookupResponse response = manager.LookupOrder(tempOrderInfo);

			if ( response.Success )
			{
				Output.SendToConsole(response.OrderInfo);
			}
			else
			{
				Output.SendToConsole("An error occurred:  \n");
				Output.SendToConsole(response.Message);
			}

			Output.SendToConsole("\nPress any key to continue...");
			Console.ReadKey();
			;
		}
	}
}
