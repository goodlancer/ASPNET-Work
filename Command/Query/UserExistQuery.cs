
using MediatR;

namespace Commands
{
    public class UserExistQuery : IRequest<bool>
    {
        public string UserName { get; set; }

    }
}
