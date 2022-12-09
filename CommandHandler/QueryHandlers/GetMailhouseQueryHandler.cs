using Commands.Query;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetMailhouseQueryHandler : IRequestHandler<GetMailhouseQuery, Mailhouse>
    {
        private readonly IBaseRepository<Mailhouse> _mailhouseBaseRepository;
        public GetMailhouseQueryHandler(IBaseRepository<Mailhouse> _mailhouseBaseRepository)
        {
            this._mailhouseBaseRepository = _mailhouseBaseRepository;
        }
        public async Task<Mailhouse> Handle(GetMailhouseQuery request, CancellationToken cancellationToken)
        {
            var mailhouses = await _mailhouseBaseRepository.GetSingleAsync(x => x.Id == request.MailhouseId);
            return mailhouses;
        }
    }
}
