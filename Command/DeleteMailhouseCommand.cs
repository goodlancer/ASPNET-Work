using MediatR;

namespace Commands
{
    public class DeleteMailhouseCommand : IRequest
    {
        public string MailhouseId { get; set; }
    }
}
