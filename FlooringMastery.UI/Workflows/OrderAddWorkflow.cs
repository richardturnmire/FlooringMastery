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
	class OrderAddWorkflow : IWorkflow
	{
		public void Execute()
		{
			Console.Clear();

			OrderManager manager = DIContainer.Kernel.Get<OrderManager>();

			var states = manager.GetStates();
			var products = manager.GetProducts();

			var orderDate = Input.GetOrderDate();
			var customerName = Input.GetCustomerName();
			var state = Input.GetState(states.States);
			var product = Input.GetProductType(products.Products);
			var area = Input.GetArea();

			var tempOrder = DIContainer.Kernel.Get<Order>();

			tempOrder.OrderNumber = -1;
			tempOrder.CustomerName = customerName;
			tempOrder.State = state.StateAbbreviation;
			tempOrder.ProductType = product.ProductType;
			tempOrder.Area = area;
			tempOrder.TaxRate = state.TaxRate;
			tempOrder.CostPerSquareFoot = product.CostPerSquareFoot;
			tempOrder.LaborCostPerSquareFoot = product.LaborCostPerSquareFoot;

			var tempOrderInfo = DIContainer.Kernel.Get<OrderInfo>();

			tempOrderInfo.Order = tempOrder;
			tempOrderInfo.OrderDate = orderDate;

			OrderAddResponse response = manager.Add(tempOrderInfo);

			if ( response.Success )
			{
				Output.SendToConsole(response.OrderInfo);

				bool oKay = Input.GetOkayToContinue("Do you want to place the order (Y)es or (N)o? ");

				if ( oKay )
				{
					var fileresponse = manager.SaveOrder(response.OrderInfo);
					Output.SendToConsole();
					if ( fileresponse.Success )
					{
						var fileresponse2 = manager.SaveOrders(response.OrderInfo);

						if ( fileresponse2.Success )
						{
							Output.SendToConsole($"Your order {response.OrderInfo.OrderNumber} has been accepted!\n");
							Output.SendToConsole();
							Output.SendToConsole("--------------------------------------------------");
							Output.SendToConsole();
						}
						else
						{
							Output.SendToConsole("An error has occurred saving the order repository.");
							Output.SendToConsole(fileresponse2.Message);
							Output.SendToConsole("\nAdditional infomation:\n");
							Output.SendToConsole(fileresponse2.Error);
						}
					}
					else
					{
						Output.SendToConsole("An error has occurred saving the order.");
						Output.SendToConsole(fileresponse.Message);
						Output.SendToConsole("\nAdditional infomation:\n");
						Output.SendToConsole(fileresponse.Error);
					}
				}
				else
				{
					Output.SendToConsole("Your order has been cancelled.\n");
				}
			}
			else
			{
				Output.SendToConsole("An error has occurred saving the order.");
				Output.SendToConsole(response.Message);
			}

			Output.SendToConsole("\nPress any key to continue...");
			Console.ReadKey();
		}
	}
}
