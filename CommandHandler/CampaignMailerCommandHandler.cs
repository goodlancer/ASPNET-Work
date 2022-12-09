using Commands;
using Domains.DBModels;
using MediatR;
using ScolptioCRMCoreService.Services;
using Services.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Domains.Dtos.Pcm;
using Domains.Enum;
using System.IO;
using PdfSharp;

using PdfSharp.Pdf;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using System.Text;

namespace CommandHandlers
{
    public class CampaignMailerCommandHandler : IRequestHandler<CampaignMailerCommand, string>
    {
        private IBaseRepository<Campaign> _campaignRepository;
        private IBaseRepository<Properties> _propertyRepository;
        private IBaseRepository<Organization> _organizationRepository;
        private IBaseRepository<DocumentTemplate> _documentTemplateRepository;
        private IBaseRepository<User> _userRepository;
        private PostCardManiaService _postCardManiaService;
        public CampaignMailerCommandHandler(IBaseRepository<Campaign> campaignRepository,
            IBaseRepository<Properties> propertyRepository,
            IBaseRepository<Organization> organizationRepository,
            IBaseRepository<DocumentTemplate> documentTemplateRepository,
            IBaseRepository<User> userRepository,
            PostCardManiaService postCardManiaService)
        {
            this._campaignRepository = campaignRepository;
            this._propertyRepository = propertyRepository;
            this._organizationRepository = organizationRepository;
            this._documentTemplateRepository = documentTemplateRepository;
            this._userRepository = userRepository;
            this._postCardManiaService = postCardManiaService;
        }

        public async Task<string> Handle(CampaignMailerCommand request, CancellationToken cancellationToken)
        {
            var campaigns = (from campaign in this._campaignRepository.GetAllAsync(x => x.StartDate <= DateTime.Now &&
                                                                                                DateTime.Now <= x.EndDate &&
                                                                                                x.Status == "Active").Result
                                   where campaign.Properties.Any(y => y.CampaignMailCount == 0)
                                   select campaign).ToList();
            if (campaigns.Count == 0)
                return "";
            string mailHtml = "";
            var orders = new List<Order>();
            foreach(var campaign in campaigns)
            {
                var template = await _documentTemplateRepository.GetByIdAsync(campaign.DocumentTemplateId);
                var organization = await _organizationRepository.GetByIdAsync(campaign.OrganizationId);
                var order = new Order();
                order.ExtRefNbr = campaign.Id;
                order.OrderConfig = new OrderConfig()
                {
                    DesignId = (int)Enums.PcmDesigns.LetterTemplate,
                    MailClass = Enums.PcmMailClass.Find(x => x.Key == campaign.ShippingClass).Value,
                    MailTrackingEnabled = false,
                    GlobalDesignVariables = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("returnaddress", organization.Address),
                        new KeyValuePair<string, string>("returnaddresscity", ""), //TODO: Fill
                        new KeyValuePair<string, string>("returnaddressname", ""),
                        new KeyValuePair<string, string>("returnaddressstate", ""),
                        new KeyValuePair<string, string>("returnaddresszipcode", "")
                    },
                    LetterConfig = new LetterConfig()
                    {
                        Color = false,
                        PrintOnBothSides = false,
                        InsertAddressingPage = false,
                        EnvelopeType = "DoubleWindow"
                    }
                };
                order.RecipientList = new List<Recipient>();
                foreach(var property in campaign.Properties)
                {
                    if (property.CampaignMailCount > 0)
                        continue;
                    var propertyDetail = await _propertyRepository.GetByIdAsync(property.Id);
                    // TO-DO: Refactor campaign and use Id instead below
                    var firstName = campaign.CreatedBy.Split(' ')[0];
                    var lastName = campaign.CreatedBy.Split(' ')[campaign.CreatedBy.Split(' ').Length - 1];
                    var user = await _userRepository.GetAllAsync(x => x.FirstName == firstName && x.LastName == lastName);
                    mailHtml = PrepareMailHtml(campaign, organization, propertyDetail, user.ToList().FirstOrDefault(), template.TemplateType);
                    var pdfUrl = PreparePdfUrl(mailHtml);
                    var recipient = new Recipient()
                    {
                        FirstName = propertyDetail.Owner1FName,
                        LastName = propertyDetail.Owner1LName,
                        Address = propertyDetail.MailAddress,
                        Address2 = "",
                        City = propertyDetail.MCity,
                        State = propertyDetail.MState,
                        ZipCode = propertyDetail.PZip,
                        ExtRefNbr = propertyDetail.Id,
                        RecipientDesignVariables = new List<KeyValuePair<string, string>>()
                        {
                            new KeyValuePair<string, string>("pdfurl", pdfUrl)
                        }
                    };
                    if(string.IsNullOrEmpty(recipient.FirstName) || string.IsNullOrEmpty(recipient.LastName)) // company owner
                    {
                        recipient.FirstName = propertyDetail.Owner1LName;
                        recipient.LastName = propertyDetail.Owner1LName;
                    }
                    order.RecipientList.Add(recipient);
                }
                orders.Add(order);
            }
            await _postCardManiaService.PlaceNewOrder(orders);
            return mailHtml;
        }

        private string PreparePdfUrl(string mailHtml)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            PdfDocument pdf = PdfGenerator.GeneratePdf(mailHtml, PageSize.A4);
            pdf.AddPage();
            var fileName = Guid.NewGuid().ToString() + ".pdf";
            MemoryStream stream = new();
            pdf.Save(stream);
            stream = new MemoryStream(stream.ToArray()); // pdf.Save closes the stream so we do this

            IAmazonS3 client = new AmazonS3Client("AKIAQ7DY7O7IK52ZUSDG", "3t4AD0wK3YrEjU2gOGOkUyqHRyzRJ7pTKd7fgGkd", RegionEndpoint.USEast2); //TO-DO: Move credentials to config
            TransferUtility utility = new(client);
            TransferUtilityUploadRequest request = new()
            {
                BucketName = "ph-saas-docs",
                Key = fileName,
                InputStream = stream
            };
            utility.Upload(request);

            return "";

        }

        private string PrepareMailHtml(Campaign campaign, Organization organization, Properties properties, User user,string templateId)
        {
            string body = string.Empty;
            //using streamreader for reading my htmltemplate   
            using (StreamReader reader = new StreamReader(Path.Combine("Template", "Template_" + templateId + ".html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("&#171;Your CompanyName&#187;", organization.Title);
            body = body.Replace("&#171;Your Company Address&#187;", organization.Address);
            body = body.Replace("&#171;Your Company Email&#187;", organization.Email);
            body = body.Replace("&#171;Your Company Phone&#187;", organization.Phone);
            body = body.Replace("&#171;Your company Fax&#187;", organization.Fax);
            body = body.Replace("&#171;OwnerName_Formatted&#187;", properties.OwnerName);
            body = body.Replace("&#171;MailAddress&#187;",properties.MailAddress);
            body = body.Replace("&#171;MUnitType&#187;",properties.MUnitType);
            body = body.Replace("&#171;MUnitNumber&#187;",properties.MUnitNumber);
            body = body.Replace("&#171;MCity&#187;",properties.MCity);
            body = body.Replace("&#171;MState&#187;",properties.MState);
            body = body.Replace("&#171;MZip&#187;",properties.MZip);
            body = body.Replace("&#171;Your Name&#187;",campaign.CreatedBy);
            body = body.Replace("&#171;Your company name&#187;", organization.Title);
            body = body.Replace("&#171;MZip&#187;",properties.MZip);
            body = body.Replace("&#171;Property APN&#187;",properties.APN);
            body = body.Replace("&#171;Property Acreage&#187;", !string.IsNullOrEmpty(properties.LandSquareFootage) ? Convert.ToInt64(properties.LandSquareFootage) / 43560 + "" : "");
            body = body.Replace("&#171;Property Legal Description&#187;",properties.LandUseDescription);
            body = body.Replace("&#171;Property Market Value&#187;",properties.MarketValue);
            body = body.Replace("&#171;Property Offer Price&#187;",properties.TotalAssessedValue);
            body = body.Replace("&#171;Property Address&#187;",properties.PropertyAddress);
            body = body.Replace("&#171;CountyName&#187;",properties.CountyName);
            body = body.Replace("&#171;PState&#187;",properties.PState);
            body = body.Replace("&#171;OwnerNames&#187;",
                (!string.IsNullOrEmpty(properties.OwnerName1Full) ? properties.OwnerName1Full : "") + "</br>" +
                (!string.IsNullOrEmpty(properties.OwnerName2Full) ? properties.OwnerName2Full : "") + "</br>" +
                (!string.IsNullOrEmpty(properties.OwnerName3Full) ? properties.OwnerName3Full : "") + "</br>" +
                (!string.IsNullOrEmpty(properties.OwnerName4Full) ? properties.OwnerName4Full : "") + "</br>"
            );
            body = body.Replace("&#171;Amount&#187;",properties.TotalAssessedValue);
            //body = body.Replace, /&#171;Your Company Website&#187;",((TO-DO)));
            body = body.Replace("&#171;Your title&#187;", user.Occupation);
            return body;
        }
    }
}
