namespace Domains.DBModels
{
    public class MailHousePricing : BaseEntity
    {
        public string Plan { get; set; }
        public double Amount { get; set; }
        public string ShippingType { get; set; }
    }
}
