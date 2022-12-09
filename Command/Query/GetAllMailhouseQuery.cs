using Domains.DBModels;

using MediatR;

using System.Collections.Generic;

namespace Commands.Query
{
    public class GetAllMailhouseQuery : IRequest<List<Mailhouse>>
    {
        public string OrgId { get; set; }
    }
}
