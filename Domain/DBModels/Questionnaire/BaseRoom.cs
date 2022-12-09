namespace Domains.DBModels.Questionnaire
{
    public class BaseRoom : BaseEntity
    {
        public string Name { get; set; }
        public bool IsFloorPlan { get; set; }
    }
}
