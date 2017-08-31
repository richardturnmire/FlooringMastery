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
    public class TaxInfoRespository : ITaxInfoRepository
    {
        private static List<TaxInfo> _states = new List<TaxInfo> { };
        private static string _fileName = @"E:\Data\FlooringMastery\FlatFiles\Production\Taxes.txt";

		public TaxInfoRespository()
		{
			if ( _states.Any() )
				return;

			FetchTaxInfoFile();
		}

		private TaxInfoFileResponse FetchTaxInfoFile()
		{
			TaxInfoFileResponse response = DIContainer.Kernel.Get<TaxInfoFileResponse>();
			response.FileName = _fileName;
			response.Success = false;

			if ( !File.Exists(_fileName) )
				response.Message = $"Tax Info file {_fileName} was not found. Contact IT";
			else
			{
				try
				{
					using ( TextReader file = File.OpenText(_fileName) )
					{
						using ( var csv = new CsvReader(file) )
							_states = csv.GetRecords<TaxInfo>().ToList();
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

		public TaxInfoFileResponse GetStates()
		{
			TaxInfoFileResponse response = DIContainer.Kernel.Get<TaxInfoFileResponse>();
			response.FileName = _fileName;
			response.States = _states;
			response.Success = true;

			return response;
		}

		public TaxInfoFileResponse GetState(string state)
		{
			TaxInfoFileResponse response = DIContainer.Kernel.Get<TaxInfoFileResponse>();
			response.FileName = _fileName;
			response.Success = false;

			response.State = _states.Where(a => a.StateAbbreviation == state).FirstOrDefault();

			if ( response.State != null )
				response.Success = true;
			else
				response.Message = $"We do not do business in {state}";

			return response;
		}

	}
}
