using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Responses
{
	public class TaxListResponse : Response
	{
		public List<TaxInfo> States { get; set; }
	}
}
