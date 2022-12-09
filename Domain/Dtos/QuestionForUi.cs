using Domains.DBModels.Questionnaire;
using System.Collections.Generic;

namespace Domains.Dtos
{
    public class QuestionForUi : Question
    {
        public new List<S3File> Attachments { get; set; }
    }
}