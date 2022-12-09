using AutoMapper;

using Commands;

using Domains.DBModels;

using MediatR;

using Services.IManagers;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandler
{
    public class UpdateUserRoleCommandHandler : AsyncRequestHandler<UpdateUserRoleCommand>
    {
        private IBaseUserManager _usermanager;
        private readonly IMapper _mapper;

        public UpdateUserRoleCommandHandler(IBaseUserManager userManager
            , IMapper mapper
            )
        {
            _usermanager = userManager;
            _mapper = mapper;

        }

        protected override Task Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            List<UserRoleMapping> userRoleMappings = new List<UserRoleMapping>();

            foreach (string roleId in request.Roles)
            {
                userRoleMappings.Add(new UserRoleMapping()
                {
                    Id = Guid.NewGuid().ToString(),
                    OrganizationId = request.OrgId,
                    UserId = request.UserId,
                    RoleId = roleId
                });
            }
            _usermanager.UpdateUserRoleOrgMaps(userRoleMappings);

            return Task.CompletedTask;
        }
    }
}
