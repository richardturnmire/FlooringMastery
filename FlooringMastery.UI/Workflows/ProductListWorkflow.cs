using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;
using FlooringMastery.Models;
using Ninject;

namespace FlooringMastery.UI.Workflows
{
	public class ProductListWorkflow : IWorkflow
	{
		public void Execute()
		{
			OrderManager manager = DIContainer.Kernel.Get<OrderManager>();

			Console.Clear();


			var line = "{0, -20}    {1,15:F2}   {2,15:F2}";

			var response = manager.GetProducts();

			if ( response.Success )
			{
				Output.SendToConsole();
				Output.SendToConsole(String.Format(line, " ", "Labor Cost", "Material Cost"));
				Output.SendToConsole(String.Format(line, "Product", "(Per Sq Ft)", "(Per Sq Ft)"));
				Output.SendToConsole();
				foreach ( var pr in response.Products )
					Output.SendToConsole(String.Format(line, pr.ProductType, pr.LaborCostPerSquareFoot, pr.CostPerSquareFoot));
			}
			else
			{
				Output.SendToConsole("\nNo states found");
			}

			Output.SendToConsole("\nPress any key to continue...");
			Console.ReadKey();
		}
	}
}
