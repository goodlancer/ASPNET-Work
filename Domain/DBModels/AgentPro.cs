using System;

namespace Domains.DBModels
{
    public class AgentPro : BaseEntity
    {
        public string APN { get; set; }
        public string PropertyAddress { get; set; }
        public string PCity { get; set; }
        public string PState { get; set; }
        public string PZip { get; set; }
        public string PZip4 { get; set; }
        public string PUnitNumber { get; set; }
        public string PUnitType { get; set; }
        public string PCarrierRoute { get; set; }
        public string PHouseNumber { get; set; }
        public string PStreetName { get; set; }
        public string PStreetPrefix { get; set; }
        public string PStreetSuffix { get; set; }
        public string PStreetType { get; set; }
        public string CountyName { get; set; }
        public string MailAddress { get; set; }
        public string MCity { get; set; }
        public string MState { get; set; }
        public string MZip { get; set; }
        public string MZip4 { get; set; }
        public string MUnitNumber { get; set; }
        public string MUnitType { get; set; }
        public string OwnerNames { get; set; }
        public string OwnerNameFormatted { get; set; }
        public string Owner1FName { get; set; }
        public string Owner1LName { get; set; }
        public string Owner1MName { get; set; }
        public string Owner2FName { get; set; }
        public string Owner2LName { get; set; }
        public string Owner2MName { get; set; }
        public string Transfervalue { get; set; }
        public string LastSaleDate { get; set; }
        public string LastContractDate { get; set; }
        public string LastSaleDocNumber { get; set; }
        public string LastSaleBookNumber { get; set; }
        public string LastSalePageNumber { get; set; }
        public string SaleType { get; set; }
        public string HomeownerExemption { get; set; }
        public string OwnerOccupied { get; set; }
        public string Phone { get; set; }
        public string PropertyType { get; set; }
        public string LandUseDescription { get; set; }
        public string Zoning { get; set; }
        public string Beds { get; set; }
        public string Baths { get; set; }
        public string PartialBaths { get; set; }
        public string BuildingArea { get; set; }
        public string LotArea { get; set; }
        public string LotAreaUnits { get; set; }
        public string NumStories { get; set; }
        public string NumUnits { get; set; }
        public string YearBuilt { get; set; }
        public string Pool { get; set; }
        public string Tract { get; set; }
        public string Block { get; set; }
        public string LotNumber { get; set; }
        public string AreaCode { get; set; }
        public string TaxAmount { get; set; }
        public string Delinquent { get; set; }
        public string TaxRateCodeArea { get; set; }
        public string TaxYear { get; set; }
        public string TotalAssessedValue { get; set; }
        public string AssessedYear { get; set; }
        public string Distress { get; set; }
        public string TBMapGrid { get; set; }
        public string TBMapPage { get; set; }
        public string LandUse { get; set; }
        public string Longitude { get; set; }
        public bool IsMigrated { get; set; }
        public string Error { get; set; }
        public string OrgId { get; set; }
        public DateTime ImportedTime { get; set; }
        public string UserId { get; set; }
        public string ImportFileId { get; set; }

    }
}
