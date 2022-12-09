using MediatR;

using System;

namespace Commands
{
    public class CreateUserCommand : IRequest<Boolean>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DOB { get; set; }
        public string Salutation { get; set; }
        public string CountryName { get; set; }
        public string Occupation { get; set; }
        public string ProfileImage { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Signature { get; set; }
        public DateTime UserCreationDate { get; set; }
        public string DisplayName
        {
            get { return $"{FirstName} {LastName}"; }
            set { }
        }
        public string OrganizationTitle { get; set; }
        public string ClientId { get; set; }

    }
}
