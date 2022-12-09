using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands
{
    public class SetOfferPricesCommand : IRequest
    {
        public string UserId { get; set; }
        public List<SetOfferPrice> SetOfferPrices { get; set; }
    }

    public class SetOfferPrice
    {
        public string OfferPrice { get; set; }
        public string PropertyId { get; set; }
    }
}
