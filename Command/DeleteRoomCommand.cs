using MediatR;

namespace Commands
{
    public class DeleteRoomCommand : IRequest<bool> 
    {
        public string Id { get; set; }
    }
}
