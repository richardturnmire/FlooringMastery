using System;
using System.Collections.Generic;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Responses;
using Ninject;

namespace FlooringMastery.BLL
{
    public class OrderManager
    {
        private IOrderRepository _orderRepository;
        private ITaxInfoRepository _states;
        private IProductInfoRepository _products;

        public OrderManager(IOrderRepository orderRepository, ITaxInfoRepository taxRepository, IProductInfoRepository productRepository)
        {
            _orderRepository = orderRepository;
            _states = taxRepository;
            _products = productRepository;
        }

        public OrderLookupResponse LookupOrder(OrderInfo orderInfo)
        {
           
            OrderLookupResponse response = DIContainer.Kernel.Get<OrderLookupResponse>();

            if (orderInfo.OrderDate != _orderRepository.GetOrderDate())
            {
                _orderRepository.FetchOrders(orderInfo.OrderDate);
            }

            var fileresponse = _orderRepository.GetOrder(orderInfo);

            if (fileresponse.Success)
            {
                response.OrderInfo = fileresponse.OrderInfo;
                response.Message = fileresponse.Message;
                response.Success = true;
            }
            else
            {
                response.OrderInfo = orderInfo;
                response.Success = false;
                response.Message = fileresponse.Message;
            }

            return response;
        }

		public OrderListResponse ListOrders(OrderInfo orderInfo)
		{

			OrderListResponse response = DIContainer.Kernel.Get<OrderListResponse>();

			if ( orderInfo.OrderDate != _orderRepository.GetOrderDate() )
			{
				_orderRepository.FetchOrders(orderInfo.OrderDate);
			}

			var fileresponse = _orderRepository.GetOrders(orderInfo);

			if ( fileresponse.Success )
			{
				response.OrderInfo = orderInfo;
				response.Orders = fileresponse.Orders;
				response.Message = fileresponse.Message;
				response.Success = true;
			}
			else
			{
				response.OrderInfo = orderInfo;
				response.Success = false;
				response.Message = fileresponse.Message;
			}

			return response;
		}

		public OrderAddResponse Add(OrderInfo orderInfo)
        {
            OrderAddResponse response = DIContainer.Kernel.Get<OrderAddResponse>();
          
            response.Success = true;

            if (orderInfo.OrderDate != _orderRepository.GetOrderDate())
            {
                _orderRepository.FetchOrders(orderInfo.OrderDate);
            }

            var newOrderNumber = _orderRepository.NewOrderNumber;

            orderInfo.Order.OrderNumber = newOrderNumber;
			orderInfo.OrderNumber = newOrderNumber;
            orderInfo.Order = CalculateOrderFields(orderInfo.Order);

            response.OrderInfo = orderInfo;

            return response;
        }

        public OrderEditResponse Edit(OrderInfo orderInfo)
        {
            OrderEditResponse response = DIContainer.Kernel.Get<OrderEditResponse>();

            response.Success = false;
			response.OrderInfo = orderInfo;

            if (orderInfo.OrderDate != _orderRepository.GetOrderDate())
            {
                _orderRepository.FetchOrders(orderInfo.OrderDate);
            }

            var fileresponse = _orderRepository.GetOrder(orderInfo);

            if (!fileresponse.Success)
            {
                response.Message = $"{orderInfo.OrderNumber} is not a valid Order.";
            }
            else
            {
				response.OrderInfo = fileresponse.OrderInfo;
				response.Success = fileresponse.Success;
            }
         
            return response;
        }

        public OrderRemovalResponse Remove(OrderInfo orderInfo)
        {
            OrderRemovalResponse response = DIContainer.Kernel.Get<OrderRemovalResponse>();
			
				response.Success = false;
                response.OrderInfo = orderInfo;
          

            if (orderInfo.OrderDate != _orderRepository.GetOrderDate())
            {
                _orderRepository.FetchOrders(orderInfo.OrderDate);
            }

			var fileresponse = _orderRepository.RemoveOrder(orderInfo);

			if (!fileresponse.Success)
            {
                response.Message = $"An error occurred trying to remove {orderInfo.OrderNumber}.";
            }

            response.Success = fileresponse.Success;

            //if (response.Success)
            //{
            //   fileresponse = _orderRepository.SaveOrders(response.OrderInfo);
            //    if (!fileresponse.Success)
            //    {
            //        response.Success = fileresponse.Success;
            //        response.Message = fileresponse.Message;
            //    }
            //}

            return response;
        }

        public OrderFileAccessResponse SaveOrder(OrderInfo orderInfo)
        {
			OrderFileAccessResponse response = DIContainer.Kernel.Get<OrderFileAccessResponse>();

			response.Success = false;
			response.OrderInfo = orderInfo;

			var fileresponse = _orderRepository.SaveOrder(orderInfo);
			response.Success = fileresponse.Success;
            if (fileresponse.Success)
            {
                response.Message = $"Order {orderInfo.Order.OrderNumber} saved";
            }
            else
            {
                response.Message = fileresponse.Message;
            }

            return response;
        }

		public OrderFileAccessResponse SaveOrders(OrderInfo orderInfo)
		{
			OrderFileAccessResponse response = DIContainer.Kernel.Get<OrderFileAccessResponse>();

			response.Success = false;
			response.OrderInfo = orderInfo;

			var fileresponse = _orderRepository.SaveOrders(orderInfo);
			response.Success = fileresponse.Success;

			if ( fileresponse.Success )
			{
				response.Message = $"Order {orderInfo.Order.OrderNumber} saved";
			}
			else
			{
				response.Message = fileresponse.Message;
			}

			return response;
		}

		public TaxInfo GetState(string state)
        {
            return _states.GetState(state).State;
        }

        public TaxInfoFileResponse GetStates()
        {
			var response = DIContainer.Kernel.Get<TaxInfoFileResponse>();
			response = _states.GetStates();
			return response;
        }

        public ProductInfo GetProduct(string product)
        {
            return _products.GetProduct(product).ProductInfo;
        }

        public ProductInfoFileResponse GetProducts()
        {
			var response = DIContainer.Kernel.Get<ProductInfoFileResponse>();
			response = _products.GetProducts();
			return response;
		}

        private Order CalculateOrderFields(Order order)
        {
            order.MaterialCost = (order.Area * order.CostPerSquareFoot);
            order.LaborCost = (order.Area * order.LaborCostPerSquareFoot);
            order.Tax = Math.Round(((order.MaterialCost + order.LaborCost) * (order.TaxRate / 100)), 2);
            order.Total = (order.MaterialCost + order.LaborCost + order.Tax);
            return order;
        }
    }
}
