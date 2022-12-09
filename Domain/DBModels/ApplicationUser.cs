using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

using System;
namespace Domains.DBModels
{
    [CollectionName("User")]
    [BsonIgnoreExtraElements]
    public class ApplicationUser : MongoIdentityUser<string>
    {

        public ApplicationUser() : base()
        {

        }

        public ApplicationUser(string userName, string email) : base(userName, email)
        {
        }
        public ApplicationUser(string userName, string email, string displayName) : base(userName, email)
        {
            this.DisplayName = displayName;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PassportNumber { get; set; }
        public string Occupation { get; set; }
        public string ProfileImage { get; set; }

        public string Status { get; set; }
        public string DisplayName
        {
            get { return $"{FirstName} {LastName}"; }
            set { }
        }

        public string Address { get; set; }
        //  public string OrganizationId { get; set; }
        // public string OrganizationName { get; set; }
        public string CountryName { get; set; }

        public DateTime DOB { get; set; }
        public string Salutation { get; set; }
        public string OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Signature { get; set; }
        public string ClientId { get; set; }
        public string TeamId { get; set; }
        public DateTime LastLoginUtc { get; set; }
        public string Logo { get; set; }
        public string Website { get; set; }
        public bool IsInvited { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAdress { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserType { get; set; }
        public string InvitedBy { get; set; }
    }
}
