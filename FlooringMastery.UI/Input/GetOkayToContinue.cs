using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.UI
{
    public partial class Input
    {
        public static bool GetOkayToContinue(String prompt)
        {
            bool result;
            string answer;

            while (true)
            {
                Output.SendToConsole();
                Console.Write(prompt);
                answer = Console.ReadLine().Trim().ToLower();

                if (answer == "y")
                {
                    result = true;
                    break;
                }
                else if (answer == "n")
                {
                    result = true;
                    break;
                }
                else
                {
                    Output.SendToConsole("Please answer with (Y)es or (N)o.  Press any key to try again...");
                    Console.ReadKey();
                }
            };

            return result;
        }
    }
}
