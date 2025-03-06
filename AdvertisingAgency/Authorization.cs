using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace AdvertisingAgency
{
    public class Authorization
    {
        public int AuthorizationID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return $"{Login} {Password}";
        }

    }
}
