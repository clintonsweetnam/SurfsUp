using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfsUp.Types.Integrations.Twitter
{
    public class Status
    {
        public Geo geo { get; set; }
        public string created_at { get; set; }
    }

    public class Geo
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }
}
