using Domains.DBModels;
using MediatR;

namespace Commands.Query
{
    public class GetCampaignByIdQuery : IRequest<Campaign>
    {
        public string OrgId { get; set; }
        public string CampaignId { get; set; }
    }
}
