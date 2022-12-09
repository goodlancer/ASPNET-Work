using MediatR;

namespace Commands
{
    public class DeleteExpenditureCommand : IRequest<bool>
    {
        public string ExpenditureId { get; set; }
    }
}
