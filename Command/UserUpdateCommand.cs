using MediatR;

using System;
using System.Text.Json.Serialization;

namespace Commands
{
    public class UserUpdateCommand : IRequest
    {
        [JsonIgnore]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime DOB { get; set; }
        public string Salutation { get; set; }
        public string CountryName { get; set; }
        public string Occupation { get; set; }
        public string ProfileImage { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Signature { get; set; }
        public string DisplayName
        {
            get { return $"{FirstName} {LastName}"; }
            set { }
        }
    }
}
