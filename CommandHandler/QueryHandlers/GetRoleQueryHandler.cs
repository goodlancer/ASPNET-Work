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
    public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, List<RolePermissionMappingTemplate>>
    {

        private readonly IBaseRepository<Role> _roleBaseRepository;
        private readonly IBaseRepository<RolePermissionMapping> _rolePermissionMappingBaseRepository;
        private readonly IBaseRepository<RolePermissionMappingTemplate> _rolePermissionMappingTemplateBaseRepository;
        private readonly IBaseRepository<Permission> _permissionBaseRepository;
        private readonly IMappingService _mappingService;


        public GetRoleQueryHandler(IBaseRepository<Role> roleBaseRepository
            , IBaseRepository<RolePermissionMapping> rolePermissionMappingBaseRepository
            , IBaseRepository<Permission> permissionBaseRepository
            , IMappingService mappingService
            , IBaseRepository<RolePermissionMappingTemplate> rolePermissionMappingTemplateBaseRepository
           )
        {
            this._roleBaseRepository = roleBaseRepository;
            this._rolePermissionMappingBaseRepository = rolePermissionMappingBaseRepository;
            this._permissionBaseRepository = permissionBaseRepository;
            this._mappingService = mappingService;
            this._rolePermissionMappingTemplateBaseRepository = rolePermissionMappingTemplateBaseRepository;
        }

        public async Task<List<RolePermissionMappingTemplate>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            var defaultRolePermissionMappingList = new List<RolePermissionMappingTemplate>();
            var roles = await _roleBaseRepository.GetAllWithPagingAsync(x => (x.OrganizationId == request.OrgId || x.OrganizationId == null), request.PageNumber, request.PageSize);
            var permissions = await _permissionBaseRepository.GetAsync();

            var allowed = new List<bool>();
            foreach (Role role in roles)
            {
                allowed.Add(true);
            }

            if (request.SearchKey != null && request.SearchKey.Length > 0)
            {
                int j = 0;
                foreach (Role role in roles)
                {
                    RolePermissionMappingTemplate defaultRoleTemplate = new RolePermissionMappingTemplate();

                    if (role.OrganizationId == null)
                    {
                        var defaultRolePermissionMapping = await _rolePermissionMappingTemplateBaseRepository.GetAsync();
                        var defaultRole = defaultRolePermissionMapping.ToList().FirstOrDefault(x => x.Id == role.Id);
                        defaultRolePermissionMappingList.Add(defaultRole);
                    }
                    else
                    {
                        defaultRoleTemplate = new RolePermissionMappingTemplate
                        {
                            Id = role.Id,
                            Title = role.Title,
                            Category = role.Category,
                            Description = role.Description,
                            IsActive = role.IsActive,
                            IsShownInUi = role.IsShownInUi,
                            Permissions = new List<Permission>()
                        };
                        var rolePermissionMappingList = await _rolePermissionMappingBaseRepository.GetAllAsync(x => x.OrganizationId == request.OrgId && x.RoleId == role.Id);
                        foreach (RolePermissionMapping mapping in rolePermissionMappingList)
                        {
                            defaultRoleTemplate.Permissions.Add(permissions.FirstOrDefault(x => x.Id == mapping.PermissionId));
                        }

                        if (defaultRoleTemplate.Title.Contains(request.SearchKey) == false)
                            allowed[j] = false;
                        j++;
                    }
                }
            }

            int w = 0;
            if (request.FilterObj != null)
            {
                foreach (Role role in roles)
                {
                    RolePermissionMappingTemplate defaultRoleTemplate = new RolePermissionMappingTemplate();

                    if (role.OrganizationId == null)
                    {
                        var defaultRolePermissionMapping = await _rolePermissionMappingTemplateBaseRepository.GetAsync();
                        var defaultRole = defaultRolePermissionMapping.ToList().FirstOrDefault(x => x.Id == role.Id);
                        defaultRolePermissionMappingList.Add(defaultRole);
                    }
                    else
                    {
                        defaultRoleTemplate = new RolePermissionMappingTemplate
                        {
                            Id = role.Id,
                            Title = role.Title,
                            Category = role.Category,
                            Description = role.Description,
                            IsActive = role.IsActive,
                            IsShownInUi = role.IsShownInUi,
                            Permissions = new List<Permission>()
                        };
                        var rolePermissionMappingList = await _rolePermissionMappingBaseRepository.GetAllAsync(x => x.OrganizationId == request.OrgId && x.RoleId == role.Id);
                        foreach (RolePermissionMapping mapping in rolePermissionMappingList)
                        {
                            defaultRoleTemplate.Permissions.Add(permissions.FirstOrDefault(x => x.Id == mapping.PermissionId));
                        }
                    }

                    if (request.FilterObj[0] != null && request.FilterObj[0].Length > 0 && defaultRoleTemplate.Title.ToLower().Contains(request.FilterObj[0].ToLower()) == false)
                        allowed[w] = false;
                    w++;
                }
            }

            w = 0;
            foreach (Role role in roles)
            {
                if (allowed[w])
                {
                    RolePermissionMappingTemplate defaultRoleTemplate = new RolePermissionMappingTemplate();

                    if (role.OrganizationId == null)
                    {
                        var defaultRolePermissionMapping = await _rolePermissionMappingTemplateBaseRepository.GetAsync();
                        var defaultRole = defaultRolePermissionMapping.ToList().FirstOrDefault(x => x.Id == role.Id);
                        defaultRolePermissionMappingList.Add(defaultRole);
                    }
                    else
                    {
                        defaultRoleTemplate = new RolePermissionMappingTemplate
                        {
                            Id = role.Id,
                            Title = role.Title,
                            Category = role.Category,
                            Description = role.Description,
                            IsActive = role.IsActive,
                            IsShownInUi = role.IsShownInUi,
                            Permissions = new List<Permission>()
                        };
                        var rolePermissionMappingList = await _rolePermissionMappingBaseRepository.GetAllAsync(x => x.OrganizationId == request.OrgId && x.RoleId == role.Id);
                        foreach (RolePermissionMapping mapping in rolePermissionMappingList)
                        {
                            defaultRoleTemplate.Permissions.Add(permissions.FirstOrDefault(x => x.Id == mapping.PermissionId));
                        }

                        defaultRolePermissionMappingList.Add(defaultRoleTemplate);
                    }
                }
                w++;
            }

            return defaultRolePermissionMappingList;
        }

    }
}
