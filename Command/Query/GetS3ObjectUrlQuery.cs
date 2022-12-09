using MediatR;

namespace Commands.Query
{
    public class GetS3ObjectUrlQuery : IRequest<string>
    {
        public string Key { get; set; }
    }
}
