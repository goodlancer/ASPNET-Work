using AutoMapper;
using Commands;
using Domains.DBModels;
using MediatR;
using Services.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers
{
    public class DeleteCampaignCommandHandler : AsyncRequestHandler<DeleteCampaignCommand>
    {
        private IBaseRepository<Campaign> _baseRepositoryCampaign;

        public DeleteCampaignCommandHandler(IMapper mapper
            , IBaseRepository<Campaign> _baseRepositoryCampaign
        )
        {
            this._baseRepositoryCampaign = _baseRepositoryCampaign;
        }

        protected override async Task Handle(DeleteCampaignCommand request, CancellationToken cancellationToken)
        {
            await _baseRepositoryCampaign.Delete(request.CampaignId);
        }
    }
}
