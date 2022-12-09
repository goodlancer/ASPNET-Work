using Commands;
using Domains.DBModels;
using MediatR;
using Services.Repository;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class AddTagCommandHandler : IRequestHandler<AddTagCommand, string>
    {
        private IBaseRepository<Tag> _tagRepo;
        public AddTagCommandHandler(IBaseRepository<Tag> tagRepo)
        {
            this._tagRepo = tagRepo;
        }

        public async Task<string> Handle(AddTagCommand request, CancellationToken cancellationToken)
        {
            var newTag = new Tag()
            {
                OrgId = request.OrgId,
                Name = request.Name
            };
            var existingTagsQuery = await _tagRepo.GetAllAsync(x => x.OrgId == request.OrgId && x.Name == request.Name);
            if (existingTagsQuery.Any())
                return "";
            return await _tagRepo.Create(newTag);
        }
    }
}
