
using System;
using System.Collections.Generic;

namespace Domains.Dtos
{
    public class UserForUi
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string DisplayName
        {
            get { return $"{FirstName} {LastName}"; }
            set { }
        }
        public string Address { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string CountryName { get; set; }
        public string Occupation { get; set; }
        public string ProfileImage { get; set; }
        public string TeamId { get; set; }
        public string TeamName { get; set; }
        public string RoleName { get; set; }
        public string PassportNumber { get; set; }
        public string UserName { get; set; }
        public DateTime DOB { get; set; }
        public string Salutation { get; set; }
        public string Status { get; set; }
        public string PhoneNumber { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Signature { get; set; }
        public string ProfileImageUrl { get; set; }
        public string ClientId { get; set; }
        public DateTime LastLoginUtc { get; set; }
        public string Logo { get; set; }
        public string Website { get; set; }
        public bool IsInvited { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAdress { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserType { get; set; }

    }
}
