using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Responses
{
    public class TaxInfoFileResponse : Response
    {
        public TaxInfo State { get; set; }
        public List<TaxInfo> States { get; set; }
        public string FileName { get; set; }
        public Exception Error { get; set; }
    }
}
