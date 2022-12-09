
using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class DeleteInvitationCommandHandler : IRequestHandler<DeleteInvitationCommand, bool>
    {

        private readonly IBaseRepository<Invitation> _baseRepositoryInvitation;

        public DeleteInvitationCommandHandler(IBaseRepository<Invitation> baseRepositoryInvitation)
        {
            _baseRepositoryInvitation = baseRepositoryInvitation;
        }

        public async Task<bool> Handle(DeleteInvitationCommand request, CancellationToken cancellationToken)
        {
            await _baseRepositoryInvitation.DeleteAllAsync(x => x.OrgId == request.OrganizationId && x.InvitedUserEmail == request.Email);
            return true;
        }

    }
}
