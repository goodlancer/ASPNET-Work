using Domains.Dtos;

using MediatR;

namespace Commands.Query
{
    public class GetExpenditureQuery : IRequest<ExpenditureForUi>
    {
        public string OrgId { get; set; }
        public string ExpenditureId { get; set; }

    }
}
