using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Data;


namespace FlooringMastery.BLL
{
    public class DIContainer
    {
        // the kernel is the master factory
        public static IKernel Kernel = new StandardKernel();

        // constructor, to configure the bindings
        static DIContainer()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();
                        
            //Kernel.Bind<IOrder>().To<OrderAddRule>();

            //Kernel.Bind<IWithdrawal>().To<OrderEditRule>();
            

        }
    }
}

