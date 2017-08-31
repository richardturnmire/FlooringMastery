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
	public class OrderRemovalWorkflow : IWorkflow
	{
		OrderManager manager = DIContainer.Kernel.Get<OrderManager>();

		public void Execute()
		{
			Console.Clear();

			var orderInfo = DIContainer.Kernel.Get<OrderInfo>();

			orderInfo.OrderDate = Input.GetOrderDate();
			orderInfo.OrderNumber = Input.GetOrderNumber();

			OrderLookupResponse response = manager.LookupOrder(orderInfo);
			
			if ( response.Success )
			{
				ShowOrderandGetOkay(response.OrderInfo);
			}
			else
			{
				Output.SendToConsole($"Order {orderInfo.OrderNumber} was not found.");
			}

			Output.SendToConsole("\nPress any key to continue...");
			Console.ReadKey();

		}

		private void ShowOrderandGetOkay(OrderInfo orderInfo)
		{
			Output.SendToConsole(orderInfo);
			Output.SendToConsole();
			var oKay = Input.GetOkayToContinue("Do you want to delete this order? (Y)es or (N)o");
			if ( oKay )
			{
				var fileresponse = manager.Remove(orderInfo);
				Output.SendToConsole();
				if ( fileresponse.Success )
				{
					Output.SendToConsole($"Your order {fileresponse.OrderInfo.OrderNumber} has been removed!\n");
					Output.SendToConsole();
					Output.SendToConsole("--------------------------------------------------");
					Output.SendToConsole();
				}
				else
				{
					Output.SendToConsole("An error has occurred removing the order.");
					Output.SendToConsole(fileresponse.Message);
					Output.SendToConsole("\nAdditional infomation:\n");
					Output.SendToConsole(fileresponse.Error);
				}
			}
			else
			{
				Output.SendToConsole("Your order was not removed.\n");
			}
		}
	}
}

