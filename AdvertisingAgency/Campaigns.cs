using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisingAgency
{
    public class Campaigns
    {
        public int CampaignID { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }

        public override string ToString()
        {
            return $"{CampaignID} {Name} {Budget} {StartDate} {EndDate} {Status}";
        }
    }
}
