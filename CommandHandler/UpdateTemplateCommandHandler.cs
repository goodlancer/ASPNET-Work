using AutoMapper;

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
    public class UpdateTemplateCommandHandler : AsyncRequestHandler<UpdateTemplateCommand>
    {
        private IBaseRepository<DocumentTemplate> _baseRepositoryTemplate;
        public UpdateTemplateCommandHandler(IBaseRepository<DocumentTemplate> baseRepositoryTemplate)
        {
            _baseRepositoryTemplate = baseRepositoryTemplate;
        }

        protected override async Task Handle(UpdateTemplateCommand request, CancellationToken cancellationToken)
        {
            var data = await _baseRepositoryTemplate.GetByIdAsync(request.TemplateId);
            data.TemplateData = request.TemplateData;
            data.TemplateName = request.TemplateName;
            data.TemplateType = request.TemplateType;
            data.Status = request.Status;

            await _baseRepositoryTemplate.UpdateAsync(data);
        }
    }
}
