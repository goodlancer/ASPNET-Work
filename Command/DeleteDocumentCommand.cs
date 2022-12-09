using MediatR;

namespace Commands
{
    public class DeleteDocumentCommand : IRequest
    {
        public string S3FileName { get; set; }
    }
}
