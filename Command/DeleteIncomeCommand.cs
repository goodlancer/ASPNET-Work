using MediatR;

namespace Commands
{
    public class DeleteIncomeCommand : IRequest<bool>
    {
        public string IncomeId { get; set; }
    }
}
