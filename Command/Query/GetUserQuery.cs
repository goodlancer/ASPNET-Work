using Domains.Dtos;

using MediatR;

namespace Commands
{
    public class GetUserQuery : IRequest<UserForUi>
    {
        public string UserId { get; set; }
        public string OrgId { get; set; }

    }
}
