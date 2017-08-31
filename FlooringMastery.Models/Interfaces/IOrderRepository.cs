using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models.Responses;

namespace FlooringMastery.Models.Interfaces
{
    public interface IOrderRepository
    {
        OrderFileAccessResponse AddOrder(OrderInfo orderInfo);
        OrderFileAccessResponse GetOrder(OrderInfo orderInfo);
		OrderFileAccessResponse FetchOrders(DateTime orderDate);
        OrderFileAccessResponse RemoveOrder(OrderInfo orderInfo);
        OrderFileAccessResponse SaveOrder(OrderInfo orderInfo);
        OrderFileAccessResponse SaveOrders(OrderInfo orderInfo);

		OrderListResponse GetOrders(OrderInfo orderInfo);

		int NewOrderNumber { get; }

        DateTime GetOrderDate();
    }
}
