using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.UI
{
    public partial class Input
    {
        public static DateTime GetOrderDate()
        {
            DateTime result;

            while (true)
            {
                Output.SendToConsole();
                Console.Write("Enter order date (mm-dd-yyyy): ");

                if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    result = date;
                    break;
                }
                else
                {
                    Output.SendToConsole("Error! An invalid date was entered, please try again.");
                }
            };

            return result;
        }
    }
}
