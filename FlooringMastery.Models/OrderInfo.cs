using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models
{
    public class OrderInfo
    {
        public DateTime OrderDate { get; set; }
		public int OrderNumber { get; set; }
        public Order Order { get; set; }
        public bool Recalculate { get; set; }
    }
}
