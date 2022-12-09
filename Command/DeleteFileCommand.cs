using MediatR;

namespace Commands
{
    public class DeleteFileCommand : IRequest
    {
        public string FileId { get; set; }
    }
}
