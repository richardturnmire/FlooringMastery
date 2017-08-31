using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Responses
{
    public class OrderEditResponse : Response
    {
        public OrderInfo OrderInfo { get; set; }
        public List<TaxInfo> States { get; set; }
        public List<ProductInfo> Products { get; set; }
    }
}
