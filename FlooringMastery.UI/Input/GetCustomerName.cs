using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.UI
{
	public partial class Input
	{
		public static string GetCustomerName()
		{
			string prompt = "Enter customer name: ";
			string result = null;

			while ( true )
			{
				Output.SendToConsole();
				Console.Write(prompt);

				var input = Console.ReadLine().Trim();
				if ( input.Length > 0 )
				{
					result = input;
					break;
				}
				else
				{
					Output.SendToConsole("Please enter the customer name. Press enter to try again.");
				}
			};

			return result;
		}

		public static string GetCustomerName(String oldCustomerName)
		{
			string prompt = $"Enter customer name [{oldCustomerName}]: ";
			string result = oldCustomerName;

			while ( true )
			{
				Output.SendToConsole();
				Console.Write(prompt);

				var input = Console.ReadLine().Trim();
				if ( input.Length > 0 )
				{
					result = input;
				}

				break;
			};

			return result;
		}
	}
}
