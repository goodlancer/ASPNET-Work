using Domains.Dtos;

using MediatR;

namespace Commands.Query
{
    public class GetIncomeQuery : IRequest<IncomeForUi>
    {
        public string OrgId { get; set; }
        public string IncomeId { get; set; }

    }
}
