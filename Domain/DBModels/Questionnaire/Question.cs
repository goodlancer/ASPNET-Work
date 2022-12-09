using System.Collections.Generic;

namespace Domains.DBModels.Questionnaire
{
    public class Question : BaseEntity
    {
        public string Interrogative { get; set; }
        public string Answer { get; set; }
        public List<string> Attachments { get; set; }
    }
}
