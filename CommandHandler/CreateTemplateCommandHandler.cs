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
    public class CreateTemplateCommandHandler : AsyncRequestHandler<CreateTemplateCommand>
    {
        private IBaseRepository<DocumentTemplate> _baseRepositoryTemplate;
        public CreateTemplateCommandHandler(IBaseRepository<DocumentTemplate> baseRepositoryTemplate)
        {

            _baseRepositoryTemplate = baseRepositoryTemplate;
        }

        protected override async Task Handle(CreateTemplateCommand request, CancellationToken cancellationToken)
        {
            var data = new DocumentTemplate();
            data.TemplateData = request.TemplateData;
            data.TemplateName = request.TemplateName;
            data.TemplateType = request.TemplateType;
            data.Status = request.Status;
            data.CreatedBy = request.CreatedBy;
            data.CreatedAt = DateTime.Now;
            data.OrgId = request.OrganizationId;

            await _baseRepositoryTemplate.Create(data);

        }
    }
}
