using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.BLL;
using FlooringMastery.Data.Repositories;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using Ninject;
using NUnit.Framework;

namespace FlooringMastery.Tests
{
    class OrderMaster
    {
		[Test]
		public void OrderLookupInjectionTests()
		{
			var kernel = new StandardKernel();

			kernel.Bind<IOrderRepository>().To<TestOrderRepository>();
			kernel.Bind<IProductInfoRepository>().To<TestProductInfoRepository>();
			kernel.Bind<ITaxInfoRepository>().To<TestTaxInfoRepository>();

			var manager = kernel.Get<OrderManager>();
			var orderInfo = kernel.Get<OrderInfo>();
			orderInfo.OrderDate = new DateTime(2013, 6, 1);
			orderInfo.OrderNumber = 1;

			var response = manager.LookupOrder(orderInfo);

			Assert.AreEqual(response.Success, true);
			Assert.NotNull(response.OrderInfo.Order);

			orderInfo = kernel.Get<OrderInfo>();

			orderInfo.OrderDate = new DateTime(2013, 6, 1);
			orderInfo.OrderNumber = 99;

			response = manager.LookupOrder(orderInfo);

			Assert.AreEqual(response.Success, false);
			Assert.IsNull(response.OrderInfo.Order);

		}

		[Test]
		public void OrderListInjectionTests()
		{
			var kernel = new StandardKernel();

			kernel.Bind<IOrderRepository>().To<TestOrderRepository>();
			kernel.Bind<IProductInfoRepository>().To<TestProductInfoRepository>();
			kernel.Bind<ITaxInfoRepository>().To<TestTaxInfoRepository>();

			var manager = kernel.Get<OrderManager>();
			var orderInfo = kernel.Get<OrderInfo>();
			orderInfo.OrderDate = new DateTime(2013, 6, 1);
			
			var response = manager.ListOrders(orderInfo);

			Assert.AreEqual(response.Success, true);
			Assert.AreEqual(response.Orders.Count, 1);
			Assert.NotNull(response.Orders);

			orderInfo = kernel.Get<OrderInfo>();

			orderInfo.OrderDate = new DateTime(2013, 6, 30);
			
			response = manager.ListOrders(orderInfo);

			Assert.AreEqual(response.Success, true);
			Assert.AreEqual(response.Orders.Count, 0);

		}

		[Test]
		public void OrderAddInjectionTests()
		{
			var kernel = new StandardKernel();

			kernel.Bind<IOrderRepository>().To<TestOrderRepository>();
			kernel.Bind<IProductInfoRepository>().To<TestProductInfoRepository>();
			kernel.Bind<ITaxInfoRepository>().To<TestTaxInfoRepository>();

			var manager = kernel.Get<OrderManager>();
			var orderInfo = kernel.Get<OrderInfo>();
			var order = kernel.Get<Order>();
			
			order.Area = 101;
			order.CustomerName = "Order Add Injection Test";
			order.CostPerSquareFoot = 1.00M;
			order.LaborCostPerSquareFoot = 2.00M;
			order.ProductType = "Test Material";
			order.State = "KY";
			order.TaxRate = 6.00M;

			orderInfo.OrderDate = new DateTime(2013, 6, 1);
			orderInfo.Order = order;

			var response = manager.Add(orderInfo);

			Assert.AreEqual(response.Success, true);
			Assert.AreEqual(response.OrderInfo.OrderNumber, 2);
			Assert.AreEqual(response.OrderInfo.Order.Total, 321.18M);
		}

		[Test]
		public void OrderEditInjectionTests()
		{
			var kernel = new StandardKernel();

			kernel.Bind<IOrderRepository>().To<TestOrderRepository>();
			kernel.Bind<IProductInfoRepository>().To<TestProductInfoRepository>();
			kernel.Bind<ITaxInfoRepository>().To<TestTaxInfoRepository>();

			var manager = kernel.Get<OrderManager>();
			var orderInfo = kernel.Get<OrderInfo>();
			var order = kernel.Get<Order>();

			order.OrderNumber = 1;
			order.Area = 101;
			order.CustomerName = "Order Add Injection Test";
			order.CostPerSquareFoot = 1.00M;
			order.LaborCostPerSquareFoot = 2.00M;
			order.ProductType = "Test Material";
			order.State = "KY";
			order.TaxRate = 6.00M;

			orderInfo.OrderDate = new DateTime(2013, 6, 1);
			orderInfo.OrderNumber = 1;
			orderInfo.Order = order;

			var response = manager.Edit(orderInfo);

			Assert.AreEqual(response.Success, true);
			Assert.AreEqual(response.OrderInfo.OrderNumber, 1);
			Assert.AreEqual(response.OrderInfo.Order.Total, 1051.88M);
		}


		[Test]
		public void OrderRemoveInjectionTests()
		{
			var kernel = new StandardKernel();

			kernel.Bind<IOrderRepository>().To<TestOrderRepository>();
			kernel.Bind<IProductInfoRepository>().To<TestProductInfoRepository>();
			kernel.Bind<ITaxInfoRepository>().To<TestTaxInfoRepository>();

			var manager = kernel.Get<OrderManager>();
			var orderInfo = kernel.Get<OrderInfo>();
			

			orderInfo.OrderDate = new DateTime(2013, 6, 1);
			orderInfo.OrderNumber = 1;

			var response = manager.Remove(orderInfo);

			Assert.AreEqual(response.Success, true);

			var response2 = manager.LookupOrder(orderInfo);

			Assert.AreEqual(response2.Success, false);
			
		}

	}
}
