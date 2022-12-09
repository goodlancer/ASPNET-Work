using System;

namespace Domains.Dtos
{
    public class PropertyForList
    {
        public int PhatchNumber { get; set; }
        public string Id { get; set; }
        public string APN { get; set; }
        public string PropertyAddress { get; set; }
        public string PCity { get; set; }
        public string PState { get; set; }
        public string PZip { get; set; }

        public string MailAddress { get; set; }
        public string MCity { get; set; }
        public string MState { get; set; }
        public string MZip { get; set; }
        public string Owner1LName { get; set; }
        public string CampaignStatus { get; set; }
        public string PropertyStatus { get; set; }
        public string CountyName { get; set; }
        public string OfferPrice { get; set; }
        public short CampaignMailCount { get; set; }
        public string TotalAssessedValue { get; set; }
        public DateTime ImportedTime { get; set; }
    }
}
