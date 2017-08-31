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
	public class TaxListWorkflow : IWorkflow
	{
		public void Execute()
		{
			OrderManager manager = DIContainer.Kernel.Get<OrderManager>();
			
			Console.Clear();

			
			var line = "{0, -5}    {1,-20}   {2,10:F2}";

			var response = manager.GetStates();

			if ( response.Success )
			{
				Output.SendToConsole();
				Output.SendToConsole(String.Format(line, "State", "Name", "Tax Rate"));
				Output.SendToConsole();
				foreach ( var state  in response.States )
					Output.SendToConsole(String.Format(line, state.StateAbbreviation, state.StateName, state.TaxRate));
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
