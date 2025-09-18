using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking_System
{
    public class Host
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Apartment> Apartments { get; set; }
        public Host(int id, string name)
        {
            Id = id;
            Name = name;
            Apartments = new List<Apartment>();
        }
    }
}
