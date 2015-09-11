using SurfsUp.Types.Integrations.Twitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfsUpRepositorys.Memory
{
    public class StatusRepository
    {
        private static IList<Status> savedStatuses;

        public StatusRepository()
        {
            if (savedStatuses == null || !savedStatuses.Any())
                savedStatuses = new List<Status>();
        }


        public async Task<IList<Status>> GetStatuses()
        {
            return savedStatuses;
        }

        public async Task SaveStatuses(IList<Status> statuses)
        {
            foreach (var status in statuses)
            {
                savedStatuses.Add(status);
            }
        }
    }
}
