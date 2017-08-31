using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.UI
{
    public partial class Input
    {
        public static decimal GetArea()
        {
			decimal result;

			string prompt = "Enter area size: ";

			Output.SendToConsole();
			while ( true )
			{
				Console.Write(prompt);
				var input = Console.ReadLine().Trim();

				if ( input.Length > 0 )
				{
					if ( decimal.TryParse(input, out result) )
					{
						if (result >= 100)
						break;
						else
						{
							Output.SendToConsole("Area must be >= 100. Press any key to try again");
						}
					}
					else
					{
						Output.SendToConsole("Error! An invalid area size was entered, please try again.");
					}
					Console.ReadKey();
				}
			};

			return result;
		}

        public static decimal GetArea(decimal oldArea)
        {
			decimal result = oldArea;

            string prompt = $"Enter area size [{oldArea}]: ";

            Output.SendToConsole();
            while (true)
            {
                Console.Write(prompt);
				var input = Console.ReadLine().Trim();
				if (input != String.Empty)
				{
					if ( decimal.TryParse(input, out result) )
					{
						if (result >= 100)
							break;
						else
						{ 
							Output.SendToConsole("Error! Area must be >= 100. Press any key to try again.");
						}
						
					}
					else
					{
						Output.SendToConsole("Error! An invalid area size was entered, please try again.");
					}
					Console.ReadKey();
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
