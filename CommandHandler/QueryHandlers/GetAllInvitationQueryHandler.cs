
using Commands.Query;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetAllInvitationQueryHandler : IRequestHandler<GetAllInvitationQuery, List<Invitation>>
    {

        private readonly IBaseRepository<Invitation> _baseRepositoryInvitation;
        private readonly IBaseRepository<Team> _baseRepositoryTeam;
        private readonly IBaseRepository<Role> _roleBaseRepository;

        public GetAllInvitationQueryHandler(IBaseRepository<Invitation> baseRepositoryInvitation
            , IBaseRepository<Team> baseRepositoryTeam, IBaseRepository<Role> roleBaseRepository)
        {
            _baseRepositoryInvitation = baseRepositoryInvitation;
            _baseRepositoryTeam = baseRepositoryTeam;
            _roleBaseRepository = roleBaseRepository;
        }

        public async Task<List<Invitation>> Handle(GetAllInvitationQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Invitation> invitationForList = null;
            var roleList = await _roleBaseRepository.GetAllAsync(m => request.RoleIds.Contains(m.Id));
            var isAdmin = false;
            foreach (var item in roleList)
            {
                if (item.Title == "Admin")
                {
                    isAdmin = true;
                }
            }
            if (!isAdmin)
            {
                invitationForList = await _baseRepositoryInvitation.GetAllWithPagingAsync(x => x.OrgId == request.OrgId && x.InvitedBy == request.LoogedInUserId,
                    request.PageNumber, request.PageSize);
            }
            else
            {
                invitationForList = await _baseRepositoryInvitation.GetAllWithPagingAsync(x => x.OrgId == request.OrgId, request.PageNumber, request.PageSize);
            }

            List<Invitation> invitationLIst = new List<Invitation>();

            var allowed = new List<bool>();
            foreach (Invitation invitation in invitationForList)
            {
                allowed.Add(true);
            }

            if (request.SearchKey != null && request.SearchKey.Length > 0)
            {
                int j = 0;
                foreach (Invitation invitation in invitationForList)
                {
                    if (invitation.Name.Contains(request.SearchKey) == false)
                        allowed[j] = false;
                    j++;
                }
            }

            int w = 0;
            if (request.FilterObj != null)
            {
                foreach (Invitation invitation in invitationForList)
                {
                    if (request.FilterObj[0] != null && request.FilterObj[0].Length > 0 && invitation.Name.ToLower().Contains(request.FilterObj[0].ToLower()) == false)
                        allowed[w] = false;
                    if (request.FilterObj[1] != null && request.FilterObj[1].Length > 0 && invitation.InvitedUserEmail.ToLower().Contains(request.FilterObj[1].ToLower()) == false)
                        allowed[w] = false;
                    if (request.FilterObj[2] != null && request.FilterObj[2].Length > 0 && invitation.Phone.ToLower().Contains(request.FilterObj[2].ToLower()) == false)
                        allowed[w] = false;
                    w++;
                }
            }

            w = 0;
            foreach (Invitation invitation in invitationForList)
            {
                if (allowed[w])
                {
                    var team = await _baseRepositoryTeam.GetByIdAsync(invitation.TeamId);
                    if (team != null)
                    {
                        invitation.TeamId = team.TeamName;
                    }
                    invitationLIst.Add(invitation);
                }
                w++;
            }

            return invitationLIst;
        }

    }
}
