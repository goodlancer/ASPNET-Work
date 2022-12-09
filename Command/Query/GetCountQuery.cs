using MediatR;

using System;
using System.Collections.Generic;

namespace Commands.Query
{
    public class GetCountQuery : IRequest<long>
    {
        public string OrganizationId { get; set; }
        public string UserId { get; set; }
        public Type EntityName { get; set; }
        public string LoogedInUserId { get; set; }
        public List<string> Roles { get; set; }
    }
}
