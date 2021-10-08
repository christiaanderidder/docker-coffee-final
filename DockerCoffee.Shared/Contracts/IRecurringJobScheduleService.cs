using System.Collections.Generic;
using DockerCoffee.Shared.Entities;

namespace DockerCoffee.Shared.Contracts
{
    public interface IRecurringJobScheduleService
    {
        public List<RecurringJobSchedule> GetAll();
    }
}