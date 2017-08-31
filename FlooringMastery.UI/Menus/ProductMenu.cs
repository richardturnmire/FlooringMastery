using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.UI.Workflows;
using Ninject;

namespace FlooringMastery.UI.Menus
{
	public class ProductMenu
	{
		public static void Start()
		{
			Console.Clear();
			while ( true )
			{
				Console.Clear();
				Output.SendToConsole("Flooring Mastery Product Menu");
				Output.SendToConsole("---------------------------------");
				Output.SendToConsole("1. List Products");
				Output.SendToConsole("2. Add a Product");
				Output.SendToConsole("3. Edit a Product");
				Output.SendToConsole("4. Remove a Product");
				Output.SendToConsole();
				Output.SendToConsole("Q. Return to Main Menu");
				Console.Write("\nEnter selection:  ");

				string userinput = Console.ReadLine().ToLower();

				switch ( userinput )
				{
					case "1":
						ProductListWorkflow productListWorkflow = DIContainer.Kernel.Get<ProductListWorkflow>();
						productListWorkflow.Execute();
						break;
					case "2":
						OrderAddWorkflow orderAddWorkflow = DIContainer.Kernel.Get<OrderAddWorkflow>();
						orderAddWorkflow.Execute();
						break;
					case "3":
						OrderEditWorkflow orderEditWorkflow = DIContainer.Kernel.Get<OrderEditWorkflow>();
						orderEditWorkflow.Execute();
						break;
					case "4":
						OrderRemovalWorkflow orderRemovalWorkflow = DIContainer.Kernel.Get<OrderRemovalWorkflow>();
						orderRemovalWorkflow.Execute();
						break;
					case "q":
						return;
					default:
						Output.SendToConsole("\nInvalid selection. Press any key to try again...");
						Console.ReadKey();
						break;
				}
			}
		}
	}
}
