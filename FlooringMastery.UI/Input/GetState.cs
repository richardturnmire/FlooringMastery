using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;
using FlooringMastery.Data.Repositories;
using FlooringMastery.Models;
using Ninject;

namespace FlooringMastery.UI
{

	public partial class Input
	{
		public static TaxInfo GetState(List<TaxInfo> states)
		{
			string prompt = "Enter state: ";
			TaxInfo result = DIContainer.Kernel.Get<TaxInfo>();

			Output.SendToConsole($"Select state (1 - {states.Count} from the following list\n");
			int cnt = 0;

			foreach ( var st in states )
			{
				cnt++;
				Output.SendToConsole($"{cnt,2}) { st.StateAbbreviation} - { st.StateName}");
			};

			while ( true )
			{
				Output.SendToConsole();
				Console.Write(prompt);
				var input = Console.ReadLine().Trim();

				if ( input.Length > 0 )
				{
					if ( int.TryParse(input, out int selection) )
					{
						if ( selection > 0 && selection <= states.Count() )
						{
							result = states.ElementAt(selection - 1);
							break;
						}
						else
						{
							Output.SendToConsole("Invalid selection. Press any key to try again...");
							Console.ReadKey();
						}
					}
					else
					{
						Output.SendToConsole("Invalid input. Press any key to try again...");
						Console.ReadKey();
					}
				}

			};

			return result;
		}

		public static TaxInfo GetState(String oldState, List<TaxInfo> states)
		{
			string prompt = $"Enter state [{oldState}]: ";
			TaxInfo result = DIContainer.Kernel.Get<TaxInfo>();

			Output.SendToConsole($"Select state (1 - {states.Count} from the following list\n");
			int cnt = 0;

			foreach ( var st in states )
			{
				cnt++;
				Output.SendToConsole($"{cnt,2}) { st.StateAbbreviation} - { st.StateName}");
			};

			while ( true )
			{
				Output.SendToConsole();
				Console.Write(prompt);
				var input = Console.ReadLine().Trim();

				if ( input.Length > 0 )
				{
					if ( int.TryParse(input, out int selection) )
					{
						if ( selection > 0 && selection <= states.Count() )
						{
							result = states.ElementAt(selection - 1);
							break;
						}
						else
						{
							Output.SendToConsole("Invalid selection. Press any key to try again...");
							Console.ReadKey();
						}
					}
					else
					{
						Output.SendToConsole("Invalid input. Press any key to try again...");
						Console.ReadKey();
					}
				}
				else
				{
					break;
				}
				
			};

			return result;
		}
	}
}
