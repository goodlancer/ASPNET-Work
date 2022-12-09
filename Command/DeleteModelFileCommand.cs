using MediatR;

namespace Commands
{
    public class DeleteModelFileCommand : IRequest<bool>
    {
        public string FileId { get; set; }
    }
}
