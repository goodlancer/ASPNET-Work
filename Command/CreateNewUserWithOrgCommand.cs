using MediatR;

namespace Commands
{
    public class CreateNewUserWithOrgCommand : IRequest
    {
        public string OrgName { get; set; }
        public string OrgTitle { get; set; }
        public string ImageId { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public CreateUserCommand CreateUserInformation { get; set; }

    }
}
