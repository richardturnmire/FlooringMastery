using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Responses
{
    public class OrderRemovalResponse : Response
    {
        public OrderInfo OrderInfo { get; set; }
		public Exception Error { get; set; }
	}
}
