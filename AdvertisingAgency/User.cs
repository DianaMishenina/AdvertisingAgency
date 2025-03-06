using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisingAgency
{
    public class User
    {
        public int UserId { get; set; }
        public string Role { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime Birthday { get; set; }
        public int AuthoID { get; set; }

        public override string ToString()
        {
            return $"{UserId} {Role} {Surname} {Name} {Patronymic} {Email} {PhoneNumber} {Birthday} {AuthoID}";
        }
    }
}
