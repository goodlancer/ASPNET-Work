using AutoMapper;

using Commands.Query;

using Domains.DBModels;
using Domains.Dtos;

using MediatR;

using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetAllUserByOrgQueryHandler : IRequestHandler<GetAllUserByOrgQuery, List<UserForUi>>
    {

        private readonly IBaseRepository<User> _userBaseRepository;
        private readonly IBaseRepository<Role> _roleBaseRepository;
        private readonly IBaseRepository<Team> _teamBaseRepository;
        private readonly IBaseRepository<TeamUserMapping> _teamUserRoleBaseRepository;

        private readonly IBaseRepository<UserRoleMapping> _userRoleMappingBaseRepository;
        private readonly IBaseRepository<UserOrganizationMapping> _userOrganizationMappingBaseRepository;
        private readonly IMapper _mapper;

        public GetAllUserByOrgQueryHandler(IBaseRepository<User> userBaseRepository
            , IBaseRepository<UserRoleMapping> userRoleMappingBaseRepository
            , IMapper mapper
            , IBaseRepository<UserOrganizationMapping> userOrganizationMappingBaseRepository
            , IBaseRepository<Role> roleBaseRepository
            , IBaseRepository<Team> teamBaseRepository
            , IBaseRepository<TeamUserMapping> teamUserRoleBaseRepository)
        {
            _userBaseRepository = userBaseRepository;
            _userRoleMappingBaseRepository = userRoleMappingBaseRepository;
            _mapper = mapper;
            _userOrganizationMappingBaseRepository = userOrganizationMappingBaseRepository;
            _roleBaseRepository = roleBaseRepository;
            _teamBaseRepository = teamBaseRepository;
            _teamUserRoleBaseRepository = teamUserRoleBaseRepository;
        }

        public async Task<List<UserForUi>> Handle(GetAllUserByOrgQuery request, CancellationToken cancellationToken)
        {
            //var roles = await _roleBaseRepository.GetAsync();
            //foreach(var role in roles)
            //{
            //    role.Id = "3de6ac94-e507-4c56-8580-c567de78f85d";
            //    await _roleBaseRepository.Create(role);
            //}
            List<string> userIds = new List<string>();
            List<UserForUi> userForUis = new List<UserForUi>();

            var roleList = await _roleBaseRepository.GetAllAsync(m => request.RoleIds.Contains(m.Id));
            var isAdmin = false;
            foreach (var item in roleList)
            {
                if (item.Title == "Admin")
                {
                    isAdmin = true;
                }
            }
            IEnumerable<User> userOrgList=null;
            if (!isAdmin)
            {
                userOrgList = await _userBaseRepository.GetAllWithPagingAsync(x => x.OrganizationId == request.OrgId && x.InvitedBy == request.LoogedInUserId, request.PageNumber, request.PageSize);
            }
            else
            {
                userOrgList = await _userBaseRepository.GetAllWithPagingAsync(x => x.OrganizationId == request.OrgId , request.PageNumber, request.PageSize);
            }
            

            var allowed = new List<bool>();
            foreach (User userOrganizationMapping in userOrgList)
            {
                allowed.Add(true);
            }

            if (request.SearchKey != null && request.SearchKey.Length > 0)
            {
                int j = 0;
                foreach (User userOrganizationMapping in userOrgList)
                {
                    var user = await _userBaseRepository.GetByIdAsync(userOrganizationMapping.Id);
                    var rolesMapping = await _userRoleMappingBaseRepository.GetAllAsync(x => x.UserId == user.Id && x.OrganizationId == request.OrgId);

                    List<string> rolesId = new List<string>();
                    foreach (UserRoleMapping rolePermissionMapping in rolesMapping)
                    {
                        rolesId.Add(rolePermissionMapping.RoleId);
                    }
                    user.Roles = rolesId;
                    var uiUser = _mapper.Map<User, UserForUi>(user);
                    if (rolesId.Count > 0)
                    {
                        var role = await _roleBaseRepository.GetByIdAsync(rolesId.First());
                        uiUser.RoleName = (role == null) ? "N/A" : role.Title;
                    }
                    else
                    {
                        uiUser.RoleName = "N/A";
                    }

                    var teamUsersMapping = await _teamUserRoleBaseRepository.GetAllAsync(x =>
                        x.OrganizationId == request.OrgId && x.UserId == userOrganizationMapping.Id);
                    if (teamUsersMapping != null && teamUsersMapping.Any())
                    {
                        var team = await _teamBaseRepository.GetSingleAsync(x => x.Id == teamUsersMapping.FirstOrDefault().TeamId);
                        if (team != null)
                        {
                            uiUser.TeamName = team.TeamName;
                        }
                    }
                    else
                    {
                        uiUser.TeamName = "N/A";
                    }

                    uiUser.Status = user.Status ?? "Active";

                    if (uiUser.DisplayName.ToLower().Contains(request.SearchKey.ToLower()) == false && uiUser.Email.ToLower().Contains(request.SearchKey.ToLower()) == false)
                        allowed[j] = false;
                    j++;
                }
            }

            int w = 0;
            if (request.FilterObj != null)
            {
                foreach (User userOrganizationMapping in userOrgList)
                {
                    var user = await _userBaseRepository.GetByIdAsync(userOrganizationMapping.Id);
                    var rolesMapping = await _userRoleMappingBaseRepository.GetAllAsync(x => x.UserId == user.Id && x.OrganizationId == request.OrgId);

                    List<string> rolesId = new List<string>();
                    foreach (UserRoleMapping rolePermissionMapping in rolesMapping)
                    {
                        rolesId.Add(rolePermissionMapping.RoleId);
                    }
                    user.Roles = rolesId;
                    var uiUser = _mapper.Map<User, UserForUi>(user);
                    if (rolesId.Count > 0)
                    {
                        var role = await _roleBaseRepository.GetByIdAsync(rolesId.First());
                        uiUser.RoleName = (role == null) ? "N/A" : role.Title;
                    }
                    else
                    {
                        uiUser.RoleName = "N/A";
                    }

                    var teamUsersMapping = await _teamUserRoleBaseRepository.GetAllAsync(x =>
                        x.OrganizationId == request.OrgId && x.UserId == userOrganizationMapping.Id);
                    if (teamUsersMapping != null && teamUsersMapping.Any())
                    {
                        var team = await _teamBaseRepository.GetSingleAsync(x => x.Id == teamUsersMapping.FirstOrDefault().TeamId);
                        if (team != null)
                        {
                            uiUser.TeamName = team.TeamName;
                        }
                    }
                    else
                    {
                        uiUser.TeamName = "N/A";
                    }

                    uiUser.Status = user.Status ?? "Active";

                    if (request.FilterObj[0] != null && request.FilterObj[0].Length > 0 && uiUser.DisplayName.ToLower().Contains(request.FilterObj[0].ToLower()) == false)
                        allowed[w] = false;
                    if (request.FilterObj[1] != null && request.FilterObj[1].Length > 0 && uiUser.Email.ToLower().Contains(request.FilterObj[1].ToLower()) == false)
                        allowed[w] = false;
                    if (request.FilterObj[2] != null && request.FilterObj[2].Length > 0 && uiUser.PhoneNumber.ToLower().Contains(request.FilterObj[1].ToLower()) == false)
                        allowed[w] = false;
                    w++;
                }
            }

            w = 0;
            foreach (User userOrganizationMapping in userOrgList)
            {
                if (allowed[w] && userOrganizationMapping != null)
                {
                    var temp = await _userBaseRepository.GetAsync();
                    var userIDs = temp.Select(m=>m.Id).ToList();
                    var re = userOrgList.Where(m => !userIDs.Contains(m.Id)).ToList();
                    var user = await _userBaseRepository.GetByIdAsync(userOrganizationMapping.Id);

                    if (user != null )
                    {
                        var rolesMapping = await _userRoleMappingBaseRepository.GetAllAsync(x => x.UserId == user.Id && x.OrganizationId == request.OrgId);

                        List<string> rolesId = new List<string>();
                        foreach (UserRoleMapping rolePermissionMapping in rolesMapping)
                        {
                            rolesId.Add(rolePermissionMapping.RoleId);
                        }
                        user.Roles = rolesId;
                        var uiUser = _mapper.Map<User, UserForUi>(user);
                        if (rolesId.Count > 0)
                        {
                            var role = await _roleBaseRepository.GetByIdAsync(rolesId.First());
                            uiUser.RoleName = (role == null) ? "N/A" : role.Title;
                        }
                        else
                        {
                            uiUser.RoleName = "N/A";
                        }

                        var teamUsersMapping = await _teamUserRoleBaseRepository.GetAllAsync(x =>
                            x.OrganizationId == request.OrgId && x.UserId == userOrganizationMapping.Id);
                        if (teamUsersMapping != null && teamUsersMapping.Any())
                        {
                            var team = await _teamBaseRepository.GetSingleAsync(x => x.Id == teamUsersMapping.FirstOrDefault().TeamId);
                            if (team != null)
                            {
                                uiUser.TeamName = team.TeamName;
                            }
                        }
                        else
                        {
                            uiUser.TeamName = "N/A";
                        }

                        uiUser.Status = user.Status ?? "Active";
                        userForUis.Add(uiUser);
                    }
                    
                }
                w++;
            }

            return userForUis;
        }

    }
}
