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
	public class OrderEditWorkflow : IWorkflow
	{
		OrderManager manager = DIContainer.Kernel.Get<OrderManager>();

		public void Execute()
		{
			Console.Clear();



			var orderInfo = DIContainer.Kernel.Get<OrderInfo>();

			orderInfo.OrderDate = Input.GetOrderDate();
			orderInfo.OrderNumber = Input.GetOrderNumber();


			var states = manager.GetStates();
			var products = manager.GetProducts();


			OrderEditResponse response = manager.Edit(orderInfo);

			if ( response.Success )
			{
				string customerName = String.Empty;
				TaxInfo state;
				ProductInfo product;
				decimal area;
				bool dataChanged = false;

				Output.SendToConsole("Order before changes\n");
				Output.SendToConsole(response.OrderInfo);

				customerName = Input.GetCustomerName(response.OrderInfo.Order.CustomerName);
				if ( customerName != response.OrderInfo.Order.CustomerName )
				{
					response.OrderInfo.Order.CustomerName = customerName;
					dataChanged = true;
				}

				var stateExists = states.States.Exists(s => s.StateAbbreviation == response.OrderInfo.Order.State);
				if ( stateExists )
				{
					state = Input.GetState(response.OrderInfo.Order.State, states.States);
					if ( state != null )
					{
						if ( state.StateAbbreviation != response.OrderInfo.Order.State )
						{
							dataChanged = true;
							response.OrderInfo.Order.State = state.StateAbbreviation;
							response.OrderInfo.Order.TaxRate = state.TaxRate;
							response.OrderInfo.Recalculate = true;
						}
					}
				}
				else
				{
					if ( response.OrderInfo.Order.State.Length == 0)
					{
						state = Input.GetState(states.States);
						if ( state != null )
						{
							dataChanged = true;
							response.OrderInfo.Order.State = state.StateAbbreviation;
							response.OrderInfo.Order.TaxRate = state.TaxRate;
							response.OrderInfo.Recalculate = true;
						}
					}
				}

				var productExists = products.Products.Exists(p => p.ProductType == response.OrderInfo.Order.ProductType);
				if ( productExists )
				{
					product = Input.GetProductType(response.OrderInfo.Order.ProductType, products.Products);
					if ( product != null )
					{
						if ( product.ProductType != response.OrderInfo.Order.ProductType )
						{
							dataChanged = true;
							response.OrderInfo.Recalculate = true;
							response.OrderInfo.Order.ProductType = product.ProductType;
							response.OrderInfo.Order.LaborCostPerSquareFoot = product.LaborCostPerSquareFoot;
							response.OrderInfo.Order.CostPerSquareFoot = product.CostPerSquareFoot;
						}
					}
				}
				else
				{
					if ( response.OrderInfo.Order.ProductType == null )
					{
						product = Input.GetProductType(products.Products);
						if ( product != null )
						{
							dataChanged = true;
							response.OrderInfo.Recalculate = true;
							response.OrderInfo.Order.ProductType = product.ProductType;
							response.OrderInfo.Order.LaborCostPerSquareFoot = product.LaborCostPerSquareFoot;
							response.OrderInfo.Order.CostPerSquareFoot = product.CostPerSquareFoot;
						}
					}
				}

				area = Input.GetArea(response.OrderInfo.Order.Area);
				if ( area != response.OrderInfo.Order.Area )
				{
					dataChanged = true;
					response.OrderInfo.Recalculate = true;
				}

				if ( dataChanged )
				{
					ShowChangesandGetOkay(response.OrderInfo);
				}
				else
				{
					Output.SendToConsole("No changes made.");
				}

			}
			else
			{
				Output.SendToConsole("Error occurred getting order");
			}

			Output.SendToConsole("\nPress any key to continue...");
			Console.ReadKey();

		}

		private void ShowChangesandGetOkay(OrderInfo orderInfo)
		{
			Output.SendToConsole(orderInfo);
			Output.SendToConsole();
			var oKay = Input.GetOkayToContinue("Do you want to save these changes? (Y)es or (N)o: ");
			if ( oKay )
			{
				var fileresponse = manager.SaveOrder(orderInfo);
				Output.SendToConsole();
				if ( fileresponse.Success )
				{
					var fileresponse2 = manager.SaveOrders(orderInfo);

					if ( fileresponse2.Success )
					{
						Output.SendToConsole($"Your order {fileresponse.OrderInfo.OrderNumber} has been updated!\n");
						Output.SendToConsole();
						Output.SendToConsole("--------------------------------------------------");
						Output.SendToConsole();
					}
					else
					{
						Output.SendToConsole("An error has occurred saving the order.");
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
				Output.SendToConsole("Your changes have been cancelled.\n");
			}
		}
	}
}

