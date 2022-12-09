using AutoMapper;

using Commands;

using Domains.DBModels;
using Domains.Dtos;

namespace ScolptioCRMWebService.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Clients, ClientsUI>();

            CreateMap<CreateUserCommand, ApplicationUser>()
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName))
                .ForMember(d => d.DOB, o => o.MapFrom(s => s.DOB))
                .ForMember(d => d.CountryName, o => o.MapFrom(s => s.CountryName))
                .ForMember(d => d.Salutation, o => o.MapFrom(s => s.Salutation))
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.Email))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
                .ForMember(d => d.Address, o => o.MapFrom(s => s.Address));

            CreateMap<CreateClientUserCommand, ApplicationUser>()
               .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName))
               .ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName))
               .ForMember(d => d.UserName, o => o.MapFrom(s => s.Email))
               .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
               .ForMember(d => d.ClientId, o => o.MapFrom(s => s.ClientId))
               .ForMember(d => d.TeamId, o => o.MapFrom(s => s.TeamId))
               .ForMember(d => d.OrganizationId, o => o.MapFrom(s => s.OrgId))
               .ForMember(d => d.Address, o => o.MapFrom(s => s.Address));

            CreateMap<CreateNewUserWithOrgCommand, User>()
               .ForMember(d => d.FirstName, o => o.MapFrom(s => s.CreateUserInformation.FirstName))
               .ForMember(d => d.Address, o => o.MapFrom(s => s.CreateUserInformation.Address))
               .ForMember(d => d.CountryName, o => o.MapFrom(s => s.CreateUserInformation.CountryName))
               .ForMember(d => d.DOB, o => o.MapFrom(s => s.CreateUserInformation.DOB));

            CreateMap<CreateRoleCommand, Role>()
               .ForMember(d => d.Title, o => o.MapFrom(s => s.RoleName))
               .ForMember(d => d.Description, o => o.MapFrom(s => s.RoleName))
               .ForMember(d => d.IsShownInUi, o => o.MapFrom(s => true))
               .ForMember(d => d.IsActive, o => o.MapFrom(s => true));

            CreateMap<User, UserForUi>()
            .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName))
            .ForMember(d => d.Address, o => o.MapFrom(s => s.Address))
            .ForMember(d => d.CountryName, o => o.MapFrom(s => s.CountryName))
            .ForMember(d => d.DOB, o => o.MapFrom(s => s.DOB));


            CreateMap<CreateListingCommand, Listing>();
            CreateMap<UpdateListingCommand, Listing>();
            CreateMap<UpdateOrgCommand, Organization>();
            CreateMap<CreateCampaignCommand, Campaign>();
            CreateMap<UpdateCampaignCommand, Campaign>();
            CreateMap<CreatePropertyDocument, PropertyDocument>();
            CreateMap<UpdatePropertyDocumentCommand, PropertyDocument>();
            CreateMap<CreateMailhouseCommand, Mailhouse>();
            CreateMap<UpdateMailhouseCommand, Mailhouse>();
            CreateMap<UpdateSalesWebsiteCommand, SalesWebsite>();
            CreateMap<CreateSalesWebsiteCommand, SalesWebsite>();
            CreateMap<Team, TeamForUi>();
            CreateMap<CreateTeamCommand, Team>();
            CreateMap<UpdateTeamCommand, Team>();
            CreateMap<UpdateRoleCommand, Role>()
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Title))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Title))
                .ForMember(d => d.IsShownInUi, o => o.MapFrom(s => true))
                .ForMember(d => d.IsActive, o => o.MapFrom(s => true));


            CreateMap<User, ApplicationUser>();
            CreateMap<ApplicationUser, User>();
            CreateMap<AgentPro, Properties>()
                .ForMember(d => d.OwnerName, o => o.MapFrom(s => s.OwnerNames));
            CreateMap<Properties, PropertyForList>();
            CreateMap<Prycd, Properties>()
               .ForMember(d => d.APN, o => o.MapFrom(s => s.APNFormatted))
               .ForMember(d => d.CountyName, o => o.MapFrom(s => s.County))
               .ForMember(d => d.LandUse, o => o.MapFrom(s => s.LandUse))
               .ForMember(d => d.Longitude, o => o.MapFrom(s => s.Longitude))
               .ForMember(d => d.LotArea, o => o.MapFrom(s => s.ACRES))
               .ForMember(d => d.LotAreaUnits, o => o.MapFrom(s => s.ACRES))
               .ForMember(d => d.LotNumber, o => o.MapFrom(s => s.LegalLot))
               .ForMember(d => d.MailAddress, o => o.MapFrom(s => s.MailingStreetAddress))
               .ForMember(d => d.MCity, o => o.MapFrom(s => s.MailCity))
               .ForMember(d => d.MState, o => o.MapFrom(s => s.MailState))
               .ForMember(d => d.MUnitNumber, o => o.MapFrom(s => s.MailUnitNumber))
               .ForMember(d => d.MZip, o => o.MapFrom(s => s.MailZZIP9))
               .ForMember(d => d.MZip4, o => o.MapFrom(s => s.MailZZIP9))
               .ForMember(d => d.Owner1FName, o => o.MapFrom(s => s.Owner1FirstName))
               .ForMember(d => d.Owner1LName, o => o.MapFrom(s => s.OwnerLastname1))
               .ForMember(d => d.Owner2FName, o => o.MapFrom(s => s.Owner2FirstName))
               .ForMember(d => d.Owner2LName, o => o.MapFrom(s => s.OwnerLastname2))
               .ForMember(d => d.OwnerNameFormatted, o => o.MapFrom(s => s.OwnerMailingName))
               .ForMember(d => d.OwnerName, o => o.MapFrom(s => s.OwnersAll))
               .ForMember(d => d.PropertyAddress, o => o.MapFrom(s => s.SitusStreetAddress))
               .ForMember(d => d.PState, o => o.MapFrom(s => s.SitusStreetAddress))
               .ForMember(d => d.PStreetName, o => o.MapFrom(s => s.SitusStreetName))
               .ForMember(d => d.PStreetSuffix, o => o.MapFrom(s => s.SitusMode))
               .ForMember(d => d.PZip, o => o.MapFrom(s => s.SitusZipCode))
               .ForMember(d => d.PZip4, o => o.MapFrom(s => s.SitusZip9))
               ;

            CreateMap<CreateFileCommand, PhFile>();
            CreateMap<UpdateSalesWebsiteCommand, SalesWebsite>()
                .ForMember(dest => dest.AboutPagePhoto, act => act.Ignore())
                .ForMember(dest => dest.InventoryHeaderPhoto, act => act.Ignore())
                .ForMember(dest => dest.WebsiteLogo, act => act.Ignore())
                .ForMember(dest => dest.CreatedBy, act => act.Ignore())
                .ForMember(dest => dest.CreatedOn, act => act.Ignore())
                ;
        }

    }
}
