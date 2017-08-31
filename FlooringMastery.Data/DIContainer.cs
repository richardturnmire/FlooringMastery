using Ninject;

namespace FlooringMastery.Data
{
	public class DIContainer
    {
        // the kernel is the master factory
        public static IKernel Kernel = new StandardKernel();

        // constructor, to configure the bindings
        static DIContainer()
        {
           // string mode = ConfigurationManager.AppSettings["Mode"].ToString();
                        
            //Kernel.Bind<IOrder>().To<OrderAddRule>();

            //Kernel.Bind<IWithdrawal>().To<OrderEditRule>();
            

        }
    }
}

