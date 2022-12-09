using Domains.DBModels.Questionnaire;
using System;
using System.Collections.Generic;

namespace Domains.Dtos
{
    public class RoomForUi<TQuestion> : BaseRoom, IComparable<RoomForUi<TQuestion>> where TQuestion : Question
    {
        public List<S3File> Images { get; set; }
        public List<TQuestion> Questions { get; set; }
        public bool IsCompleted { get; set; }
        public int CompareTo(RoomForUi<TQuestion> other)
        {
            if (IsFloorPlan && other.IsFloorPlan || !IsFloorPlan && !IsFloorPlan)
                return 0;
            return IsFloorPlan ? -1 : 1;
        }
    }

}
