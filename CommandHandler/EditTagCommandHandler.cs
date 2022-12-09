using Commands;
using Domains.DBModels;
using MediatR;
using Services.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class EditTagCommandHandler : IRequestHandler<EditTagCommand, bool>
    {
        private IBaseRepository<Tag> _tagRepo;
        public EditTagCommandHandler(IBaseRepository<Tag> tagRepo)
        {
            this._tagRepo = tagRepo;
        }

        public async Task<bool> Handle(EditTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await _tagRepo.GetByIdAsync(request.Id);
            if (tag.OrgId != request.OrgId) // access denied
                return false;
            tag.Name = request.Name;
            return await _tagRepo.UpdateAsync(tag);
        }
    }
}
