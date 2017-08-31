using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;

namespace FlooringMastery.UI
{
    public partial class Input
    {
        public static ProductInfo GetProductType(List<ProductInfo> products)
        {
			string prompt = $"Enter product type (1 - {products.Count}): ";
			ProductInfo result = null;

			
			int cnt = 0;
			foreach ( var prod in products )
			{
				cnt++;
				Output.SendToConsole($"{cnt,2}) {prod.ProductType} ");
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
						if ( selection > 0 && selection <= products.Count() )
						{
							result = products.ElementAt(selection - 1);
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
			}

			return result;
		}
	

        public static ProductInfo GetProductType(String oldProductType, List<ProductInfo> products)
        {
            string prompt = $"Enter product type [{oldProductType}]: ";
			ProductInfo result = null;

			Output.SendToConsole($"Select product (1 - {products.Count} from the following list\n");
			int cnt = 0;
            foreach (var prod in products)
            {
                cnt++;
                Output.SendToConsole($"{cnt,2}) {prod.ProductType} ");
            };

            while (true)
            {
                Output.SendToConsole();
                Console.Write(prompt);
				var input = Console.ReadLine().Trim();
				if (input.Length > 0)
				{
					if ( int.TryParse(input, out int selection) )
					{
						if ( selection > 0 && selection <= products.Count() )
						{
							result = products.ElementAt(selection - 1);
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
            }
               
            return result;
        }
    }
}
