using Domains.DBModels;

using MediatR;

namespace Commands
{
    public class GetOrgQuery : IRequest<Organization>
    {
        public string OrgId { get; set; }

    }
}
