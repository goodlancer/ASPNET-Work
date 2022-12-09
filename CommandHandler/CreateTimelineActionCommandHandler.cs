using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Commands;
using Domains.DBModels;
using Services.Repository;
using static Domains.Enum.Enums;
using Domains.Enum;

namespace CommandHandlers
{
    public class CreateTimelineActionCommandHandler : AsyncRequestHandler<CreateTimelineActionCommand>
    {
        private IBaseRepository<PropertyTimelineActionMaster> _propertyTimelineActionMaster { get; set; }
        private IBaseRepository<PropertyTimelineAction> _propertyTimelineAction { get; set; }
        private readonly string SharedText = TimelineConstants.SHARED;
        public CreateTimelineActionCommandHandler(IBaseRepository<PropertyTimelineActionMaster> propertyTimelineActionMaster, IBaseRepository<PropertyTimelineAction> propertyTimelineAction)
        {
            _propertyTimelineActionMaster = propertyTimelineActionMaster;
            _propertyTimelineAction = propertyTimelineAction;
        }
        protected async override Task Handle(CreateTimelineActionCommand request, CancellationToken cancellationToken)
        {
            var result = await this.Validate(request); // Make sure all required columns mentioned in master table have been supplied
            if (!string.IsNullOrEmpty(result))
                throw new ArgumentNullException(result);
            await _propertyTimelineAction.Create(request.NewEntry);
        }

        private async Task<string> Validate(CreateTimelineActionCommand request)
        {
            var sharedCols = (await _propertyTimelineActionMaster.GetAllAsync(x => x.Action == SharedText)).FirstOrDefault().FillColumns;
            var row = (await _propertyTimelineActionMaster.GetAllAsync(x => x.Action == request.Action.ToString())).FirstOrDefault();
            var targetColumns = row.FillColumns;
            request.NewEntry.ActionId = row.Id;
            var targetColumnList = targetColumns.Replace(SharedText, sharedCols).Split(',').ToList();
            foreach(var column in targetColumnList)
            {
                var propValue = request.NewEntry.GetType().GetProperty(column).GetValue(request.NewEntry, null);
                if (propValue == null)
                {
                    if (request.Action == TimelineAction.UpdateOfferPrice && column == "OfferPriceBefore") // ignore special case
                        continue;
                    else
                        return column; // Validation fail
                }
            }
            return ""; // Validation pass
        }
    }
}
