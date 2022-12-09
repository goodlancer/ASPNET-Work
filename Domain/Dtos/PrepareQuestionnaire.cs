using Domains.DBModels.Questionnaire;
using System.Collections.Generic;

namespace Domains.Dtos
{
    public class PrepareQuestionnaire
    {
        public List<RoomForUi<Question>> Rooms { get; set; }
    }
}
