using Domains.DBModels;
using Domains.Dtos;
using MediatR;
using System.Collections.Generic;

namespace Commands
{
    public class ProjectResourceUpdateCommand : IRequest<bool>
    {
        public string ProjectId { get; set; }
        public string ResourceType { get; set; }
        public List<DocumentForUi> Files { get; set; }
    }
}
