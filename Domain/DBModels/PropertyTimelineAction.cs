using System;
namespace Domains.DBModels
{
    public class PropertyTimelineAction : BaseEntity
    {
        public string PropertyId { get; set; }
        public string ActionId { get; set; }
        public string UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public string DocumentBefore { get; set; }
        public string DocumentAfter { get; set; }
        public string CampaignId { get; set; }
        public string StatusBefore { get; set; }
        public string StatusAfter { get; set; }
        public string ListingId { get; set; }
        public string OfferPriceBefore { get; set; }
        public string OfferPriceAfter { get; set; }
        public string DocumentTemplateType { get; set; }
        public string MarketValueBefore { get; set; }
        public string MarketValueAfter { get; set; }

    }
}
