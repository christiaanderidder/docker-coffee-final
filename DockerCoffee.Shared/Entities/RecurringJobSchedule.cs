namespace DockerCoffee.Shared.Entities
{
    public class RecurringJobSchedule : BaseEntity
    {
        public RecurringJobType Type { get; set; }
        public string CronExpression { get; set; } = "*/5 * * * *";
    }
}
