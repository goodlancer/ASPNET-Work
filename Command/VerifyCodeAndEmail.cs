using MediatR;

namespace Commands
{
    public class VerifyCodeAndEmail : IRequest<bool>
    {
        public string Code { get; set; }
        public string Email { get; set; }
    }
}
