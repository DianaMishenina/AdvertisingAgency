using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisingAgency
{
    public class Designs
    {
        public int DesignID { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int CampaignID { get; set; }

        public override string ToString()
        {
            return $"{DesignID} {Type} {Description} {CampaignID}";
        }
    }
}
