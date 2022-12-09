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
    public class GetModelFileByIdQueryHandler : IRequestHandler<GetModelFileByIdQuery, ModelFile>
    {
        private readonly IBaseRepository<ModelFile> _ModelFileBaseRepository;
        public GetModelFileByIdQueryHandler(IBaseRepository<ModelFile> _ModelFileBaseRepository)
        {
            this._ModelFileBaseRepository = _ModelFileBaseRepository;
        }

        public async Task<ModelFile> Handle(GetModelFileByIdQuery request, CancellationToken cancellationToken)
        {
            var ModelFile = await _ModelFileBaseRepository.GetByIdAsync(request.FileId);
            return ModelFile;
        }
    }
}
