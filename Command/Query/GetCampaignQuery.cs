using Domains.DBModels;
using MediatR;
using System.Collections.Generic;

namespace Commands.Query
{
    public class GetCampaignQuery : IRequest<List<Campaign>>
    {
        public string OrgId { get; set; }
        public string SearchKey { get; set; }
    }
}
