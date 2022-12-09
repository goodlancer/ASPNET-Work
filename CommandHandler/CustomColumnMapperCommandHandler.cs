using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class CustomColumnMapperCommandHandler : IRequestHandler<CustomColumnMapperCommand, bool>
    {
        private IBaseRepository<PropertiesFileImport> _baseRepositoryPropertiesFileImport;
        public CustomColumnMapperCommandHandler(IBaseRepository<PropertiesFileImport> baseRepositoryPropertiesFileImport)
        {
            _baseRepositoryPropertiesFileImport = baseRepositoryPropertiesFileImport;
        }
        public async Task<bool> Handle(CustomColumnMapperCommand request, CancellationToken cancellationToken)
        {
            var propertiesFileImport = await _baseRepositoryPropertiesFileImport.GetSingleAsync(x => x.Id == request.FileId);
            propertiesFileImport.ColumnMapping = request.ColumnMaps;
            propertiesFileImport.Status = "ColumnMapped";
            await _baseRepositoryPropertiesFileImport.UpdateAsync(propertiesFileImport);
            return true;
        }
    }
}
