using Domains.Dtos;
using MediatR;
using System;
using System.Collections.Generic;

namespace Commands
{
    public class CreateCampaignCommand : IRequest
    {
        public string CampaignName { get; set; }
        public string DocumentTemplateId { get; set; }
        public List<PropertyForList> Properties { get; set; }
        public string MailType { get; set; }
        public string ShippingClass { get; set; }
        public string PrintType { get; set; }
        public string DoubleSided { get; set; }
        public double EstimatedMaillingCost { get; set; }
        public string SendingFrequency { get; set; }
        public int SendCount { get; set; }
        public int Progress { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime AddedDate { get; set; }
        public string OrganizationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string JobTime { get; set; }
        public string UserId { get; set; }

    }
}
