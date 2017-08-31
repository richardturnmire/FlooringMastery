using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.UI.Workflows;
using Ninject;

namespace FlooringMastery.UI.Menus
{
	public class OrderMenu
	{
		public static void Start()
		{
			Console.Clear();
			while ( true )
			{
				Console.Clear();
				Output.SendToConsole("Flooring Mastery Order Menu");
				Output.SendToConsole("---------------------------------");
				Output.SendToConsole("1. Order Lookup");
				Output.SendToConsole("2. Add an Order");
				Output.SendToConsole("3. Edit an order");
				Output.SendToConsole("4. Remove an order");
				Output.SendToConsole("5. List orders");
				Output.SendToConsole();
				Output.SendToConsole("Q. Return to Main Menu");
				Console.Write("\nEnter selection:  ");

				string userinput = Console.ReadLine().ToLower();

				switch ( userinput )
				{
					case "1":
						OrderLookupWorkflow lookupWorkflow = DIContainer.Kernel.Get<OrderLookupWorkflow>();
						lookupWorkflow.Execute();
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
					case "5":
						OrderListWorkflow orderListWorkflow = DIContainer.Kernel.Get<OrderListWorkflow>();
						orderListWorkflow.Execute();
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
