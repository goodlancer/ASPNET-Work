
using Commands.Query;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class GetModelFilesQueryHandler : IRequestHandler<GetModelFilesQuery, List<ModelFile>>
    {

        private readonly IBaseRepository<ModelFile> _baseRepositoryModelFile;


        public GetModelFilesQueryHandler(IBaseRepository<ModelFile> baseRepositoryModelFile)
        {
            _baseRepositoryModelFile = baseRepositoryModelFile;
        }

        public async Task<List<ModelFile>> Handle(GetModelFilesQuery request, CancellationToken cancellationToken)
        {
            var files = await _baseRepositoryModelFile.GetAllAsync(x => x.ProjectId == request.ProjectId);
            return (List<ModelFile>)files;
        }
    }
}
