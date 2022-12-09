using MediatR;

namespace Commands
{
    public class DeleteCampaignCommand : IRequest
    {
        public string CampaignId { get; set; }
    }
}
