using Commands;

using Domains.DBModels;

using MediatR;
using Microsoft.Extensions.Logging;
using Services.Repository;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class DeleteTemplateCommandHandler : AsyncRequestHandler<DeleteTemplateCommand>
    {
        private IBaseRepository<DocumentTemplate> _baseRepositoryTemplate;
        public DeleteTemplateCommandHandler(IBaseRepository<DocumentTemplate> baseRepositoryTemplate)
        {
            _baseRepositoryTemplate = baseRepositoryTemplate;
        }

        protected override async Task Handle(DeleteTemplateCommand request, CancellationToken cancellationToken)
        {
            await _baseRepositoryTemplate.Delete(request.TemplateId);
        }
    }
}
