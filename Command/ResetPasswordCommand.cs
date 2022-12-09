using MediatR;

namespace Commands
{
    public class ResetPasswordCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string Slug { get; set; }
        public string NewPassword { get; set; }
    }
}
