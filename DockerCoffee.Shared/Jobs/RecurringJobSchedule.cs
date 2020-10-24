namespace DockerCoffee.Shared.Jobs
{
    public class RecurringJobSchedule
    {
        public RecurringJobType Type { get; set; }
        public string CronExpression { get; set; } = "*/5 * * * *";
    }
}
