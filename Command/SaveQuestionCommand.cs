using Domains.Dtos;
using MediatR;

namespace Commands
{
    public class SaveQuestionCommand : IRequest<bool>
    {
        public QuestionForUi Question { get; set; }
    }
}
