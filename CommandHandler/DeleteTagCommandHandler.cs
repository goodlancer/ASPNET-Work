using Commands;
using Domains.DBModels;
using MediatR;
using Services.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, bool>
    {
        private IBaseRepository<Tag> _tagRepo;
        public DeleteTagCommandHandler(IBaseRepository<Tag> tagRepo)
        {
            this._tagRepo = tagRepo;
        }
        public async Task<bool> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await _tagRepo.GetByIdAsync(request.Id);
            if (tag.OrgId != request.OrgId) // access denied
                return false;
            await _tagRepo.Delete(request.Id);
            return true;
        }
    }
}
