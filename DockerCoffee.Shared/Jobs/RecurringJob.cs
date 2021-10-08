using System;

namespace DockerCoffee.Shared.Jobs
{
    public class RecurringJob
    {
        public RecurringJobType Type { get; set; }
        public string Message { get; set; } = "";

        public string GetDeduplicationHeader() => Type.ToString();
    }
}
