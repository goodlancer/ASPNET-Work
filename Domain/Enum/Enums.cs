using System.Collections.Generic;

namespace Domains.Enum
{
    public class Enums
    {
        public static readonly List<KeyValuePair<string, string>> PcmMailClass = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("USPS Standard Class", "Standard"),
            new KeyValuePair<string, string>("USPS First Class", "FirstClass")
        };
        public Enums()
        {
        }
        public enum WebsiteStatus
        {
            Draft = 0,
            Published = 1,
            InActive = 2,
        }
        public enum PcmDesigns
        {
            LetterTemplate = 2552
        }

        public enum TimelineAction
        {
            Import,
            Edit,
            CampaignAdd,
            CampaignRemove,
            StatusChange,
            Listing,
            UpdateOfferPrice,
            OfferGenerated,
            OtherDocGenerated,
            MarketingFilled,
            MarketValueUpdated,
            ImageAdd,
            DocumentAdd,
            DocumentDelete,
            DueDiligenceComplete,
            DueDiligenceEdit,
            ImageDelete,
            MarketingUpdated
        }

        public enum InviteOption
        {
           Invited = 1,
           Regsiterd = 2,
        }
    }
    public class TimelineConstants
    {
        public static readonly string SHARED = "((SHARED))";
        public static readonly string NameOfUser = "((NameOfUser))";
        public static readonly string CampaignName = "((CampaignName))";
        public static readonly string OfferPriceBefore = "((OfferPriceBefore))";
        public static readonly string OfferPriceAfter = "((OfferPriceAfter))";
        public static readonly string MarketValueBefore = "((MarketValueBefore))";
        public static readonly string MarketValueAfter = "((MarketValueAfter))";
        public static readonly string DocumentType = "((DocumentType))";
    }
  
}
