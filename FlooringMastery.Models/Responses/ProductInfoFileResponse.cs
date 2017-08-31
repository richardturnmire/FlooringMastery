using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Responses
{
    public class ProductInfoFileResponse : Response
    {
        public ProductInfo ProductInfo { get; set; }
        public List<ProductInfo> Products { get; set; }

        public string FileName { get; set; }
        public Exception Error { get; set; }
    }
}
