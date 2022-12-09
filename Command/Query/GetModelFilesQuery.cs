using Domains.DBModels;
using Domains.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Commands.Query
{
    public class GetModelFilesQuery : Pagination, IRequest<List<ModelFile>>
    {
        public string ProjectId { get; set; }
    }
}
