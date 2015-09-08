using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurfsUp.Models
{
    public class TwitterObject
    {
        public List<Status> statuses { get; set; }
        public SearchMetadata search_metadata { get; set; }
    }

    public class Status
    {
        public Geo geo { get; set; }
    }

    public class Geo
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class SearchMetadata
    {
        public double completed_in { get; set; }
        public long max_id { get; set; }
        public string max_id_str { get; set; }
        public string next_results { get; set; }
        public string query { get; set; }
        public string refresh_url { get; set; }
        public int count { get; set; }
        public int since_id { get; set; }
        public string since_id_str { get; set; }
    }
}