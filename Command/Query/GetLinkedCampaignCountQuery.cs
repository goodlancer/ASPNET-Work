using MediatR;
using System.Collections.Generic;

namespace Commands.Query
{
    public class GetLinkedCampaignCountQuery : IRequest<bool>
    {
        public List<string> PropertyIds { get; set; }
    }
}
