
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
    public class GetFilesQueryHandler : IRequestHandler<GetFilesQuery, List<PhFile>>
    {

        private readonly IBaseRepository<PhFile> _baseRepositoryPhFile;


        public GetFilesQueryHandler(IBaseRepository<PhFile> baseRepositoryUser)
        {
            _baseRepositoryPhFile = baseRepositoryUser;
        }

        public async Task<List<PhFile>> Handle(GetFilesQuery request, CancellationToken cancellationToken)
        {
            List<PhFile> phFiles = new List<PhFile>();

            if (request.FileIds.Any())
            {
                foreach (var requestFileId in request.FileIds)
                {
                    try
                    {
                        var phFile = await _baseRepositoryPhFile.GetByIdAsync(requestFileId);
                        if (phFile != null)
                        {
                            phFiles.Add(phFile);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }

            }

            return phFiles;
        }

    }
}
