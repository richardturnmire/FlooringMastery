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
        public static int GetOrderNumber()
        {
            int result = -1;

            Output.SendToConsole();
            while (true)
            {
                Console.Write("Enter order number: ");
                if (int.TryParse(Console.ReadLine(), out result))
                {
                    break;
                }
                else
                {
                    Output.SendToConsole("Error! An invalid order number was entered, please try again.");
                }
            };

            return result;
        }
    }
    
}
