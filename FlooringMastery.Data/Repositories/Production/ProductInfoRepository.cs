using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Responses;
using Ninject;

namespace FlooringMastery.Data.Repositories.Production
{ 
    public class ProductInfoRepository : IProductInfoRepository
    {
    private static List<ProductInfo> _products = new List<ProductInfo> { };
    private static string _fileName = @"E:\Data\FlooringMastery\FlatFiles\Production\Products.txt";

		public ProductInfoRepository()
		{
			if ( _products.Any() )
				return;

			FetchProductInfoFile();
		}

		private ProductInfoFileResponse FetchProductInfoFile()
		{
			ProductInfoFileResponse response = DIContainer.Kernel.Get<ProductInfoFileResponse>();

			response.Success = false;
			response.FileName = _fileName;

			if ( !File.Exists(_fileName) )
				response.Message = $"Product Info file {_fileName} was not found. Contact IT";
			else
			{
				try
				{
					using ( TextReader file = File.OpenText(_fileName) )
					{
						using ( var csv = new CsvReader(file) )
							_products = csv.GetRecords<ProductInfo>().ToList();
					}
					response.Success = true;
				}
				catch ( Exception ex )
				{
					response.Message = "An error has occurred trying to access the Test Tax Info file. Contact IT.";
					response.Error = ex;
				}
			}

			return response;
		}

		public ProductInfoFileResponse GetProduct(string product)
		{

			ProductInfoFileResponse response = DIContainer.Kernel.Get<ProductInfoFileResponse>();

			response.Success = false;
			response.FileName = _fileName;

			response.ProductInfo = _products.Where(a => a.ProductType == product).FirstOrDefault();

			if ( response.ProductInfo != null )
				response.Success = true;
			else
				response.Message = $"Invalid product {product}";

			return response;
		}

		public ProductInfoFileResponse GetProducts()
		{
			ProductInfoFileResponse response = DIContainer.Kernel.Get<ProductInfoFileResponse>();

			response.Success = true;
			response.FileName = _fileName;
			response.Products = _products;
			return response;
		}
	}
}
