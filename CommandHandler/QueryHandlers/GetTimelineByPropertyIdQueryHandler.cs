using Commands.Query;
using Domains.DBModels;
using Domains.Dtos;
using Domains.Enum;
using MediatR;
using Services.Repository;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{

    public class GetTimelineByPropertyIdQueryHandler : IRequestHandler<GetTimelineByPropertyIdQuery, PropertyTimelineForUi>
    {
        private IBaseRepository<PropertyTimelineActionMaster> _propertyTimelineActionMaster { get; set; }
        private IBaseRepository<PropertyTimelineAction> _propertyTimelineAction { get; set; }
        private IBaseRepository<User> _userRepo { get; set; }
        private IBaseRepository<Campaign> _campaignRepo { get; set; }
        private IBaseRepository<Properties> _propertyRepo { get; set; }
        public GetTimelineByPropertyIdQueryHandler(IBaseRepository<PropertyTimelineActionMaster> propertyTimelineActionMaster,
            IBaseRepository<PropertyTimelineAction> propertyTimelineAction,
            IBaseRepository<User> userRepo,
            IBaseRepository<Campaign> campaignRepo,
            IBaseRepository<Properties> propertyRepo)
        {
            _propertyTimelineActionMaster = propertyTimelineActionMaster;
            _propertyTimelineAction = propertyTimelineAction;
            _userRepo = userRepo;
            _campaignRepo = campaignRepo;
            _propertyRepo = propertyRepo;
        }
        public async Task<PropertyTimelineForUi> Handle(GetTimelineByPropertyIdQuery request, CancellationToken cancellationToken)
        {
                PropertyTimelineForUi res = new PropertyTimelineForUi();
                var events = (await _propertyTimelineAction.GetAllAsync(x => x.PropertyId == request.PropertyId)).OrderByDescending(x => x.Timestamp);
                var master = await _propertyTimelineActionMaster.GetAllAsync(x => true); // Not many records, so get all here instead of hitting DB again and again in below loop
                var lastEventTimeStamp = DateTime.MaxValue;
                var singleDay = new SingleDayEventList();
                foreach (var ev in events)
                {
                    if (ev.Timestamp.Date != lastEventTimeStamp.Date && singleDay.Tuples.Any())
                    {
                        res.SingleDayEventLists.Add(singleDay);
                        singleDay = new SingleDayEventList();
                    }
                    var text = master.Where(x => x.Id == ev.ActionId).FirstOrDefault().Text;
                    singleDay.Tuples.Add(new Tuple<DateTime, string>(ev.Timestamp, await FillPlaceholders(text, ev)));
                    lastEventTimeStamp = ev.Timestamp;
                }
                res.SingleDayEventLists.Add(singleDay);
                return res;
        }

        private async Task<string> FillPlaceholders(string text, PropertyTimelineAction ev)
        {
            if (text.Contains(TimelineConstants.NameOfUser))
            {
                var user = await _userRepo.GetByIdAsync(ev.UserId);
                var replaceTxt = "[deleted user]";
                if(user != null)
                    replaceTxt = $"{user.FirstName} {user.LastName.First()}.";
                text = text.Replace(TimelineConstants.NameOfUser, replaceTxt);
            }
            if (text.Contains(TimelineConstants.CampaignName))
            {
                var campaign = await _campaignRepo.GetByIdAsync(ev.CampaignId);
                var replaceTxt = "[deleted campaign]";
                if (campaign != null)
                    replaceTxt = campaign.CampaignName;
                text = text.Replace(TimelineConstants.CampaignName, replaceTxt);
            }
            if (text.Contains(TimelineConstants.OfferPriceBefore))
            {
                var replaceTxt = "\"Not set\"";
                if (ev.OfferPriceBefore != "0" && !string.IsNullOrEmpty(ev.OfferPriceBefore))
                    replaceTxt = ev.OfferPriceBefore;
                text = text.Replace(TimelineConstants.OfferPriceBefore, replaceTxt);
                text = text.Replace(TimelineConstants.OfferPriceAfter, ev.OfferPriceAfter);
            }
            if (text.Contains(TimelineConstants.MarketValueBefore))
            {
                var replaceTxt = "\"Not set\"";
                if (ev.MarketValueBefore != "0" && !string.IsNullOrEmpty(ev.MarketValueBefore))
                    replaceTxt = ev.MarketValueBefore;
                text = text.Replace(TimelineConstants.MarketValueBefore, replaceTxt);
                text = text.Replace(TimelineConstants.MarketValueAfter, ev.MarketValueAfter);
            }
            if (text.Contains(TimelineConstants.DocumentType))
            {
                var replaceTxt = "[Unknown document]";
                switch (ev.DocumentTemplateType)
                {
                    case "1":
                        replaceTxt = "Offer Letter";
                        break;
                    case "2":
                        replaceTxt = "Neighbor Letter";
                        break;
                    case "3":
                        replaceTxt = "Blind Offer envelope";
                        break;
                    case "4":
                        replaceTxt = "BLIND OFFER (2nd offer)";
                        break;
                }
                text = text.Replace(TimelineConstants.DocumentType, replaceTxt);
            }
            return text;
        }
    }
}
