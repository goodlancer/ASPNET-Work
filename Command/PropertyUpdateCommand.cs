using MediatR;

using System.Text.Json.Serialization;

namespace Commands
{
    public class PropertyUpdateCommand : IRequest<bool>
    {
        [JsonIgnore]
        public string OrgId { get; set; }
        public string PropertiesId { get; set; }
        public string ResourceStatus { get; set; }
        public string totalAssessedValue { get; set; }
        public string propertyDimension { get; set; }
        public string hoaRestriction { get; set; }
        public string zoningRestriction { get; set; }
        public string accessType { get; set; }
        public string visualAccess { get; set; }
        public string topography { get; set; }
        public string powerAvailable { get; set; }
        public string gasAvailable { get; set; }
        public string pertTest { get; set; }
        public string floodZone { get; set; }
        public string survey { get; set; }
        public string zoning { get; set; }
        public string[] utilities { get; set; }
        public string UserId { get; set; }
        public bool IsDueDiligenceTransaction { get; set; }
    }
}
