using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models.Responses;

namespace FlooringMastery.Models.Interfaces
{
    public interface IProductInfoRepository
    {
       ProductInfoFileResponse GetProduct(string product);
        ProductInfoFileResponse GetProducts();
    }
}
