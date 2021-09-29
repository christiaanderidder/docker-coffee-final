namespace DockerCoffee.Scheduler.Jobs
{
    public class RecurringJob
    {
        public RecurringJobType Type { get; set; }
        public string Message { get; set; } = "";
    }
}
