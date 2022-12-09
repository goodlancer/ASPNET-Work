using Domains.DBModels;
using MediatR;
using System;

namespace Commands.Query
{
    public class GetModelFileByIdQuery : IRequest<ModelFile>
    {
        public string FileId { get; set; }
    }
}
