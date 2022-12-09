using Commands;

using Domains.DBModels;

using MediatR;

using Services.Repository;

using System.Threading;
using System.Threading.Tasks;

namespace CommandHandlers.QueryHandlers
{
    public class UserExistQueryHandler : IRequestHandler<UserExistQuery, bool>
    {
        private readonly IBaseRepository<User> _userBaseRepository;
        public UserExistQueryHandler(IBaseRepository<User> userBaseRepository)
        {
            _userBaseRepository = userBaseRepository;
        }

        public async Task<bool> Handle(UserExistQuery request, CancellationToken cancellationToken)
        {
            var user = await _userBaseRepository.GetSingleAsync(x => x.Email == request.UserName);
            return user != null;
        }

    }
}
