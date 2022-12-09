using Commands;

using Domains.DBModels;

using Infrastructure;

using MediatR;

using Services.IManagers;
using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class AcceptInvitationCommandHandler : AsyncRequestHandler<AcceptInvitationCommand>
    {

        private readonly IBaseRepository<Invitation> _baseRepositoryInvitation;
        private IMappingService _mappingService;
        private readonly IBaseUserManager _userManager;

        public AcceptInvitationCommandHandler(IBaseRepository<Invitation> _baseRepositoryInvitation
            , IMappingService _mappingService
            , IBaseUserManager userManager)
        {
            this._baseRepositoryInvitation = _baseRepositoryInvitation;
            this._mappingService = _mappingService;
            this._userManager = userManager;
        }

        protected override async Task Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
        {
            var invitation = await _baseRepositoryInvitation.GetSingleAsync(x => x.InvitedUserEmail == request.UserName);
            var applicationUser = await _userManager.FindByNameAsync(request.UserName);

            if (invitation != null)
            {
                await _mappingService.MapUserOrgRole(Const.DEFAULT_USER_ROLE_ID, applicationUser.Id, invitation.OrgId);

                var rolePermissionMappingTemplate = await _mappingService.GetRolePermissionMappingTemplateById(Const.DEFAULT_USER_ROLE_ID);
                foreach (Permission permission in rolePermissionMappingTemplate.Permissions)
                {
                    await _mappingService.MapRolePermissionByOrg(Const.DEFAULT_USER_ROLE_ID, permission, invitation.OrgId);
                }
                await _mappingService.MapOrgUser(applicationUser.Id, invitation.OrgId);
            }
        }
    }
}
