using Domains.DBModels;

using MediatR;

using System.Collections.Generic;

namespace Commands
{
    public class UpdateListingCommand : IRequest
    {
        public string Id { get; set; }
        public double ListingPrice { get; set; }
        public double MarketValue { get; set; }
        public double Savings { get; set; }

        public string APN { get; set; }
        public string Address { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public double HOA_Fees { get; set; }

        public string AccessType { get; set; }
        public string VisualAccess { get; set; }
        public string Topography { get; set; }
        public string PowerAvailable { get; set; }
        public string GasAvailable { get; set; }
        public string PertTest { get; set; }
        public string FloodZone { get; set; }
        public string Survey { get; set; }
        public string Zoning { get; set; }
        public List<string> Utilities { get; set; }

        public string MapInformation { get; set; }
        public string OrganizationId { get; set; }

        public List<PropertyImage> Images { get; set; }
        public string Label { get; set; }

        public string Description { get; set; }
        public string ParcelSize { get; set; }
        public bool Electricity { get; set; }
        public bool Water { get; set; }
        public string LegalDescription { get; set; }
        public string Sewage { get; set; }
        public bool IsFeaturedListing { get; set; }
        public string PropertyType { get; set; }
        public string PropertyId { get; set; }
        public bool IsFromListingModule { get; set; }
        public string UserId { get; set; }
    }
}
