using Domains.DBModels;
using Domains.Dtos;

using MediatR;

namespace Commands.Query
{
    public class GetProjectQuery : IRequest<Projects>
    {
        public string OrgId { get; set; }
        public string Id { get; set; }

    }
}
