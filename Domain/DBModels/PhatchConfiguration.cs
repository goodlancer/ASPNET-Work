namespace Domains.DBModels
{
    public class PhatchConfiguration : BaseEntity
    {
        public string ConfigKey { get; set; }
        public object ConfigValue { get; set; }
    }
}
