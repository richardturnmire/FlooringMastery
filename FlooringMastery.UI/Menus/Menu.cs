using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.UI.Menus;
using FlooringMastery.UI.Workflows;
using Ninject;

namespace FlooringMastery.UI.Menus
{
    public class Menu
    {
        public static void Start()
        {
            Console.Clear();
            while (true)
            {
                Console.Clear();
                Output.SendToConsole("Flooring Mastery");
                Output.SendToConsole("---------------------------------");
                Output.SendToConsole("1. Order Menu");
                Output.SendToConsole("2. Tax Menu");
                Output.SendToConsole("3. Product Menu");
				Output.SendToConsole();
                Output.SendToConsole("Q. Return to main Menu");
                Console.Write("\nEnter selection:  ");

                string userinput = Console.ReadLine().ToLower();

                switch (userinput)
                {
                    case "1":
                        //OrderMenu lookupWorkflow = DIContainer.Kernel.Get<OrderLookupWorkflow>();
                        OrderMenu.Start();
                        break;
                    case "2":
                       // OrderAddWorkflow orderAddWorkflow = DIContainer.Kernel.Get<OrderAddWorkflow>();
                        TaxMenu.Start();
                        break;
                    case "3":
                        // OrderEditWorkflow orderEditWorkflow = DIContainer.Kernel.Get<OrderEditWorkflow>();
                        ProductMenu.Start();
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
