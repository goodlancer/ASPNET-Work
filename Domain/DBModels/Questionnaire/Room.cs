using System.Collections.Generic;

namespace Domains.DBModels.Questionnaire
{
    public class Room : BaseRoom
    {
        public string ProjectId { get; set; }
        public List<string> QuestionIds { get; set; }
        public List<string> ImageS3Keys { get; set; }
    }
}
