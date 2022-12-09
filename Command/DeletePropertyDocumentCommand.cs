using MediatR;


namespace Commands
{
    public class DeletePropertyDocumentCommand : IRequest
    {
        public string DocumentId { get; set; }
    }
}
