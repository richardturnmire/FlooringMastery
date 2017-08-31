using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Responses
{ 
	public class OrderListResponse : Response
	{
		public OrderInfo OrderInfo { get; set; }
		public List<Order> Orders { get; set; }
	}
}
