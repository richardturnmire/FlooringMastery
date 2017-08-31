using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Data.Repositories.Production;
//using FlooringMastery.Data.Repositories.Test;

using FlooringMastery.Data.Repositories;
using FlooringMastery.BLL;

namespace FlooringMastery.UI
{
    public class DIContainer
    {

        // the kernel is the master factory
        public static IKernel Kernel = new StandardKernel();

        // constructor, to configure the bindings
        static DIContainer()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            Kernel.Bind<OrderManager>().ToSelf().InSingletonScope();

            if (mode.ToLower() == "Production")
            {
                Kernel.Bind<IOrderRepository>().To<OrderRepository>();
                Kernel.Bind<IProductInfoRepository>().To<ProductInfoRepository>();
                Kernel.Bind<ITaxInfoRepository>().To<TaxInfoRespository>();
            }
            else
            {
                Kernel.Bind<IOrderRepository>().To<TestOrderRepository>();
                Kernel.Bind<IProductInfoRepository>().To<TestProductInfoRepository>();
                Kernel.Bind<ITaxInfoRepository>().To<TestTaxInfoRepository>();

            }
        }
       
    }
}

