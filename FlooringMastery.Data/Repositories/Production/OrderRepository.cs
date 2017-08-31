using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Responses;
using Ninject;

namespace FlooringMastery.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private static List<Order> _orders = new List<Order> { };
        private static string _fileName = @"E:\Data\FlooringMastery\FlatFiles\Production\Orders_{0:MMddyyyy}.txt";

		public DateTime OrderDate { get; set; }

		public OrderRepository()
		{

		}

		public DateTime GetOrderDate()
		{
			return OrderDate;
		}

		public OrderFileAccessResponse FetchOrders(DateTime orderDate)
		{
			OrderFileAccessResponse response = DIContainer.Kernel.Get<OrderFileAccessResponse>();

			response.Success = false;
			response.OrderInfo = DIContainer.Kernel.Get<OrderInfo>();
			response.OrderInfo.OrderDate = orderDate;
			response.FileName = String.Format(_fileName, orderDate.ToString("MMddyyyy"));

			_orders.Clear();

			if ( File.Exists(response.FileName) )
			{
				try
				{
					using ( TextReader file = File.OpenText(response.FileName) )
					{
						using ( var csv = new CsvReader(file) )
						{
							_orders = csv.GetRecords<Order>().ToList();
						}
					}
					OrderDate = orderDate;
					response.Success = true;
				}
				catch ( Exception ex )
				{
					response.Message = $"An error has occurred trying to access {response.FileName}. Contact IT.";
					response.Error = ex;
				};
			}
			else
			{
				response.Message = $"No orders for {orderDate:MM-dd-yyyy}.";
			}
			return response;
		}

		public int NewOrderNumber
		{
			get
			{
				int result = _orders.Max(o => o.OrderNumber as int?) ?? 0;
				result++;
				return result;
			}
		}

		private static Order CalculateOrderFields(Order order)
		{
			order.MaterialCost = order.Area * order.CostPerSquareFoot;
			order.LaborCost = order.Area * order.LaborCostPerSquareFoot;
			order.Tax = Math.Round((order.MaterialCost + order.LaborCost) * (order.TaxRate / 100), 2);
			order.Total = order.MaterialCost + order.LaborCost + order.Tax;

			return order;
		}

		public OrderFileAccessResponse AddOrder(OrderInfo orderInfo)
		{
			OrderFileAccessResponse response = DIContainer.Kernel.Get<OrderFileAccessResponse>();
			response.OrderInfo = orderInfo;
			response.Success = false;
			var saveOrderNumber = orderInfo.OrderNumber;
			try
			{
				if ( _orders.Exists(o => o.OrderNumber == saveOrderNumber) )
				{
					response.Message = $"Error:  Order {saveOrderNumber} is a duplicate. This should not happen. \n Contact IT.";
					response.Success = false;
				}
				else
				{
					_orders.Add(orderInfo.Order);
				}
			}
			catch ( Exception ex )
			{
				response.Message = $"Error occurred during lookup of order #{saveOrderNumber}";
				response.Error = ex;
			}

			return response;
		}

		public OrderFileAccessResponse GetOrder(OrderInfo orderInfo)
		{
			OrderFileAccessResponse response = DIContainer.Kernel.Get<OrderFileAccessResponse>();
			response.FileName = _fileName;
			response.OrderInfo = orderInfo;
			response.Success = false;

			var saveOrderNumber = orderInfo.OrderNumber;

			try
			{
				if ( _orders.Any() )
				{
					var result = _orders.Where(a => a.OrderNumber == saveOrderNumber).FirstOrDefault();

					if ( result != null )
					{
						response.OrderInfo.Order = result;
						response.Success = true;
					}
					else
						response.Message = $"Order {saveOrderNumber} was not found.";
				}
				else
				{
					response.Message = $"No orders with a date of {orderInfo.OrderDate}";
				}
			}
			catch ( Exception ex )
			{
				response.Message = $"Error occurred during lookup of order #{saveOrderNumber}";
				response.Error = ex;
			}

			return response;
		}

		public OrderListResponse GetOrders(OrderInfo orderInfo)
		{
			OrderListResponse response = DIContainer.Kernel.Get<OrderListResponse>();
			// response.FileName = _fileName;
			response.OrderInfo = orderInfo;
			response.Success = false;
			response.Orders = _orders;

			return response;
		}

		public OrderFileAccessResponse RemoveOrder(OrderInfo orderInfo)
		{
			OrderFileAccessResponse response = DIContainer.Kernel.Get<OrderFileAccessResponse>();
			response.OrderInfo = orderInfo;
			response.Success = false;

			try
			{
				int inx = _orders.FindIndex(o => o.OrderNumber == orderInfo.OrderNumber);
				if ( inx > -1 )
				{
					_orders.RemoveAt(inx);
					response.Success = true;
				}
				else
				{
					response.Message = $"Error trying to remove order. Order {orderInfo.OrderNumber} was not found";
				}

			}
			catch ( Exception ex )
			{
				response.Message = "An Error has occurred!";
				response.Error = ex;
			}

			return response;
		}

		public OrderFileAccessResponse SaveOrder(OrderInfo orderInfo)
		{
			OrderFileAccessResponse response = DIContainer.Kernel.Get<OrderFileAccessResponse>();
			response.OrderInfo = orderInfo;
			response.Success = false;
			try
			{
				int inx = _orders.FindIndex(o => o.OrderNumber == orderInfo.OrderNumber);
				if ( inx > -1 )
					_orders[inx] = orderInfo.Order;
				else
					_orders.Add(orderInfo.Order);

				response.Success = true;
			}
			catch ( Exception ex )
			{
				response.Message = "An Error has occurred!";
				response.Error = ex;
			}

			return response;
		}

		public OrderFileAccessResponse SaveOrders(OrderInfo orderInfo)
		{
			OrderFileAccessResponse response = DIContainer.Kernel.Get<OrderFileAccessResponse>();
			response.FileName = string.Format(_fileName, orderInfo.OrderDate.ToString("MMddyyyy"));
			response.OrderInfo = orderInfo;
			response.Success = false;

			try
			{
				using ( TextWriter file = File.CreateText(response.FileName) )
				{
					using ( var csv = new CsvWriter(file) )
					{
						csv.WriteRecords(_orders);
					}
				}

				response.Success = true;
			}
			catch ( Exception ex )
			{
				response.Message = $"An error has occurred trying to access {response.FileName}. Contact IT.";
				response.Error = ex;
			}

			return response;
		}
	}
}
