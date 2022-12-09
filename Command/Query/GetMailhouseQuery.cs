using Domains.DBModels;

using MediatR;

namespace Commands.Query
{
    public class GetMailhouseQuery : IRequest<Mailhouse>
    {
        public string MailhouseId { get; set; }
    }
}
